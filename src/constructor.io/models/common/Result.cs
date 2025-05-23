using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Result
 */
namespace Constructorio_NET.Models
{
    public class Result
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("data")]
        public ResultData Data { get; set; }

        [JsonProperty("matched_terms")]
        public List<string> MatchedTerms { get; set; }

        [JsonProperty("variations")]
        public List<Result> Variations { get; set; }

        [JsonProperty("variations_map")]
        public object VariationsMap { get; set; }

        [JsonProperty("variation_slice")]
        public Dictionary<string, List<string>> VariationSlice { get; set; }

        [JsonProperty("strategy")]
        public RecommendationStrategy Strategy { get; set; }

        [JsonProperty("labels")]
        public Dictionary<string, object> Labels { get; set; }
    }
}
