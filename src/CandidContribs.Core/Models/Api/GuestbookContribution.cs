using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Umbraco.Core;

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

        [JsonProperty("typeName")]
        public string TypeName => ContributionType.ToString();

        [JsonProperty("text")]
        public string Text { get; set; }

    }
}
