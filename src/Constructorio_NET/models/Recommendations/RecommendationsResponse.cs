using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Recommendations Response
 */
namespace Constructorio_NET.Models
{
    public class RecommendationsResponse {

        [JsonProperty("result_id")]
        public string ResultId { get; set; }

        [JsonProperty("response")]
        public RecommendationsResponseInner Response { get; set; }

        [JsonProperty("request")]
        public Dictionary<string, Object> Request { get; set; }

    }
}