using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Browse Response Inner
 **/
namespace Constructorio_NET
{
    public class BrowseResponseInner {

        [JsonPropertyName("facets")]
        List<FilterFacet> Facets { get; set; }

        [JsonPropertyName("groups")]
        public List<FilterGroup> Groups { get; set; }

        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }

        [JsonPropertyName("total_num_results")]
        public Int32 TotalNumberOfResults { get; set; }

        [JsonPropertyName("sort_options")]
        public List<FilterSortOption> SortOptions { get; set; }

    }

}