using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web.Mvc;
using CandidContribs.Core.Models.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Umbraco.Web.WebApi;

namespace CandidContribs.Core.Controllers.Api
{
    ///umbraco/api/GuestbookApi/
    public class GuestbookApiController : UmbracoApiController
    {
        [HttpGet]
        public JArray GetEntries(string id)
        {
            Thread.Sleep(2000); // hack to keep 'planting' message visible for a couple of seconds!!

            var appDataFolder = UmbracoContext.HttpContext.Server.MapPath("~/App_Data");
            var jsonPath = $"{appDataFolder}\\github\\{id}.json";

            if (!File.Exists(jsonPath))
            {
                return new JArray();
            }

            using (var r = new StreamReader(jsonPath))
            {
                var json = r.ReadToEnd();
                return (JArray)JsonConvert.DeserializeObject(json);
            }
        }
    }
}