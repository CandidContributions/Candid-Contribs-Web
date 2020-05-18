using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CandidContribs.Web.Models.Shared
{
    public class CheckBoxViewModel
    {
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("checked")]
        public bool Checked { get; set; }
    }
}