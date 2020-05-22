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
                            Title = "A Level 5 Welcome!",
                            Tags = new List<string>
                                {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.CommunityTime)}
                        },
                        new DayScheduleEntry(
                            new DateTime(2020, 5, 29, 15, 0, 0, DateTimeKind.Utc),
                            new DateTime(2020, 5, 29, 16, 0, 0, DateTimeKind.Utc))
                        {
                            Title = "First timers session / What to hack",
                            Tags = new List<string>
                            {
                                EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.FirstTimerSession),
                                EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.Hackathon)
                            }
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
                            new DateTime(2020, 5, 30, 19, 0, 0, DateTimeKind.Utc))
                        {
                            Title = "BetaPatch: show off your packages!",
                            //Speaker = "Callum Whyte",
                            Tags = new List<string>
                                {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.CommunityTime)}
                        },
                        new DayScheduleEntry(
                            new DateTime(2020, 5, 30, 19, 0, 0, DateTimeKind.Utc),
                            new DateTime(2020, 5, 30, 22, 0, 0, DateTimeKind.Utc))
                        {
                            Title = "Hack time / party time ... til bed time",
                            Tags = new List<string>
                                {EnumExtensions<ScheduleEntryTags>.GetDisplayName(ScheduleEntryTags.Hackathon)}
                        },
                    }
                }
            };

        public IEnumerable<CheckBoxViewModel> GetDays()
        {
            return _codePatchSchedule.Select(
                s => new CheckBoxViewModel {Text = s.EventStart.ToString("O"), Value = s.DayTitle, Checked = false});
        }

        public IEnumerable<CheckBoxViewModel> GetActivities()
        {
            var activities = EnumExtensions<ScheduleEntryTags>.GetDisplayNames(ScheduleEntryTags.Hackathon);
            return activities.Select(x => new CheckBoxViewModel { Text = x, Value = x, Checked = false });
        }

        public IEnumerable<DaySchedule> GetSchedule()
        {
            return _codePatchSchedule;
        }
    }
}