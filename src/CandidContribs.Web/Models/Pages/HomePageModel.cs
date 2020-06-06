using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedModels;

namespace CandidContribs.Web.Models.Pages
{
    public class HomePageModel: Home
    {
        public HomePageModel(IPublishedContent content) : base(content) {
            AllEpisodes = new List<Episode>();
        }

        public List<Episode> AllEpisodes { get; set; }
        public Episode LatestEpisode;

    

    }
}