using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Recommendations Response
 */
namespace Constructorio_NET
{
    public class RecommendationsResponse {

        [JsonPropertyName("result_id")]
        public String ResultId { get; set; }

        [JsonPropertyName("response")]
        public RecommendationsResponseInner Response { get; set; }

        [JsonPropertyName("request")]
        public Dictionary<String, Object> Request { get; set; }

    }
}