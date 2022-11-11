using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Browse Facet Options Response
 */
namespace Constructorio_NET.Models.Browse
{
    public class BrowseFacetOptionsResponse
    {
        [JsonProperty("result_id")]
        public string ResultId { get; set; }

        [JsonProperty("response")]
        public BrowseFacetOptionsResponseInner Response { get; set; }

        [JsonProperty("request")]
        public Dictionary<string, object> Request { get; set; }
    }
}