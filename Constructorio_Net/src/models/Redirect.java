using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Redirect ... uses Gson/Reflection to load data in
 */
namespace Constructorio_NET
{
    public class Redirect {

        [JsonPropertyName("data")]
        public RedirectData data;

        [JsonPropertyName("matched_terms")]
        public List<String> matchedTerms;

        [JsonPropertyName("matched_user_segments")]
        public List<String> matchedUserSegments;

    }
}