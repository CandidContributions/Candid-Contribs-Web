using Newtonsoft.Json;

namespace CandidContribs.Core.Models.Api
{
    public class APIResponse
    {
        
        [JsonProperty("response")]
        public EpisodeResponse Response { get; set; }

        
    }
}