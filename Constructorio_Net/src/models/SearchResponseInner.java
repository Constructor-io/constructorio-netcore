using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Search Response Inner
 **/
namespace Constructorio_NET
{
    public class SearchResponseInner {

        [JsonPropertyName("facets")]
        private List<FilterFacet> facets { get; set; }

        [JsonPropertyName("groups")]
        private List<FilterGroup> groups { get; set; }

        [JsonPropertyName("results")]
        private List<Result> results { get; set; }

        [JsonPropertyName("total_num_results")]
        private Integer totalNumberOfResults { get; set; }

        [JsonPropertyName("sort_options")]
        private List<FilterSortOption> { get; set; }

        [JsonPropertyName("redirect")]
        private Redirect redirect { get; set; }

    }
}