using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using CandidContribs.Core.Helpers;
using CandidContribs.Core.Models.Api;
using CandidContribs.Core.Models.Pages;
using CandidContribs.Core.Models.Shared;
using Umbraco.Web.Models;
using Umbraco.Core.Logging;

namespace CandidContribs.Core.Controllers
{

    public class EventStatusController : Umbraco.Web.Mvc.RenderMvcController
    {
        public override ActionResult Index(ContentModel model)
        {
            var appDataFolder = Server.MapPath("~/App_Data");
            var storageFolderPath = appDataFolder + "\\github\\umbrackathon";

            var eventStatusModel = new EventStatusModel(model.Content)
            {
                GuestbookEntries = new List<GuestbookEntry>()
            };

            if (!Directory.Exists(storageFolderPath))
            {
                return CurrentTemplate(eventStatusModel);
            }

            foreach (var filename in Directory.GetFiles(storageFolderPath))
            {
                var githubUser = Path.GetFileNameWithoutExtension(filename);
                eventStatusModel.GuestbookEntries.Add(new GuestbookEntry
                {
                    GitHubUsername = githubUser
                });
            }

            return CurrentTemplate(eventStatusModel);
        }
    }
}