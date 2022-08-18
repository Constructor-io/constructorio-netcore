using System;
using System.Collections.Generic;
using Constructorio_NET.Models.Items;
using Newtonsoft.Json;

/**
 * Constructor.io Items Response
 */
namespace Constructorio_NET.Models
{
    public class ItemsResponse
    {
        [JsonProperty("items")]
        public List<ConstructorItem> Items { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
    }
}