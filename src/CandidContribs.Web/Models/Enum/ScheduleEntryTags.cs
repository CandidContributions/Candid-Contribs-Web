using System.ComponentModel.DataAnnotations;

namespace CandidContribs.Web.Models.Enum
{
    public enum ScheduleEntryTags
    {
        [Display(Name = "Hackathon")]
        Hackathon,
        [Display(Name = "Lightning talks")]
        LightningTalks,
        [Display(Name = "Guided conversation")]
        GuidedConversation,
        [Display(Name = "Community time")]
        CommunityTime,
        [Display(Name = "First timer session")]
        FirstTimerSession
    }
}