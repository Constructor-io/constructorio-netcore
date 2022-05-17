using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Item Facet
 */
namespace Constructorio_NET
{
    public class ResultFacet {

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("values")]
        public List<String> Values { get; set; }
    }
}