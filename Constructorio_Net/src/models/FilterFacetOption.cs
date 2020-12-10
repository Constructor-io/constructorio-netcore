using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Filter Facet Option
 **/
namespace Constructorio_NET
{
    public class FilterFacetOption {

        [JsonPropertyName("count")]
        public Integer Count { get; set; }

        [JsonPropertyName("data")]
        public Dictionary<String, Object> Data { get; set; }

        [JsonPropertyName("display_name")]
        public String DisplayName { get; set; }

        [JsonPropertyName("status")]
        public String Status { get; set; }

        [JsonPropertyName("value")]
        public String Value { get; set; }

    }

}