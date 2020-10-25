using System;
using System.Threading.Tasks;
using System.Web.Http;
using CandidContribs.Core.Helpers;
using CandidContribs.Core.Models.Forms;
using MailChimp.Net;
using MailChimp.Net.Models;
using Umbraco.Core.Logging;
using Umbraco.Web.WebApi;

namespace CandidContribs.Core.Controllers.Api
{
    ///umbraco/api/MailchimpApi/
    public class MailchimpApiController : UmbracoApiController
    {
        [HttpPost]
        public async Task EventSignup(EventSignupForm input)
        {
            if (string.IsNullOrWhiteSpace(input.MailchimpGroupId))
            {
                Logger.Error<MailchimpApiController>("Unknown mailchimp group id");
                throw new Exception("Unknown mailchimp group id");
            }

            var mailChimpManager = new MailChimpManager(AppSettings.CandidContribs.MailchimpApi.Key);
            var audienceId = AppSettings.CandidContribs.MailchimpApi.AudienceId;

            var member = new MailChimp.Net.Models.Member
            {
                EmailAddress = input.Email, 
                StatusIfNew = Status.Subscribed
            };

            if (!string.IsNullOrWhiteSpace(input.First)) member.MergeFields.Add("FNAME", input.First);
            if (!string.IsNullOrWhiteSpace(input.Last)) member.MergeFields.Add("LNAME", input.Last);
           // member.MergeFields.Add("COUNTRY", "Spain");
            member.Interests.Add(input.MailchimpGroupId, true);

            await mailChimpManager.Members.AddOrUpdateAsync(audienceId, member);
        }
    }
}