using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CandidContribs.Core.Models.Forms
{
    public class EventSignupForm
    {
        public string MailchimpGroupId { get; set; }

        public string Email { get; set; }

        public string First { get; set; }

        public string Last { get; set; }

        // signup info text from CMS
        public IHtmlString Text { get; set; }
    }
}
