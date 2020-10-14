using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using CandidContribs.Core.Models.Pages;
using CandidContribs.Core.Models.Published;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace CandidContribs.Core.Controllers
{

    public class EventStatusController : Umbraco.Web.Mvc.RenderMvcController
    {
        public async Task<ActionResult> Index(ContentModel model)
        {
            var appDataFolder = Server.MapPath("~/App_Data");
            if (!System.IO.Directory.Exists(appDataFolder + "\\github"))
            {
                System.IO.Directory.CreateDirectory(appDataFolder + "\\github");
            }
            if (!System.IO.Directory.Exists(appDataFolder + "\\github\\umbrackathon"))
            {
                System.IO.Directory.CreateDirectory(appDataFolder + "\\github\\umbrackathon");
            }
            var storageFolderPath = appDataFolder + "\\github\\umbrackathon";

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MyApplication", "1"));
            var contentsUrl = "https://api.github.com/repos/candidcontributions/CanConUmbrackathon/contents/Guestbook?ref=main";
            var contentsJson = await httpClient.GetStringAsync(contentsUrl);
            var contents = (JArray)JsonConvert.DeserializeObject(contentsJson);

            foreach (var file in contents)
            {
                var filename = (string)file["name"];
                if ((string)file["type"] != "file" || filename == "readme.md")
                {
                    continue;
                }

                var downloadUrl = new Uri((string)file["download_url"]);
                using (var client = new WebClient())
                {
                    // if already have file want to overwrite contents but keep file with date created stamp
                    using (var downloadFileStream = client.OpenRead(downloadUrl))
                    {
                        using (var localFileStream = System.IO.File.OpenWrite($"{storageFolderPath}\\{filename}"))
                        {
                            downloadFileStream.CopyTo(localFileStream);
                        }
                    }
                }
            }

            return CurrentTemplate(model);
        }
    }
}