using System.Collections.Generic;
using Newtonsoft.Json;

namespace CandidContribs.Core.Models.Api
{
    public class GuestbookEntry
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("gitHubUsername")]
        public string GitHubUsername { get; set; }

        [JsonProperty("ourUrl")]
        public string OurUrl { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        public List<GuestbookContribution> Contributions { get; set; }

    }
}
