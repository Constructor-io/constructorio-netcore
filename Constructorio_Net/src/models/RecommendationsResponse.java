using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Recommendations Response
 **/
namespace Constructorio_NET
{
    public class RecommendationsResponse {

        [JsonPropertyName("result_id")]
        public String resultId;

        [JsonPropertyName("response")]
        public RecommendationsResponseInner response;

        [JsonPropertyName("request")]
        public Map<String, Object> request;

    }
}