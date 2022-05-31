using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Browse Facet Options Response
 */
namespace Constructorio_NET
{
    public class BrowseFacetOptionsResponse
    {
        [JsonProperty("response")]
        public BrowseFacetOptionsResponseInner Response { get; set; }

        [JsonProperty("request")]
        public Dictionary<String, Object> Request { get; set; }
    }
}