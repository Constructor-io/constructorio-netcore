using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Browse Facet Options Response
 */
namespace Constructorio_NET.Models
{
    public class BrowseFacetOptionsResponse
    {
        [JsonProperty("result_id")]
        public String ResultId { get; set; }

        [JsonProperty("response")]
        public BrowseFacetOptionsResponseInner Response { get; set; }

        [JsonProperty("request")]
        public Dictionary<String, Object> Request { get; set; }
    }
}