using System.Collections.Generic;
using System.Linq;
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

        [JsonProperty("gitHubUrl")]
        public string GitHubUrl { get; set; }

        [JsonProperty("twitterUrl")]
        public string TwitterUrl { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("statusMessage")]
        public string Status { get; set; }

        [JsonProperty("thankYouMessage")]
        public string ThankYouMessage { get; set; }

        [JsonProperty("contributions")]
        public List<GuestbookContribution> Contributions { get; set; } = new List<GuestbookContribution>();

        [JsonProperty("hasContributions")]
        public bool HasContributions => Contributions.Any();

        public void SetProperty(string prefix, string content)
        {
            switch (prefix)
            {
                case "NAME":
                    DisplayName = content;
                    break;
                case "OUR":
                    OurUrl = content;
                    break;
                case "GITHUB":
                    GitHubUrl = content;
                    break;
                case "TWITTER":
                    TwitterUrl = content;
                    break;
                case "LOCATION":
                    Location = content;
                    break;
                case "STATUS":
                    Status = content;
                    break;
                case "PR":
                    Contributions.Add(new GuestbookContribution {ContributionType = ContributionType.PullRequest, Url = content});
                    break;
                case "ISSUE":
                    Contributions.Add(new GuestbookContribution { ContributionType = ContributionType.IssueTriage, Url = content });
                    break;
                case "FORUM":
                    Contributions.Add(new GuestbookContribution { ContributionType = ContributionType.ForumPost, Url = content });
                    break;
                case "BLOG":
                    Contributions.Add(new GuestbookContribution { ContributionType = ContributionType.BlogPost, Url = content });
                    break;
                case "PACKAGE":
                    Contributions.Add(new GuestbookContribution { ContributionType = ContributionType.Package, Url = content });
                    break;
            }
        }
    }
}
