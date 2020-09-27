using System.Collections.Generic;
using CandidContribs.Core.Models.Published;
using Umbraco.Core.Models.PublishedContent;

namespace CandidContribs.Core.Models.Pages
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