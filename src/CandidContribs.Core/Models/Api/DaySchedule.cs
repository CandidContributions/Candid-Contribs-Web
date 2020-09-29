using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CandidContribs.Core.Models.Api
{
    public class DaySchedule
    {
        public DaySchedule(string dayTitle, DateTime eventStart, decimal durationInHours)
        {
            DayTitle = dayTitle;
            EventStart = eventStart;
            DurationInHours = durationInHours;
        }

        [JsonProperty("dayTitle")]
        public string DayTitle { get; set; }

        [JsonProperty("eventStart")]
        public DateTime EventStart { get; set; }
        [JsonProperty("duration")]
        public decimal DurationInHours { get; set; }

        [JsonProperty("entries")]
        public List<DayScheduleEntry> Entries { get; set; } 
    }
}