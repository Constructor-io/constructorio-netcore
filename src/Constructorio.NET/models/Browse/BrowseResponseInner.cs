using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Browse Response Inner
 **/
namespace Constructorio_NET.Models
{
    public class BrowseResponseInner
    {
        [JsonProperty("collection")]
        public CollectionResponse Collection { get; set; }

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

        [JsonProperty("refined_content")]
        public List<RefinedContent> RefinedContent { get; set; }

        [JsonProperty("result_sources")]
        public ResultSources ResultSources { get; set; }
    }
}
