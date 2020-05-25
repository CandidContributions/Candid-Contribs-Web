using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace CandidContribs.Web.Models.Api
{
    public class DayScheduleEntry
    {
        public DayScheduleEntry(DateTime startUtc, DateTime endUtc)
        {
            Start = startUtc;
            End = endUtc;
            Tags = new List<string>();
        }

        public DayScheduleEntry(DateTime day, string startUtcString, decimal durationMins)
        {
            // start will e.g. "09:00" or "14:30" in UTC
            var startTime = TimeSpan.ParseExact(startUtcString, "h\\:mm", CultureInfo.InvariantCulture);

            Start = new DateTime(day.Year, day.Month, day.Day, startTime.Hours, startTime.Minutes, 0, DateTimeKind.Utc);
            End = Start.AddMinutes(Convert.ToInt16(durationMins));

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
        [JsonProperty("abstract")]
        public string Abstract { get; set; }
        [JsonProperty("speaker")]
        public string Speaker { get; set; }
        [JsonProperty("speakerImage")]
        public string SpeakerImage { get; set; }
        [JsonProperty("speakerAbout")]
        public string SpeakerAbout { get; set; }

        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; set; }
    }
}