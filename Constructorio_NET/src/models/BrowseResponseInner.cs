using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Browse Response Inner
 **/
namespace Constructorio_NET
{
    public class BrowseResponseInner {

        [JsonProperty("facets")]
        List<FilterFacet> Facets { get; set; }

        [JsonProperty("groups")]
        public List<FilterGroup> Groups { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("total_num_results")]
        public Int32 TotalNumberOfResults { get; set; }

        [JsonProperty("sort_options")]
        public List<FilterSortOption> SortOptions { get; set; }

    }

}