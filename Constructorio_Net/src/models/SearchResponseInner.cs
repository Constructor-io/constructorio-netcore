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
        public List<FilterFacet> facets { get; set; }

        [JsonPropertyName("groups")]
        public List<FilterGroup> groups { get; set; }

        [JsonPropertyName("results")]
        public List<Result> results { get; set; }

        [JsonPropertyName("total_num_results")]
        public Integer totalNumberOfResults { get; set; }

        [JsonPropertyName("sort_options")]
        public List<FilterSortOption> { get; set; }

        [JsonPropertyName("redirect")]
        public Redirect redirect { get; set; }

    }
}