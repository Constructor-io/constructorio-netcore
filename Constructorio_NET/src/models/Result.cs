using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Result
 */
namespace Constructorio_NET
{
    public class Result {

        [JsonProperty("value")]
        public String Value { get; set; }

        [JsonProperty("data")]
        public ResultData Data { get; set; }

        [JsonProperty("matched_terms")]
        public List<String> MatchedTerms { get; set; }

        [JsonProperty("variations")]
        public List<Result> Variations{ get; set; }

    }
}