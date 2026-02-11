using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Response object for v2 searchabilities operations (GET, PATCH, DELETE).
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SearchabilitiesV2Response
    {
        /// <summary>
        /// Gets or sets the list of searchability configurations.
        /// </summary>
        [JsonProperty("searchabilities")]
        public List<SearchabilityV2> Searchabilities { get; set; }

        /// <summary>
        /// Gets or sets the total count of searchability configurations.
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
    }
}
