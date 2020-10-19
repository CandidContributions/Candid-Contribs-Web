using CandidContribs.Core.Helpers;
using CandidContribs.Core.Services;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Scheduling;

namespace CandidContribs.Core.ScheduledTasks
{
    public class GuestbookGitHubComposer : ComponentComposer<GuestbookGitHubComponent>
    {
    }

    public class GuestbookGitHubComponent : IComponent
    {
        private IProfilingLogger _logger;
        private IGuestbookGitHubService _guestbookGitHubService;
        private BackgroundTaskRunner<IBackgroundTask> _guestbookGitHubRunner;
        private readonly IUmbracoContextFactory _context;

        public GuestbookGitHubComponent(IProfilingLogger logger, IRuntimeState runtime, IContentService contentService, IUmbracoContextFactory context, IGuestbookGitHubService guestbookGitHubService)
        {
            _context = context;
            _logger = logger;
            _guestbookGitHubRunner = new BackgroundTaskRunner<IBackgroundTask>("GuestbookGitHub", _logger);
            _guestbookGitHubService = guestbookGitHubService;
        }

        public void Initialize()
        {

            using (var cref = _context.EnsureUmbracoContext())
            {
                var minsToMilliSeconds = 60 * 1000;
                int delayBeforeWeStart = AppSettings.CandidContribs.GuestbookGitHubApi.DelayStartMins * minsToMilliSeconds;
                int howOftenWeRepeat = AppSettings.CandidContribs.GuestbookGitHubApi.RepeatMins * minsToMilliSeconds;

                var task = new GuestbookGitHub(_guestbookGitHubRunner, delayBeforeWeStart, howOftenWeRepeat, _logger, _guestbookGitHubService);

                _guestbookGitHubRunner.TryAdd(task);
            }
        }

        public void Terminate()
        {
        }
    }

    public class GuestbookGitHub : RecurringTaskBase
    {
        private IProfilingLogger _logger;
        private IGuestbookGitHubService _guestbookGitHubService;

        public GuestbookGitHub(IBackgroundTaskRunner<RecurringTaskBase> runner, int delayBeforeWeStart, int howOftenWeRepeat, IProfilingLogger logger, IGuestbookGitHubService guestbookGitHubService)
            : base(runner, delayBeforeWeStart, howOftenWeRepeat)
        {
            _logger = logger;
            _guestbookGitHubService = guestbookGitHubService;
        }

        public override bool PerformRun()
        {
            if (!AppSettings.CandidContribs.GuestbookGitHubApi.Enabled)
            {
                _logger.Info<GuestbookGitHub>("GuestbookGitHubApi import disabled");
                return false;
            }

            if (string.IsNullOrWhiteSpace(AppSettings.CandidContribs.GuestbookGitHubApi.CurrentEvent))
            {
                _logger.Info<GuestbookGitHub>("GuestbookGitHubApi import aborted: no current event set");
                return false;
            }

            _logger.Info<GuestbookGitHub>("GuestbookGitHubApi import started");

            if (!string.IsNullOrWhiteSpace(AppSettings.CandidContribs.GuestbookGitHubApi.AccessToken))
            {
                _guestbookGitHubService.DownloadGuestbookFiles(AppSettings.CandidContribs.GuestbookGitHubApi.CurrentEvent);
            }

            _guestbookGitHubService.PersistAsJson(AppSettings.CandidContribs.GuestbookGitHubApi.CurrentEvent);

            _logger.Info<GuestbookGitHub>("GuestbookGitHubApi import finished");

            return true;
        }

        public override bool IsAsync => false;
    }
}
