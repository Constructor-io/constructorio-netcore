﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

/**
 * Constructor.io Browse Facets Response Inner
 */
namespace Constructorio_NET.Models
{
    public class BrowseFacetsResponseInner
    {
        [JsonProperty("facets")]
        public List<FilterFacet> Facets { get; set; }

        [JsonProperty("total_num_results")]
        public int TotalNumResults { get; set; }
    }
}
