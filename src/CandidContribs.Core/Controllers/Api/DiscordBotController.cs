using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CandidContribs.Core.Models.Api.DiscordBot;
using CandidContribs.Core.Models.Published;
using StackExchange.Profiling.Internal;
using Umbraco.Web;
using Umbraco.Web.WebApi;

namespace CandidContribs.Core.Controllers.Api
{
    public class DiscordBotController : UmbracoApiController
    {
        private static readonly Lazy<List<TokenRequestFail>> _tokenRequestFails = new Lazy<List<TokenRequestFail>>(() => new List<TokenRequestFail>());
        private static readonly Lazy<string> _token = new Lazy<string>(GenerateToken);

        /// <summary>
        /// This endpoint is used to block spamming secrets and supplying a very basic access token
        /// We currently only support 1 client
        /// </summary>
        /// <param name="secret"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Token")]
        public IHttpActionResult Token([FromBody]string secret)
        {
            if (secret.IsNullOrWhiteSpace())
            {
                return BadRequest("secret parameter must be specified");
            }

            var clientIp = GetClientIp();
            if (clientIp == null)
            {
                return BadRequest("Can't detect clientIp");
            }

            if (_tokenRequestFails.Value.Count(r => r.Ip == clientIp && r.DateTime >= DateTime.Now.AddHours(-3)) > 3)
            {
                return BadRequest("To many failed attempts");
            }

            var discordBotFolder = Umbraco.ContentAtRoot()
                .FirstOrDefault(n => n.IsDocumentType(DiscordBotFolder.ModelTypeAlias)) as DiscordBotFolder;
            if (discordBotFolder?.Secret.IsNullOrWhiteSpace() != false)
            {
                return BadRequest("Invalid server configuration");
            }

            if (secret != discordBotFolder.Secret)
            {
                _tokenRequestFails.Value.Add(new TokenRequestFail(DateTime.Now, clientIp));
                return BadRequest("Invalid secret");
            }

            return Ok(_token.Value);
        }

        [HttpGet]
        [Route("BingoConfiguration")]
        public IHttpActionResult BingoConfiguration(string token, int wordCount)
        {
            if (token != _token.Value)
            {
                return Unauthorized();
            }

            //todo this should be moved into a service
            var discordBotFolder = Umbraco.ContentAtRoot()
                .FirstOrDefault(n => n.IsDocumentType(DiscordBotFolder.ModelTypeAlias)) as DiscordBotFolder;
            var bingoFolder = discordBotFolder.FirstChild<BingoFolder>();
            var configuration = new BingoConfiguration();
            configuration.Words = bingoFolder.FirstChild<BingoWordsFolder>()
                .Children<BingoWord>().Select(w => w.Name).ToList();
            configuration.KeyedPhrases = bingoFolder.FirstChild<BingoPhrasesFolder>().Children<BingoPhrase>()
                .Select(kp => new KeyedPhrases
                { Key = kp.Key, Phrases = kp.Collection.Select(p => new Phrase { Boost = p.Boost, Text = p.Text }).ToList() })
                .ToList();
            var autoRoundSettings = bingoFolder.FirstChild<BingoSettings>().FirstChild<Models.Published.AutoRoundSettings>();
            configuration.AutoRoundSettings = new Models.Api.DiscordBot.AutoRoundSettings
            {
                MaximumTimeout = autoRoundSettings.MaximumTimeout,
                MinimumTimeout = autoRoundSettings.MinimumTimeOut,
                PreferedTimeout = autoRoundSettings.PreferedTimeout,
                PreferedTimeoutSkewPercentage = autoRoundSettings.PreferedTimeoutSkewPercentage
            };

            return Ok(configuration);
        }


        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }

        private static string GenerateToken()
        {
            const int length = 250;

            // creating a StringBuilder object()
            var stringBuilder = new StringBuilder();
            var random = new Random();

            for (var i = 0; i < length; i++)
            {
                var flt = random.NextDouble();
                var shift = Convert.ToInt32(Math.Floor(25 * flt));
                var letter = Convert.ToChar(shift + 65);
                stringBuilder.Append(letter);
            }

            return stringBuilder.ToString();
        }
    }
}
