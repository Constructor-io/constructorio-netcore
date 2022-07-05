using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Redirect
 */
namespace Constructorio_NET.Models
{
    public class Redirect {

        [JsonProperty("data")]
        public RedirectData Data { get; set; }

        [JsonProperty("matched_terms")]
        public List<String> MatchedTerms { get; set; }

        [JsonProperty("matched_user_segments")]
        public List<String> MatchedUserSegments { get; set; }

    }
}