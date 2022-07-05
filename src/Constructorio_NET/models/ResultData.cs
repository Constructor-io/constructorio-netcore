using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Result Data
 */
namespace Constructorio_NET.Models
{
    public class ResultData {

        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("url")]
        public String Url { get; set; }

        [JsonProperty("image_url")]
        public String ImageUrl { get; set; }

        [JsonProperty("groups")]
        public List<ResultGroup> Groups { get; set; }

        [JsonProperty("facets")]
        public List<ResultFacet> Facets { get; set; }

        [JsonProperty("variation_id")]
        public String VariationId { get; set; }

        [JsonExtensionData]
        public Dictionary<String, Object> Metadata { get; set; }

    }
}