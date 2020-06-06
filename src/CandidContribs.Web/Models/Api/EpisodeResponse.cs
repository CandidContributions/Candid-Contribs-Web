using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CandidContribs.Web.Models.Api
{
    public class EpisodeResponse
    {
        
        [JsonProperty("items")]
        public List<EpisodeModel> Items { get; set; }

        [JsonProperty("episode")]
        public EpisodeModel Episode { get; set; }

    }
}