using System;
using Newtonsoft.Json;

/**
 * Constructor.io Redirect Data
 */
namespace Constructorio_NET.Models.Search
{
    public class RedirectData
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("rule_id")]
        public int RuleId { get; set; }

        [JsonProperty("match_id")]
        public int MatchId { get; set; }
    }
}