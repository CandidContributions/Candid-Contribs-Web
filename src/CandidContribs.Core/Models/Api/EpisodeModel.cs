using System;
using Newtonsoft.Json;

namespace CandidContribs.Core.Models.Api
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
        public String Description { get; set; }

        [JsonProperty("published_at")]
        public DateTime PublishedDate { get; set; }

        [JsonProperty("plays_count")]
        public int PlaysCount { get; set; }

        [JsonProperty("downloads_count")]
        public int DownloadsCount { get; set; }

        public int GetListens() => PlaysCount + DownloadsCount;
    }
}