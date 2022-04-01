using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Search Response Inner
 */
namespace Constructorio_NET
{
    public class SearchResponseInner {

        [JsonPropertyName("facets")]
        public List<FilterFacet> Facets { get; set; }

        [JsonPropertyName("groups")]
        public List<FilterGroup> Groups { get; set; }

        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }

        [JsonPropertyName("total_num_results")]
        public Int32 TotalNumberOfResults { get; set; }

        [JsonPropertyName("sort_options")]
        public List<FilterSortOption> SortOptions { get; set; }

        [JsonPropertyName("redirect")]
        public Redirect Redirect { get; set; }

    }
}