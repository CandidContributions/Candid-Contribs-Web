using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Umbraco.Core.Models;

namespace CandidContribs.Web.Models.Api
{
    public class DayScheduleEntry
    {
        public DayScheduleEntry(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
            Tags = new List<string>();
        }

        [JsonProperty("start")]
        public DateTime Start { get; set; }
        [JsonProperty("end")]
        public DateTime End { get; set; }
        [JsonProperty("duration")]
        public string Duration
        {
            get
            {
                var duration = End - Start;
                return
                    duration.Hours > 0 ?
                        $"{duration.Hours} hour{(duration.Hours > 1 ? "s" : "")}{(duration.Minutes > 0 ? " " + duration.Minutes + " minutes" : "")}" :
                        $"{duration.Minutes} minutes";
            }
        }

        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("speaker")]
        public string Speaker { get; set; }

        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; set; }
    }
}