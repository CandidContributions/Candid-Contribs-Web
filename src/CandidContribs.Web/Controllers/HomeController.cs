using CandidContribs.Web.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.PublishedModels;

namespace CandidContribs.Web.Controllers
{

    public class HomeController : Umbraco.Web.Mvc.RenderMvcController
    {
        public override ActionResult Index(ContentModel model)
        {
            var homePageModel = new HomePageModel(model.Content);
            var episodes = Umbraco.ContentSingleAtXPath("//episodesFolder").Children;
            foreach (var ep in episodes) 
            {
                homePageModel.AllEpisodes.Add((Episode)ep);
            }
            homePageModel.LatestEpisode = homePageModel.AllEpisodes.OrderByDescending(x => x.PublishedDate).FirstOrDefault();

            return CurrentTemplate(homePageModel);
        }
    }
}