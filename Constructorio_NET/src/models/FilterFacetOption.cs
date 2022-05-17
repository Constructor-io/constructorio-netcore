using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Filter Facet Option
 **/
namespace Constructorio_NET
{
    public class FilterFacetOption {

        [JsonProperty("count")]
        public Int32 Count { get; set; }

        [JsonProperty("data")]
        public Dictionary<String, Object> Data { get; set; }

        [JsonProperty("display_name")]
        public String DisplayName { get; set; }

        [JsonProperty("status")]
        public String Status { get; set; }

        [JsonProperty("value")]
        public String Value { get; set; }

    }

}