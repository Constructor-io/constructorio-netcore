﻿using System;
using System.Collections.Generic;
using Constructorio_NET.Models;
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
        public int TotalCount { get; set; }
    }
}