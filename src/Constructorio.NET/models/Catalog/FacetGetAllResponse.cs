using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

/**
 * Constructor.io FacetGetAll Response
 */
namespace Constructorio_NET.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class FacetGetAllResponse
    {
        public List<Facet> Facets { get; set; }

        public int TotalCount { get; set; }
    }
}