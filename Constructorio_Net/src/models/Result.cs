using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Result
 */
namespace Constructorio_NET
{
    public class Result {

        [JsonPropertyName("value")]
        public String Value;

        [JsonPropertyName("data")]
        public ResultData Data;

        [JsonPropertyName("matched_terms")]
        public List<String> MatchedTerms;

        [JsonPropertyName("variations")]
        public List<Result> Variations;

    }
}