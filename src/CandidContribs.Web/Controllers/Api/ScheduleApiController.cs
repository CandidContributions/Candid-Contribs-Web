using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CandidContribs.Web.Extensions;
using CandidContribs.Web.Models.Api;
using CandidContribs.Web.Models.Enum;
using CandidContribs.Web.Models.Shared;
using Umbraco.Web.WebApi;

namespace CandidContribs.Web.Controllers.Api
{
    public class ScheduleApiController : UmbracoApiController
    {
        private readonly List<DaySchedule> _codePatchSchedule = new List<DaySchedule>
            {
                // NOTE THAT ALL TIMES ARE IN UTC
                new DaySchedule("part1", new DateTime(2020, 5, 29, 14, 0, 0, DateTimeKind.Utc), 7)
                {
                    Entries = new List<DayScheduleEntry>
                    {
                        new DayScheduleEntry(
                            new DateTime(2020, 5, 29, 14, 0, 0, DateTimeKind.Utc),
                            new DateTime(2020, 5, 29, 15, 0, 0, DateTimeKind.Utc))
                        {
                            Title = "Welcome and introductions",
                            Tags = new List<string>
                                {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.CommunityTime)}
                        },
                        new DayScheduleEntry(
                            new DateTime(2020, 5, 29, 15, 0, 0, DateTimeKind.Utc),
                            new DateTime(2020, 5, 29, 18, 0, 0, DateTimeKind.Utc))
                        {
                            Title = "Friday bar",
                            Tags = new List<string>
                                {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.CommunityTime)}
                        },
                        new DayScheduleEntry(
                            new DateTime(2020, 5, 29, 18, 0, 0, DateTimeKind.Utc),
                            new DateTime(2020, 5, 29, 18, 30, 0, DateTimeKind.Utc))
                        {
                            Title = "Guided conversation: e.g. Open source",
                            Tags = new List<string>
                                {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.GuidedConversation)}
                        },
                        new DayScheduleEntry(
                            new DateTime(2020, 5, 29, 18, 30, 0, DateTimeKind.Utc),
                            new DateTime(2020, 5, 29, 19, 00, 0, DateTimeKind.Utc))
                        {
                            Title = "Guided conversation: e.g. Imposter syndrome",
                            Tags = new List<string>
                                {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.GuidedConversation)}
                        }
                    }
                },
                new DaySchedule("part2", new DateTime(2020, 5, 30, 8, 0, 0, DateTimeKind.Utc), 13)
                {
                    Entries = new List<DayScheduleEntry>
                    {
                        new DayScheduleEntry(
                            new DateTime(2020, 5, 30, 8, 0, 0, DateTimeKind.Utc),
                            new DateTime(2020, 5, 30, 9, 00, 0, DateTimeKind.Utc))
                        {
                            Title = "Welcome and catch-ups",
                            Tags = new List<string>
                                {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.CommunityTime)}
                        },
                        new DayScheduleEntry(
                            new DateTime(2020, 5, 30, 9, 00, 0, DateTimeKind.Utc),
                            new DateTime(2020, 5, 30, 9, 30, 0, DateTimeKind.Utc))
                        {
                            Title = "First-timer session",
                            Tags = new List<string>
                                {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.FirstTimerSession)}
                        },
                        new DayScheduleEntry(
                            new DateTime(2020, 5, 30, 9, 30, 0, DateTimeKind.Utc),
                            new DateTime(2020, 5, 30, 10, 30, 0, DateTimeKind.Utc))
                        {
                            Title = "Hackathon",
                            Tags = new List<string>
                                {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.Hackathon)}
                        },
                        new DayScheduleEntry(
                            new DateTime(2020, 5, 30, 10, 30, 0, DateTimeKind.Utc),
                            new DateTime(2020, 5, 30, 11, 00, 0, DateTimeKind.Utc))
                        {
                            Title = "Talk: 'Lorem Ipsum dolor sit amet est acquiem era est ipsum or et adminutum'",
                            Speaker = "Jessica Smith",
                            Tags = new List<string>
                                {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.LightningTalks)}
                        }
                    }
                }
            };

        public IEnumerable<CheckBoxViewModel> GetDays()
        {
            return _codePatchSchedule.Select(
                s => new CheckBoxViewModel {Text = s.EventStart.ToString("O"), Value = s.DayTitle, Checked = true});
        }

        public IEnumerable<CheckBoxViewModel> GetActivities()
        {
            var activities = EnumExtensions<ScheduleEntryTags>.GetDisplayNames(ScheduleEntryTags.Hackathon);
            return activities.Select(x => new CheckBoxViewModel { Text = x, Value = x, Checked = true });
        }

        public IEnumerable<DaySchedule> GetSchedule()
        {
            return _codePatchSchedule;
        }
    }
}