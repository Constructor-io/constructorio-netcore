using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Search Response
 */
namespace Constructorio_NET.Models.Search
{
    public class SearchResponse
    {
        [JsonProperty("result_id")]
        public string ResultId { get; set; }

        [JsonProperty("response")]
        public SearchResponseInner Response { get; set; }

        [JsonProperty("request")]
        public Dictionary<string, object> Request { get; set; }
    }
}