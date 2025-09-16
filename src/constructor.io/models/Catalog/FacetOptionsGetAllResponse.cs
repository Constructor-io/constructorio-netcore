using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

/**
 * Constructor.io FacetOptionsGetAll Response
 */
namespace Constructorio_NET.Models
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class FacetOptionsGetAllResponse
    {
        public List<FacetOption> FacetOptions { get; set; }

        public int TotalCount { get; set; }
    }
}