using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Browse Facets Response
 */
namespace Constructorio_NET.Models
{
    public class BrowseFacetsResponse
    {
        [JsonProperty("result_id")]
        public string ResultId { get; set; }

        [JsonProperty("response")]
        public BrowseFacetsResponseInner Response { get; set; }

        [JsonProperty("request")]
        public Dictionary<string, object> Request { get; set; }
    }
}