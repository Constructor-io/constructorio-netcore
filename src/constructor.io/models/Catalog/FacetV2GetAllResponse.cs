using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Response object for retrieving all v2 facet configurations.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class FacetV2GetAllResponse
    {
        /// <summary>
        /// Gets or sets the list of facet configurations.
        /// </summary>
        [JsonProperty("facets")]
        public List<FacetV2> Facets { get; set; }

        /// <summary>
        /// Gets or sets the total count of facet configurations.
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
    }
}
