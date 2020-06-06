using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CandidContribs.Web.Extensions;
using CandidContribs.Web.Models.Api;
using CandidContribs.Web.Models.Enum;
using CandidContribs.Web.Models.Shared;
using Newtonsoft.Json;
using Umbraco.Core;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;
using Umbraco.Web.WebApi;
using DayScheduleEntry = CandidContribs.Web.Models.Api.DayScheduleEntry;

namespace CandidContribs.Web.Controllers.Api
{
    public class PodcastApiController : UmbracoApiController
    {
        static HttpClient client = new HttpClient();

        public async Task<IEnumerable<EpisodeModel>> GetDays()
        {
            List<EpisodeModel> episodes = new List<EpisodeModel>();
            episodes = await GetEpisodes();
            return episodes;
        }

        public async Task<List<EpisodeModel>> GetEpisodes()
        {
            List<EpisodeModel> episodes = new List<EpisodeModel>();
            var path = "https://api.spreaker.com/v2/shows/4200995/episodes";

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {

                var episodeString = await response.Content.ReadAsStringAsync();
                var convertedEps = JsonConvert.DeserializeObject<APIResponse>(episodeString);
                episodes = convertedEps.Response.Items;

                var currentEpisodes = (EpisodesFolder)Umbraco.ContentSingleAtXPath("//episodesFolder");
                if (currentEpisodes != null )
                {
                    
                    IContentService contentService = Services.ContentService;
                    foreach (var ep in episodes)
                    {
                        var exists = currentEpisodes.SearchChildren(ep.Id.ToString()).Count() >0;
                        if (!exists)
                        {
                            var newPage = contentService.Create(ep.Title, currentEpisodes.Id, "episode", -1);
                            newPage.SetValue("title", ep.Title);
                            newPage.SetValue("link", ep.PlaybackUrl);
                            newPage.SetValue("spreakerId", ep.Id);
                            contentService.SaveAndPublish(newPage);
                        }
                    }
                }

            }
            return episodes;

        }

    }

}