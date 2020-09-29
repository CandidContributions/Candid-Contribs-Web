using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CandidContribs.Core.Models.Pages;
using CandidContribs.Core.Models.Published;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace CandidContribs.Core.Controllers
{

    public class HomeController : Umbraco.Web.Mvc.RenderMvcController
    {
        public override ActionResult Index(ContentModel model)
        {
            var homePageModel = new HomePageModel(model.Content)
            {
                PastEvents = new List<EventsPage>(),
                UpcomingEvents = new List<EventsPage>()
            };

            var episodesFolder = Umbraco.ContentSingleAtXPath($"//{EpisodesFolder.ModelTypeAlias}");
            if (episodesFolder != null)
            {
                var episodes = episodesFolder.Children;
                foreach (var ep in episodes)
                {
                    homePageModel.AllEpisodes.Add((Episode) ep);
                }
                homePageModel.LatestEpisode = Enumerable.OrderByDescending<Episode, DateTime>(homePageModel.AllEpisodes, x => x.PublishedDate).FirstOrDefault();
            }

            var allEventPages = Umbraco.ContentAtXPath($"//{EventsPage.ModelTypeAlias}");
            foreach (var page in allEventPages)
            {
                if (!page.IsVisible()) continue;
                if (!(page is EventsPage eventsPage)) continue;

                if (eventsPage.Part2StartDate >= DateTime.Today)
                {
                    homePageModel.UpcomingEvents.Add(eventsPage);
                }
                else
                {
                    homePageModel.PastEvents.Add(eventsPage);
                }
            }

            return CurrentTemplate(homePageModel);
        }
    }
}