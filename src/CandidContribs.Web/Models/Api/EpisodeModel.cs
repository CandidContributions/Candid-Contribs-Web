using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CandidContribs.Web.Models.Api
{
    public class EpisodeModel
    {
       

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("playback_url")]
        public String PlaybackUrl { get; set; }

        [JsonProperty("episode_id")]
        public Int32 Id { get; set; }

        [JsonProperty("description")]
        public String Descrption { get; set; }

        [JsonProperty("published_at")]
        public DateTime PublisedDate { get; set; }

        [JsonProperty("plays_count")]
        public int PlaysCount { get; set; }





    }
}