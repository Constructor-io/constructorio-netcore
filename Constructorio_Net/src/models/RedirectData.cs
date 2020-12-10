using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Redirect Data
 */
namespace Constructorio_NET
{
    public class RedirectData {

        [JsonPropertyName("url")]
        public String Url;

        [JsonPropertyName("rule_id")]
        public Int32 RuleId { get; set; }

        [JsonPropertyName("match_id")]
        public Int32 MatchId { get; set; }

    }
}