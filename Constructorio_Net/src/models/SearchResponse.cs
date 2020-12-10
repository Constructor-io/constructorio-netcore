using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Search Response
 */
namespace Constructorio_NET
{
    public class SearchResponse {

        [JsonPropertyName("result_id")]
        public String ResultId { get; set; }

        [JsonPropertyName("response")]
        public SearchResponseInner Response { get; set; }

        [JsonPropertyName("request")]
        public Dictionary<String, Object> Request { get; set; }

    }
}