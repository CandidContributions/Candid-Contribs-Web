using Newtonsoft.Json;

namespace CandidContribs.Core.Models.Api.GitHub
{
    public class GitHubFileInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("download_url")]
        public string DownloadUrl { get; set; }

        public bool IsValidFile()
        {
            if (Type != "file") return false;
            return (Name != "readme.md" && Name != "0-template.md");
        }
    }
}
