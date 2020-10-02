using System.Linq;
using System.Net.Http;
using CandidContribs.Core.Helpers;
using CandidContribs.Core.Models.Api;
using CandidContribs.Core.Models.Published;
using Newtonsoft.Json;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Scheduling;

namespace CandidContribs.Core.ScheduledTasks
{
    public class SpreakerFeedComposer : ComponentComposer<SpreakerFeedComponent>
    {
    }

    public class SpreakerFeedComponent : IComponent
    {
        private IProfilingLogger _logger;
        private IRuntimeState _runtime;
        private IContentService _contentService;
        private BackgroundTaskRunner<IBackgroundTask> _spreakerFeedRunner;
        private readonly IUmbracoContextFactory _context;

        public SpreakerFeedComponent(IProfilingLogger logger, IRuntimeState runtime, IContentService contentService, IUmbracoContextFactory context)
        {
            _context = context;
            _logger = logger;
            _runtime = runtime;
            _contentService = contentService;
            _spreakerFeedRunner = new BackgroundTaskRunner<IBackgroundTask>("SpreakerFeed", _logger);
        }

        public void Initialize()
        {

            using (var cref = _context.EnsureUmbracoContext())
            {
                var minsToMilliSeconds = 60 * 1000;
                int delayBeforeWeStart = AppSettings.CandidContribs.SpreakerApi.DelayStartMins * minsToMilliSeconds;
                int howOftenWeRepeat = AppSettings.CandidContribs.SpreakerApi.RepeatMins * minsToMilliSeconds;

                var task = new SpreakerFeed(_spreakerFeedRunner, delayBeforeWeStart, howOftenWeRepeat, _runtime, _logger, _contentService, _context);

                _spreakerFeedRunner.TryAdd(task);
            }
        }

        public void Terminate()
        {
        }
    }

    public class SpreakerFeed : RecurringTaskBase
    {
        private IRuntimeState _runtime;
        private IProfilingLogger _logger;
        private IContentService _contentService;
        public IUmbracoContextFactory _context;

        static HttpClient client = new HttpClient();

        public SpreakerFeed(IBackgroundTaskRunner<RecurringTaskBase> runner, int delayBeforeWeStart, int howOftenWeRepeat, IRuntimeState runtime, IProfilingLogger logger, IContentService contentService, IUmbracoContextFactory context)
            : base(runner, delayBeforeWeStart, howOftenWeRepeat)
        {
            _runtime = runtime;
            _logger = logger;
            _contentService = contentService;
            _context = context;
        }

        public override bool PerformRun()
        {
            const string spreakerApiEpisodesUrlFormat = "https://api.spreaker.com/v2/shows/{0}/episodes";
            const string spreakerApiEpisodeUrlFormat = "https://api.spreaker.com/v2/episodes/{0}";

            if (!AppSettings.CandidContribs.SpreakerApi.Enabled)
            {
                _logger.Info<SpreakerFeed>("Spreaker episode import disabled");
                return false;
            }

            _logger.Info<SpreakerFeed>("Spreaker episode import started");

            using (var cref = _context.EnsureUmbracoContext())
            {
                // get episodes folder to add episodes to
                var cache = cref.UmbracoContext.Content;
                var cmsEpisodesFolder = (EpisodesFolder) cache.GetByXPath("//episodesFolder").FirstOrDefault();
                if (cmsEpisodesFolder == null)
                {
                    _logger.Error<SpreakerFeed>("Spreaker episode import failed: no EpisodesFolder found");
                    return false;
                }

                var episodesApiUrl = string.Format(spreakerApiEpisodesUrlFormat, AppSettings.CandidContribs.SpreakerApi.ShowId);
                var response = client.GetAsync(episodesApiUrl).Result;
                if (!response.IsSuccessStatusCode)
                {
                    _logger.Error<SpreakerFeed>("Spreaker episode import failed: response code {0}, url {1}", response.StatusCode, episodesApiUrl);
                    return false;
                }

                // get API response for all episodes
                var episodesString = response.Content.ReadAsStringAsync().Result;
                var convertedEps = JsonConvert.DeserializeObject<APIResponse>(episodesString);

                // get episodes in ascending date order before trying to add to CMS
                var episodes = convertedEps.Response.Items.OrderBy(x => x.PublishedDate);

                foreach (var episode in episodes)
                {
                    // is this the best way to find by API id?
                    var cmsEpisode = cmsEpisodesFolder.SearchChildren(episode.Id.ToString()).FirstOrDefault();
                    if (cmsEpisode != null)
                    {
                        // already exists so nothing to do
                        continue;
                    }

                    var episodeDetailsUrl = string.Format(spreakerApiEpisodeUrlFormat, episode.Id);
                    var episodeDetailsResponse = client.GetAsync(episodeDetailsUrl).Result;

                    if (!episodeDetailsResponse.IsSuccessStatusCode)
                    {
                        _logger.Error<SpreakerFeed>("Spreaker episode import failed: response code {0}, url {1}",
                            response.StatusCode, episodeDetailsUrl);
                        continue;
                    }

                    var episodeString = episodeDetailsResponse.Content.ReadAsStringAsync().Result;
                    var convertedEp = JsonConvert.DeserializeObject<APIResponse>(episodeString);
                    AddNewEpisode(convertedEp.Response.Episode, cmsEpisodesFolder);
                }
            }

            _logger.Info<SpreakerFeed>("Spreaker episode import finished");

            return true;
        }

        private void AddNewEpisode(EpisodeModel spreakerEpisode, EpisodesFolder cmsEpisodesFolder)
        {
            var cmsEpisode = _contentService.Create(spreakerEpisode.Title, cmsEpisodesFolder.Id, Episode.ModelTypeAlias, -1);
            cmsEpisode.SetValue("spreakerId", spreakerEpisode.Id);
            cmsEpisode.SetValue("podcastTitle", spreakerEpisode.Title);
            cmsEpisode.SetValue("podcastLink", spreakerEpisode.PlaybackUrl);
            cmsEpisode.SetValue("showNotes", spreakerEpisode.Description);
            cmsEpisode.SetValue("publishedDate", spreakerEpisode.PublishedDate);
            cmsEpisode.SetValue("listensCount", spreakerEpisode.GetListens());

            // try and set the correct title for displaying on web page
            // format should be either Episode X: title, or SX EpY: Episode
            if (spreakerEpisode.Title.Contains(":"))
            {
                cmsEpisode.SetValue("displayTitle", spreakerEpisode.Title.Split(':')[1].Trim());
            }

            _contentService.SaveAndPublish(cmsEpisode);
        }

        public override bool IsAsync => false;
    }
}
