using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Item Facet
 */
namespace Constructorio_NET.Models
{
    public class ResultFacet {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("values")]
        public List<string> Values { get; set; }
    }
}