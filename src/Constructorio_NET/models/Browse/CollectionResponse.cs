using Newtonsoft.Json;
using System;
using System.Collections.Generic;

/**
 * Constructor.io Collection
 */
namespace Constructorio_NET.Models
{
    public class CollectionResponse {
        [JsonProperty("data")]
        public Dictionary<string, Object> Data { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}