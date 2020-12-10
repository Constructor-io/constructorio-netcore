using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Redirect Data
 */*/
namespace Constructorio_NET
{
    public class RedirectData {

        [JsonPropertyName("url")]
        public String url;

        [JsonPropertyName("rule_id")]
        public Integer ruleId;

        [JsonPropertyName("match_id")]
        public Integer matchId;

    }
}