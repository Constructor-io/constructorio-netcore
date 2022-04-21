using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Result Data
 */
namespace Constructorio_NET
{
    public class ResultData {

        [JsonPropertyName("description")]
        public String Description { get; set; }

        [JsonPropertyName("id")]
        public String Id { get; set; }

        [JsonPropertyName("url")]
        public String Url { get; set; }

        [JsonPropertyName("image_url")]
        public String ImageUrl { get; set; }

        [JsonPropertyName("groups")]
        public List<ResultGroup> Groups { get; set; }

        [JsonPropertyName("facets")]
        public List<ResultFacet> Facets { get; set; }

        [JsonPropertyName("variation_id")]
        public String VariationId { get; set; }

        [JsonPropertyName("metadata")]
        public Dictionary<String, Object> Metadata { get; set; }

    }
}