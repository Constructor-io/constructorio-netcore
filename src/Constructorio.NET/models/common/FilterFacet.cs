using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Filter Facet
 **/
namespace Constructorio_NET.Models
{
    public class FilterFacet
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public Dictionary<string, object> Status { get; set; }

        [JsonProperty("max")]
        public double Max { get; set; }

        [JsonProperty("min")]
        public double Min { get; set; }

        [JsonProperty("options")]
        public List<FilterFacetOption> Options { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }
    }
}