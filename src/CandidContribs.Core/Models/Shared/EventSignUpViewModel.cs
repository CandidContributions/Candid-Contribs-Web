using System.Web;

namespace CandidContribs.Core.Models.Shared
{
    public class EventSignUpViewModel
    {
        public IHtmlString Text { get; set; }

        public int? MailchimpGroupId { get; set; }
    }
}