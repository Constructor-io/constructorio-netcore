using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Browse Facets Response
 */
namespace Constructorio_NET
{
    public class BrowseFacetsResponse
    {
        [JsonProperty("result_id")]
        public String ResultId { get; set; }

        [JsonProperty("response")]
        public BrowseFacetsResponseInner Response { get; set; }

        [JsonProperty("request")]
        public Dictionary<String, Object> Request { get; set; }
    }
}