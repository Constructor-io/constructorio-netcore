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
    List<FilterFacet> facets;

    [JsonPropertyName("groups")]
    private List<FilterGroup> groups;

    [JsonPropertyName("results")]
    private List<Result> results;

    [JsonPropertyName("total_num_results")]
    private Integer totalNumberOfResults;

    [JsonPropertyName("sort_options")]
    private List<FilterSortOption> sortOptions;

    /**
     * @return the facets
     */
    public List<FilterFacet> getFacets() {
      return facets;
    }

    /**
     * @return the groups
     */
    public List<FilterGroup> getGroups() {
      return groups;
    }

    /**
     * @return the results
     */
    public List<Result> getResults() {
      return results;
    }

    /**
     * @return the totalNumberOfResults
     */
    public Integer getTotalNumberOfResults() {
      return totalNumberOfResults;
    }

    /**
     * @return the sort options
     */
    public List<FilterSortOption> getSortOptions() {
      return sortOptions;
    }
}