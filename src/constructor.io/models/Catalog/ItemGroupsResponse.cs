using System;
using Newtonsoft.Json;

/**
 * Constructor.io Item Groups Response
 */
namespace Constructorio_NET.Models
{
    public class ItemGroupsResponse
    {
        [JsonProperty("item_groups")]
        public ItemGroupsInnerResponse ItemGroups { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}