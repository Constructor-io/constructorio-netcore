using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Browse Response
 */
namespace Constructorio_NET
{
    public class BrowseResponse {

        [JsonPropertyName("result_id")]
        public String ResultId { get; set; }

        [JsonPropertyName("response")]
        public BrowseResponseInner Response { get; set; }

        [JsonPropertyName("request")]
        public Dictionary<String, Object> Request { get; set; }

    }
}