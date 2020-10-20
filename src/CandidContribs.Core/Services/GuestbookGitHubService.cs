using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using CandidContribs.Core.Helpers;
using CandidContribs.Core.Models.Api;
using CandidContribs.Core.Models.Api.GitHub;
using Newtonsoft.Json;
using Umbraco.Core.Logging;

namespace CandidContribs.Core.Services
{
    public class GuestbookGitHubService : IGuestbookGitHubService
    {
        private string _storageFolderPath;
        private HttpClient _httpClient;
        private IProfilingLogger _logger;

        public GuestbookGitHubService(IProfilingLogger logger)
        {
            _logger = logger;
        }

        public void DownloadGuestbookFiles(string eventName)
        {
            using (_httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Candid-Contributions-Website", "1"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", AppSettings.CandidContribs.GuestbookGitHubApi.AccessToken);

                SetStorageFolderPath(eventName);

                var folderContents = GetGitHubFolderContents(AppSettings.CandidContribs.GuestbookGitHubApi.GuestbookRepoFolder);

                foreach (var fileInfo in folderContents.Where(x => x.IsValidFile()))
                {
                    DownloadGitHubFile(fileInfo);
                }
            }
        }

        public void PersistAsJson(string eventName)
        {
            var appDataFolder = Umbraco.Core.IO.IOHelper.MapPath("~/App_Data");
            _storageFolderPath = $"{appDataFolder}\\github\\{eventName}";

            var entries = new List<GuestbookEntry>();

            if (Directory.Exists(_storageFolderPath))
            {
                var files = Directory.GetFiles(_storageFolderPath);
                foreach (var filename in files)
                {
                    entries.Add(GetGuestbookEntryFromMarkdown(filename));
                }
            }

            File.WriteAllText($"{appDataFolder}\\github\\{eventName}.json", JsonConvert.SerializeObject(entries));
        }

        private GuestbookEntry GetGuestbookEntryFromMarkdown(string filePath)
        {
            var entry = new GuestbookEntry
            {
                GitHubUsername = Path.GetFileNameWithoutExtension(filePath)
            };

            var lines = File.ReadLines(filePath);
            foreach (var line in lines.Where(x => !string.IsNullOrWhiteSpace(x) && x.Contains(":")))
            {
                var colon = line.IndexOf(':');
                var prefix = line.Substring(0, colon).Trim();
                var content = line.Substring(colon+1).Trim();
                entry.SetProperty(prefix, content);
            }

            return entry;
        }

        private void SetStorageFolderPath(string eventName)
        {
            var appDataFolder = Umbraco.Core.IO.IOHelper.MapPath("~/App_Data");
            _storageFolderPath = $"{appDataFolder}\\github\\{eventName}";

            if (!Directory.Exists(_storageFolderPath))
            {
                Directory.CreateDirectory(_storageFolderPath);
            }
        }

        private List<GitHubFileInfo> GetGitHubFolderContents(string contentsUrl)
        {
            var contentsJson = _httpClient.GetStringAsync($"https://api.github.com/repos/{contentsUrl}").Result;
            return JsonConvert.DeserializeObject<List<GitHubFileInfo>>(contentsJson);
        }

        private void DownloadGitHubFile(GitHubFileInfo fileInfo)
        {
            // user should have used the correct format of GitHubUsername.md
            var githubUser = Path.GetFileNameWithoutExtension(fileInfo.Name);
            var filePath = $"{_storageFolderPath}\\{fileInfo.Name}";

            // if file doesn't already exist then need to validate the username
            if (!File.Exists(filePath))
            {
                if (!IsGitHubUsernameValid(githubUser))
                {
                    // invalid username, so ignore
                    return;
                }
            }

            // get file contents and save locally - might have changed so already get contents
            var response = _httpClient.GetAsync(fileInfo.DownloadUrl).Result;
            using (var localFileStream = File.Create($"{_storageFolderPath}\\{fileInfo.Name}"))
            {
                var contentStream = response.Content.ReadAsStreamAsync().Result;
                contentStream.CopyTo(localFileStream);
            }
        }

        private bool IsGitHubUsernameValid(string githubUser)
        {
            try
            {
                var userUrl = new Uri($"https://api.github.com/users/{githubUser}");
                var contentsJson = _httpClient.GetStringAsync(userUrl).Result;
                return true;
            }
            catch (Exception ex)
            {
                _logger.Warn<GuestbookGitHubService>("Invalid Github username {Name}", githubUser);
                return false;
            }
        }
    }
}
