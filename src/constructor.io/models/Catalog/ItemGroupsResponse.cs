using System;
using System.Collections.Generic;
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
    }
}