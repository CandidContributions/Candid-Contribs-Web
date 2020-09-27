using System.ComponentModel.DataAnnotations;

namespace CandidContribs.Core.Models.Enum
{
    public enum ScheduleEntryTags
    {
        [Display(Name = "Hackathon")]
        Hackathon,
        [Display(Name = "Lightning talk")]
        LightningTalk,
        [Display(Name = "Guided conversation")]
        GuidedConversation,
        [Display(Name = "Community time")]
        CommunityTime,
        [Display(Name = "First timer session")]
        FirstTimerSession
    }
}