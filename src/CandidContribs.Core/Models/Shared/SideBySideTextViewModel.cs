using System.Web;

namespace CandidContribs.Core.Models.Shared
{
    public class SideBySideTextViewModel
    {
        public IHtmlString TextLeft { get; set; }
        public IHtmlString TextRight { get; set; }
    }
}