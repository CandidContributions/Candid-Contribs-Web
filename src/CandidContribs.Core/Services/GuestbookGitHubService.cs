using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using CandidContribs.Core.Helpers;
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

        public void DownloadGuestbookFiles()
        {
            using (_httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Candid-Contributions-Website", "1"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", AppSettings.CandidContribs.GuestbookGitHubApi.AccessToken);

                //hard-coded for 'umbrackathon' at the moment
                SetStorageFolderPath("umbrackathon");

                var folderContents = GetGitHubFolderContents(AppSettings.CandidContribs.GuestbookGitHubApi.GuestbookUrl);

                foreach (var fileInfo in folderContents.Where(x => x.IsValidFile()))
                {
                    DownloadGitHubFile(fileInfo);
                }
            }
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
            var contentsJson = _httpClient.GetStringAsync(contentsUrl).Result;
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
                var userUrl = new Uri($"{AppSettings.CandidContribs.GuestbookGitHubApi.UsersUrl}{githubUser}");
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
