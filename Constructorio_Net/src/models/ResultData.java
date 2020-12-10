using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Result Data
 **/
namespace Constructorio_NET
{
    public class ResultData {

        [JsonPropertyName("description")]
        public String description;

        [JsonPropertyName("id")]
        public String id;

        [JsonPropertyName("url")]
        public String url;

        [JsonPropertyName("image_url")]
        public String imageUrl;

        [JsonPropertyName("groups")]
        public List<ResultGroup> groups;

        [JsonPropertyName("facets")]
        public List<ResultFacet> facets;

        [JsonPropertyName("variation_id")]
        public String variationId;

        [JsonPropertyName("metadata")]
        public Map<String, Object> metadata;

    }
}