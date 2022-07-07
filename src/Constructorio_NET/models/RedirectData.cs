using System;
using Newtonsoft.Json;

/**
 * Constructor.io Redirect Data
 */
namespace Constructorio_NET.Models
{
    public class RedirectData {

        [JsonProperty("url")]
        public String Url;

        [JsonProperty("rule_id")]
        public Int32 RuleId { get; set; }

        [JsonProperty("match_id")]
        public Int32 MatchId { get; set; }

    }
}