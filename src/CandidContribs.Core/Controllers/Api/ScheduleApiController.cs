using System;
using System.Collections.Generic;
using System.Linq;
using CandidContribs.Core.Extensions;
using CandidContribs.Core.Models.Api;
using CandidContribs.Core.Models.Enum;
using CandidContribs.Core.Models.Published;
using CandidContribs.Core.Models.Shared;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web.WebApi;
using DayScheduleEntry = CandidContribs.Core.Models.Api.DayScheduleEntry;

namespace CandidContribs.Core.Controllers.Api
{
    public class ScheduleApiController : UmbracoApiController
    {
        private readonly List<DaySchedule> _schedule = new List<DaySchedule>();

        private readonly List<DaySchedule> _sampleSchedule = new List<DaySchedule>
        {
            new DaySchedule("part1", new DateTime(2020, 5, 29, 14, 0, 0, DateTimeKind.Utc), 7)
            {
                Entries = new List<DayScheduleEntry>
                {
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 29, 14, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 29, 15, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "A Level 5 Welcome!",
                        Speaker = "Candid Contributions",
                        SpeakerImage = "http://localhost:51375/media/hx4jcrst/emma_round.png",
                        SpeakerAbout = "Some interesting information about the Candid Contribs podcast",
                        Abstract =
                            "<p>This session is all about discovering and expressing your creative coding side!</p>" +
                            "<p>I believe creating software is an incredibly creative process. Although it may not feel like this when your day job is to code 'Yet Another Timesheet Application'. By doing some creative coding such as programming graphics, music or a small game you'll get a very rewarding feeling because you usually discover something new and others can enjoy your creation.</p>" +
                            "<p>Let's break away from our regular work and explore 3 fun creative programming tools to create retro games, visual arts, and music: PICO-8, Processing, and Sonic Pi. I'll give a brief demo of each of these tools to show what you can achieve with them. By the end of the talk, I hope I've inspired you to take your creative coding skills to the next level!</p>",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.CommunityTime)}
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 29, 15, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 29, 16, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "First timers session / What to hack",
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 29, 16, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 29, 17, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "Guided conversation: Community",
                        //Speaker = "Carole Logan",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.GuidedConversation)}
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 29, 17, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 29, 18, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "Hack time / chat time",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.Hackathon)}
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 29, 18, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 29, 19, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "Community catch-ups",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.CommunityTime)}
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 29, 19, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 29, 20, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "Hack time / chat time",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.Hackathon)}
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 29, 20, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 29, 22, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "Games night (or just keep coding!)",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.Hackathon)}
                    }
                }
            },
            new DaySchedule("part2", new DateTime(2020, 5, 30, 8, 0, 0, DateTimeKind.Utc), 13)
            {
                Entries = new List<DayScheduleEntry>
                {
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 30, 08, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 30, 09, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "First timers session / What to hack",
                        Tags = new List<string>
                        {
                            EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.FirstTimerSession),
                            EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.Hackathon)
                        }
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 30, 09, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 30, 10, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "Hack time / chat time",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.Hackathon)}
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 30, 10, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 30, 11, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "Lightning talks - to be announced",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.LightningTalk)}
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 30, 11, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 30, 12, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "Hack time / chat time",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.Hackathon)}
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 30, 12, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 30, 13, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "Community catch-ups",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.CommunityTime)}
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 30, 13, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 30, 14, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "Hack time / chat time",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.Hackathon)}
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 30, 14, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 30, 15, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "Lightning talks - to be announced",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.LightningTalk)}
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 30, 15, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 30, 16, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "Hack time / chat time",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.Hackathon)}
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 30, 16, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 30, 17, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "Live recording of Candid Contributions episode 12: you're all the guests!",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.GuidedConversation)}
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 30, 17, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 30, 18, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "Hack time / chat time",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.Hackathon)}
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 30, 18, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 30, 20, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "BetaPatch: show off your packages!",
                        //Speaker = "Callum Whyte",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.CommunityTime)}
                    },
                    new DayScheduleEntry(
                        new DateTime(2020, 5, 30, 20, 0, 0, DateTimeKind.Utc),
                        new DateTime(2020, 5, 30, 22, 0, 0, DateTimeKind.Utc))
                    {
                        Title = "Hack time / party time ... til bed time!",
                        Tags = new List<string>
                            {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.Hackathon)}
                    },
                }
            }
        };

        public IEnumerable<CheckBoxViewModel> GetDays()
        {
            SetUpSchedule();
            return _schedule.Select(
                s => new CheckBoxViewModel {Text = s.EventStart.ToString("O"), Value = s.DayTitle, Checked = false});
        }

        public IEnumerable<CheckBoxViewModel> GetActivities()
        {
            var dataTypeService = Services.DataTypeService;

            var editor = dataTypeService.GetDataType((string) Helpers.AppSettings.CandidContribs.ScheduleTagsKey);
            if (editor == null) return new List<CheckBoxViewModel>();

            var valueList = (ValueListConfiguration) editor.Configuration;
            return valueList.Items.Select(x => new CheckBoxViewModel
            {
                Text = x.Value,
                Value = x.Value,
                Checked = false
            });

            //var activities = EnumExtensions<ScheduleEntryTags>.GetDisplayNames(ScheduleEntryTags.Hackathon);
            //return activities.Select(x => new CheckBoxViewModel {Text = x, Value = x, Checked = false});
        }

        public IEnumerable<DaySchedule> GetSchedule()
        {
            SetUpSchedule();
            return _schedule;
        }

        private void SetUpSchedule()
        {
            // TODO - this is hardcoded to get the schedule for the first EventsPage in the tree
            var eventsPage = (EventsPage)Umbraco.ContentSingleAtXPath("//eventsPage");
            if (eventsPage == null) return;

            AddDaySchedule(1, eventsPage.Part1StartDate, eventsPage.Part1Entries);
            AddDaySchedule(2, eventsPage.Part2StartDate, eventsPage.Part2Entries);
        }

        private void AddDaySchedule(int part, DateTime startDate, IEnumerable<CandidContribs.Core.Models.Published.DayScheduleEntry> entries)
        {
            var entriesToShow = entries
                .Where(x => !x.Hidden)
                .OrderBy(x => x.StartTimeUtc).ToList();

            if (startDate == DateTime.MinValue || !entriesToShow.Any()) return;

            // start a new day schedule, start time and duration will be derived from entries
            var day = new DaySchedule($"part{part}", startDate, 0)
            {
                Entries = new List<DayScheduleEntry>()
            };

            foreach (var cmsEntry in entriesToShow)
            {
                var entry = new DayScheduleEntry(startDate, cmsEntry.StartTimeUtc, cmsEntry.DurationMins)
                {
                    Title = cmsEntry.Title, 
                    Abstract = cmsEntry.Abstract.ToHtmlString(),
                    Tags = cmsEntry.Tags
                };

                if (cmsEntry.Speaker is Person person)
                {
                    entry.Speaker = person.FullName;
                    entry.SpeakerAbout = person.About;
                    if (person.Image != null)
                    {
                        entry.SpeakerImage = person.Image.Url;
                    }
                }
                
                day.Entries.Add(entry);
            }

            // set full start date/time and duration from scheduled entries
            var firstEvent = day.Entries.First();
            day.EventStart = new DateTime(firstEvent.Start.Year, firstEvent.Start.Month, firstEvent.Start.Day, firstEvent.Start.Hour, firstEvent.Start.Minute, 0, DateTimeKind.Utc);
            day.DurationInHours = Convert.ToDecimal((double) (day.Entries.Last().End - day.EventStart).TotalHours);

            _schedule.Add(day);
        }
    }
}