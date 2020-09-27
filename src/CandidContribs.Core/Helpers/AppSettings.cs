using System.Configuration;

namespace CandidContribs.Core.Helpers
{
    public static class AppSettings
    {
        public static class CandidContribs
        {
            public static string ScheduleTagsKey => ConfigurationManager.AppSettings["CandidContribs.ScheduleTagsKey"];
            public static bool SpreakerApiEnabled => bool.TryParse(ConfigurationManager.AppSettings["CandidContribs.SpreakerApi.Enabled"], out var val) && val;
            public static string SpreakerApiShowId => ConfigurationManager.AppSettings["CandidContribs.SpreakerApi.ShowId"];
            public static int SpreakerApiDelayStartMins => int.TryParse(ConfigurationManager.AppSettings["CandidContribs.SpreakerApi.DelayStartMins"], out var val) ? val : 1;
            public static int SpreakerApiRepeatMins => int.TryParse(ConfigurationManager.AppSettings["CandidContribs.SpreakerApi.RepeatMins"], out var val) ? val : 30;
        }
    }
}