using System.Configuration;

namespace CandidContribs.Core.Helpers
{
    public static class AppSettings
    {
        public static class CandidContribs
        {
            public static class SpreakerApi
            {
                public static bool Enabled => bool.TryParse(ConfigurationManager.AppSettings["CandidContribs.SpreakerApi.Enabled"], out var val) && val;
                public static string ShowId => ConfigurationManager.AppSettings["CandidContribs.SpreakerApi.ShowId"];
                public static int DelayStartMins => int.TryParse(ConfigurationManager.AppSettings["CandidContribs.SpreakerApi.DelayStartMins"], out var val) ? val : 1;
                public static int RepeatMins => int.TryParse(ConfigurationManager.AppSettings["CandidContribs.SpreakerApi.RepeatMins"], out var val) ? val : 30;
            }

            public static class MailchimpApi
            {
                public static string Key => ConfigurationManager.AppSettings["CandidContribs.MailchimpApi.Key"];
                public static string AudienceId => ConfigurationManager.AppSettings["CandidContribs.MailchimpApi.AudienceId"];
                public static string Server => ConfigurationManager.AppSettings["CandidContribs.MailchimpApi.Server"];
            }
        }
    }
}