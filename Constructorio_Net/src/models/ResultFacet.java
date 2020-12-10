using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Item Facet
 */
*/
namespace Constructorio_NET
{
    public class ResultFacet {

        [JsonPropertyName("name")]
        public String name;

        [JsonPropertyName("values")]
        public List<String> values;

    }
}