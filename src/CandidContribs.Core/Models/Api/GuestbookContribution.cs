using Newtonsoft.Json;

namespace CandidContribs.Core.Models.Api
{
    public enum ContributionType
    {
        Other,
        PullRequest,
        BlogPost,
        IssueTriage,
        ForumPost,
        Package
    }
    public class GuestbookContribution
    {
        [JsonProperty("type")]
        public ContributionType ContributionType { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
