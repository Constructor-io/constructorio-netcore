using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Filter Facet
 **/
namespace Constructorio_NET
{
    public class FilterFacet {

        [JsonProperty("display_name")]
        public String DisplayName { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("status")]
        public Dictionary<String, Object> Status { get; set; }

        [JsonProperty("max")]
        public Double Max { get; set; }

        [JsonProperty("min")]
        public Double Min { get; set; }

        [JsonProperty("options")]
        public List<FilterFacetOption> Options { get; set; }

        [JsonProperty("type")]
        public String Type { get; set; }

    }

}