using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidContribs.Core.Models.Api.DiscordBot
{
    public class TokenRequestFail
    {
        public DateTime DateTime { get; set; }
        public string Ip { get; set; }

        public TokenRequestFail(DateTime dateTime, string ip)
        {
            DateTime = dateTime;
            Ip = ip;
        }
    }
}
