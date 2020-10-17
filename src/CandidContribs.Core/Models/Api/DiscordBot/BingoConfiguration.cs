using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidContribs.Core.Models.Api.DiscordBot
{
    public class BingoConfiguration
    {
        public List<string> Words { get; set; }
        public List<KeyedPhrases> KeyedPhrases { get; set; }
        public AutoRoundSettings AutoRoundSettings { get; set; }
    }

    public class KeyedPhrases
    {
        public string Key { get; set; }
        public List<Phrase> Phrases { get; set; }
    }

    public class Phrase
    {
        public string Text { get; set; }
        public int Boost { get; set; }
    }

    public class AutoRoundSettings
    {
        public int MinimumTimeout { get; set; }
        public int MaximumTimeout { get; set; }
        public int PreferedTimeout { get; set; }
        public int PreferedTimeoutSkewPercentage { get; set; }
    }
}
