using System;
using System.Collections.Generic;
using Constructorio_NET.Models.Items;
using Newtonsoft.Json;

/**
 * Constructor.io Items Response
 */
namespace Constructorio_NET.Models
{
    public class VariationsResponse
    {
        [JsonProperty("variations")]
        public List<ConstructorVariation> Variations { get; set; }

        [JsonProperty("total_count")]
        public string TotalCount { get; set; }
    }
}