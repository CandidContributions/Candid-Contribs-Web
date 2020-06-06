using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CandidContribs.Web.Models.Api
{
    public class APIResponse
    {
        
        [JsonProperty("response")]
        public EpisodeResponse Response { get; set; }

        
    }
}