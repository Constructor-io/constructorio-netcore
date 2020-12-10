using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Redirect
 */
namespace Constructorio_NET
{
    public class Redirect {

        [JsonPropertyName("data")]
        public RedirectData Data { get; set; }

        [JsonPropertyName("matched_terms")]
        public List<String> MatchedTerms { get; set; }

        [JsonPropertyName("matched_user_segments")]
        public List<String> MatchedUserSegments { get; set; }

    }
}