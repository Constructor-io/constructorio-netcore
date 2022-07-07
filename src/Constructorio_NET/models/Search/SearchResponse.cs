using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Search Response
 */
namespace Constructorio_NET.Models
{
    public class SearchResponse {

        [JsonProperty("result_id")]
        public String ResultId { get; set; }

        [JsonProperty("response")]
        public SearchResponseInner Response { get; set; }

        [JsonProperty("request")]
        public Dictionary<String, Object> Request { get; set; }
    }
}