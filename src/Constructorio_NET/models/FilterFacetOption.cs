using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Filter Facet Option
 **/
namespace Constructorio_NET.Models
{
    public class FilterFacetOption {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}