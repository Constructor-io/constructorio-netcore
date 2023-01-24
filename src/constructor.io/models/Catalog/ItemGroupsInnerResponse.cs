using System;
using Newtonsoft.Json;

/**
 * Constructor.io Item Groups Response
 */
namespace Constructorio_NET.Models
{
    public class ItemGroupsInnerResponse
    {
        [JsonProperty("processed")]
        public int Processed { get; set; }

        [JsonProperty("inserted")]
        public int Inserted { get; set; }

        [JsonProperty("updated")]
        public int Updated { get; set; }
    }
}
