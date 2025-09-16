using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Searchabilities Response
 */
namespace Constructorio_NET.Models
{
    public class SearchabilitiesResponse
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("searchabilities")]
        public List<Searchability> Searchabilities { get; set; }
    }
}