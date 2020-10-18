using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using CandidContribs.Core.Helpers;
using CandidContribs.Core.Models.Published;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private IRuntimeState _runtime;
        private IContentService _contentService;
        private BackgroundTaskRunner<IBackgroundTask> _guestbookGitHubRunner;
        private readonly IUmbracoContextFactory _context;

        public GuestbookGitHubComponent(IProfilingLogger logger, IRuntimeState runtime, IContentService contentService, IUmbracoContextFactory context)
        {
            _context = context;
            _logger = logger;
            _runtime = runtime;
            _contentService = contentService;
            _guestbookGitHubRunner = new BackgroundTaskRunner<IBackgroundTask>("GuestbookGitHub", _logger);
        }

        public void Initialize()
        {

            using (var cref = _context.EnsureUmbracoContext())
            {
                var minsToMilliSeconds = 60 * 1000;
                int delayBeforeWeStart = AppSettings.CandidContribs.GuestbookGitHubApi.DelayStartMins * minsToMilliSeconds;
                int howOftenWeRepeat = AppSettings.CandidContribs.GuestbookGitHubApi.RepeatMins * minsToMilliSeconds;

                var task = new GuestbookGitHub(_guestbookGitHubRunner, delayBeforeWeStart, howOftenWeRepeat, _runtime, _logger, _contentService, _context);

                _guestbookGitHubRunner.TryAdd(task);
            }
        }

        public void Terminate()
        {
        }
    }

    public class GuestbookGitHub : RecurringTaskBase
    {
        private IRuntimeState _runtime;
        private IProfilingLogger _logger;
        private IContentService _contentService;
        public IUmbracoContextFactory _context;

        static HttpClient client = new HttpClient();

        public GuestbookGitHub(IBackgroundTaskRunner<RecurringTaskBase> runner, int delayBeforeWeStart, int howOftenWeRepeat, IRuntimeState runtime, IProfilingLogger logger, IContentService contentService, IUmbracoContextFactory context)
            : base(runner, delayBeforeWeStart, howOftenWeRepeat)
        {
            _runtime = runtime;
            _logger = logger;
            _contentService = contentService;
            _context = context;
        }

        public override bool PerformRun()
        {
            if (!AppSettings.CandidContribs.GuestbookGitHubApi.Enabled)
            {
                _logger.Info<GuestbookGitHub>("GuestbookGitHubApi import disabled");
                return false;
            }

            _logger.Info<GuestbookGitHub>("GuestbookGitHubApi import started");

            var appDataFolder = Umbraco.Core.IO.IOHelper.MapPath("~/App_Data");
            if (!System.IO.Directory.Exists(appDataFolder + "\\github"))
            {
                System.IO.Directory.CreateDirectory(appDataFolder + "\\github");
            }
            if (!System.IO.Directory.Exists(appDataFolder + "\\github\\umbrackathon"))
            {
                System.IO.Directory.CreateDirectory(appDataFolder + "\\github\\umbrackathon");
            }
            var storageFolderPath = appDataFolder + "\\github\\umbrackathon";

            JArray contents;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MyApplication", "1"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", AppSettings.CandidContribs.GuestbookGitHubApi.AccessToken);
                var contentsUrl = AppSettings.CandidContribs.GuestbookGitHubApi.GuestbookUrl;
                var contentsJson = httpClient.GetStringAsync(contentsUrl).Result;
                contents = (JArray)JsonConvert.DeserializeObject(contentsJson);
            }

            foreach (var file in contents)
            {
                var filename = (string)file["name"];
                if ((string)file["type"] != "file" || filename == "readme.md" || filename == "_template.md")
                {
                    continue;
                }

                var githubUser = Path.GetFileNameWithoutExtension(filename);
                var filePath = $"{storageFolderPath}\\{filename}";
                var userUrl = new Uri($"{AppSettings.CandidContribs.GuestbookGitHubApi.UsersUrl}{githubUser}");

                if (!System.IO.File.Exists(filePath))
                {
                    // new markdown file so need to check username is valid
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MyApplication", "1"));
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", AppSettings.CandidContribs.GuestbookGitHubApi.AccessToken);
                        try
                        {
                            var contentsJson = httpClient.GetStringAsync(userUrl).Result;
                        }
                        catch (Exception ex)
                        {
                            _logger.Warn<GuestbookGitHub>("Invalid Github username {Name}", githubUser);
                            continue;
                        }
                    }
                }

                var downloadUrl = new Uri((string)file["download_url"]);
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MyApplication", "1"));
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", AppSettings.CandidContribs.GuestbookGitHubApi.AccessToken);
                    try
                    {
                        var response = httpClient.GetAsync(downloadUrl).Result;
                        using (var localFileStream = System.IO.File.OpenWrite($"{storageFolderPath}\\{filename}"))
                        {
                            var contentStream = response.Content.ReadAsStreamAsync().Result;
                            contentStream.CopyTo(localFileStream);
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.Warn<GuestbookGitHub>("Invalid Github username {Name}", githubUser);
                        continue;
                    }
                }



                //var response = await client.GetAsync(uri);
                //using (var fs = new FileStream(
                //    HostingEnvironment.MapPath(string.Format("~/Downloads/{0}.pdf", pdfGuid)),
                //    FileMode.CreateNew))
                //{
                //    await response.Content.CopyToAsync(fs);
                //}

                //using (var client = new WebClient())
                //{
                //    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MyApplication", "1"));
                //    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", AppSettings.CandidContribs.GuestbookGitHubApi.AccessToken);

                //    // if already have file want to overwrite contents but keep file with date created stamp
                //    using (var downloadFileStream = client.OpenRead(downloadUrl))
                //    {
                //        using (var localFileStream = System.IO.File.OpenWrite($"{storageFolderPath}\\{filename}"))
                //        {
                //            downloadFileStream.CopyTo(localFileStream);
                //        }
                //    }
                //}
            }

            _logger.Info<GuestbookGitHub>("GuestbookGitHubApi import finished");

            return true;
        }

        public override bool IsAsync => false;
    }
}
