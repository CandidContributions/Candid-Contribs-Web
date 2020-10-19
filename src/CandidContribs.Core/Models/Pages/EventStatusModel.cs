using System.Collections.Generic;
using CandidContribs.Core.Models.Api;
using CandidContribs.Core.Models.Published;
using CandidContribs.Core.Models.Shared;
using Umbraco.Core.Models.PublishedContent;

namespace CandidContribs.Core.Models.Pages
{
    public class EventStatusModel : EventStatus
    {
        public EventStatusModel(IPublishedContent content) : base(content) {
            
        }

        public List<GuestbookEntry> GuestbookEntries { get; set; }
    }
}