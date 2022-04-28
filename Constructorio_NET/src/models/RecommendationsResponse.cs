using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Recommendations Response
 */
namespace Constructorio_NET
{
    public class RecommendationsResponse {

        [JsonProperty("result_id")]
        public String ResultId { get; set; }

        [JsonProperty("response")]
        public RecommendationsResponseInner Response { get; set; }

        [JsonProperty("request")]
        public Dictionary<String, Object> Request { get; set; }

    }
}