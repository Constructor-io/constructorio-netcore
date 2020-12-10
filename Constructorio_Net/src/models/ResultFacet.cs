using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Item Facet
 */
namespace Constructorio_NET
{
    public class ResultFacet {

        [JsonPropertyName("name")]
        public String Name { get; set; }

        [JsonPropertyName("values")]
        public List<String> Values { get; set; }

    }
}