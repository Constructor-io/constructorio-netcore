using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Item Groups Response
 */
namespace Constructorio_NET.Models
{
    public class ItemGroupsGetResponse
    {
        [JsonProperty("item_groups")]
        public List<ConstructorItemGroup> ItemGroups { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
    }
}
