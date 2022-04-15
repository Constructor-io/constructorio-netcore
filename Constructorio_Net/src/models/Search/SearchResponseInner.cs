using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Search Response Inner
 */
namespace Constructorio_NET
{
    public class SearchResponseInner {
        [JsonProperty("facets")]
        public List<FilterFacet> Facets { get; set; }

        [JsonProperty("groups")]
        public List<FilterGroup> Groups { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("total_num_results")]
        public int TotalNumResults { get; set; }

        [JsonProperty("sort_options")]
        public List<FilterSortOption> SortOptions { get; set; }

        [JsonProperty("redirect")]
        public Redirect Redirect { get; set; }
    }
}