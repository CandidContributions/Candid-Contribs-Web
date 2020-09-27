using System;
using System.Linq;
using System.Web.Mvc;
using CandidContribs.Core.Models.Pages;
using CandidContribs.Core.Models.Published;
using Umbraco.Web.Models;

namespace CandidContribs.Core.Controllers
{

    public class HomeController : Umbraco.Web.Mvc.RenderMvcController
    {
        public override ActionResult Index(ContentModel model)
        {
            var homePageModel = new HomePageModel(model.Content);

            var episodesFolder = Umbraco.ContentSingleAtXPath("//episodesFolder");
            if (episodesFolder != null)
            {
                var episodes = episodesFolder.Children;
                foreach (var ep in episodes)
                {
                    homePageModel.AllEpisodes.Add((Episode) ep);
                }
                homePageModel.LatestEpisode = Enumerable.OrderByDescending<Episode, DateTime>(homePageModel.AllEpisodes, x => x.PublishedDate).FirstOrDefault();
            }

            return CurrentTemplate(homePageModel);
        }
    }
}