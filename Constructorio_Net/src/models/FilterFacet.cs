using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Filter Facet
 **/
namespace Constructorio_NET
{
    public class FilterFacet {

        [JsonPropertyName("display_name")]
        public String DisplayName { get; set; }

        [JsonPropertyName("name")]
        public String Name { get; set; }

        [JsonPropertyName("status")]
        public Dictionary<String, Object> Status { get; set; }

        [JsonPropertyName("max")]
        public Double Max { get; set; }

        [JsonPropertyName("min")]
        public Double Min { get; set; }

        [JsonPropertyName("options")]
        public List<FilterFacetOption> Options { get; set; }

        [JsonPropertyName("type")]
        public String Type { get; set; }

    }

}