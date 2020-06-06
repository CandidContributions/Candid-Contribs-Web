using CandidContribs.Web.Models.Api;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;
using Umbraco.Web.Scheduling;



namespace CandidContribs.Web.ScheduledTasks
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


                int delayBeforeWeStart = 60000; // 60000ms = 1min
                int howOftenWeRepeat = 300000; //300000ms = 5mins

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

            _logger.Info<SpreakerFeed>("Running episode import");
            List<EpisodeModel> episodes = new List<EpisodeModel>();
            var path = ConfigurationManager.AppSettings["SpreakerURL"];

            using (var cref = _context.EnsureUmbracoContext())
            {

                HttpResponseMessage response = client.GetAsync(path).Result;
                if (response.IsSuccessStatusCode)
                {
                    //get API response
                    var episodeString = response.Content.ReadAsStringAsync().Result;
                    var convertedEps = JsonConvert.DeserializeObject<APIResponse>(episodeString);
                    episodes = convertedEps.Response.Items;

                    //get episodes folder
                    var cache = cref.UmbracoContext.Content;
                    var currentEpisodes = (EpisodesFolder)cache.GetByXPath("//episodesFolder").FirstOrDefault();


                    if (currentEpisodes != null)
                    {
                        foreach (var ep in episodes)
                        {
                            //hmmm not sure this is best way?
                            var exists = currentEpisodes.SearchChildren(ep.Id.ToString()).Count() > 0;
                            if (!exists)
                            {

                                var newPage = _contentService.Create(ep.Title, currentEpisodes.Id, "episode", -1);
                                newPage.SetValue("title", ep.Title);
                                newPage.SetValue("link", ep.PlaybackUrl);
                                newPage.SetValue("spreakerId", ep.Id);
                                _contentService.SaveAndPublish(newPage);

                                _logger.Info<SpreakerFeed>("New Episode added: {Title}", ep.Title);
                            }
                        }
                    }

                }

            }
            return true;
        }


        public override bool IsAsync => false;
    }
}
