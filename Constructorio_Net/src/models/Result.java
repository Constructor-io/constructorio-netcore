using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Result
 **/
namespace Constructorio_NET
{
    public class Result {

        [JsonPropertyName("value")]
        public String value;

        [JsonPropertyName("data")]
        public ResultData data;

        [JsonPropertyName("matched_terms")]
        public List<String> matchedTerms;

        [JsonPropertyName("variations")]
        public List<Result> variations;

    }
}