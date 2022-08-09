using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Result Data
 */
namespace Constructorio_NET.Models
{
    public class ResultData
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("groups")]
        public List<ResultGroup> Groups { get; set; }

        [JsonProperty("facets")]
        public List<ResultFacet> Facets { get; set; }

        [JsonProperty("variation_id")]
        public string VariationId { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> Metadata { get; set; }
    }
}