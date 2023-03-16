using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Collection
 */
namespace Constructorio_NET.Models.Browse
{
    public class CollectionResponse
    {
        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}