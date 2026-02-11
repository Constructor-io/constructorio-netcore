using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io v2 Retrieve Searchabilities Request Class.
    /// </summary>
    public class RetrieveSearchabilitiesV2Request
    {
        /// <summary>
        /// Gets or sets name of searchability field to filter for.
        /// Supports exact matching (name=value) and substring matching (name=*value, value* or *value*).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the page of searchability results to retrieve. Can't be used with Offset.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the offset of results to skip. Can't be used with Page.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets the name of the section. Defaults to "Products".
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Gets or sets the number of results per page to retrieve. Default: 20, Max: 1000.
        /// </summary>
        public int NumResultsPerPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to filter by fuzzy_searchable.
        /// </summary>
        public bool? FuzzySearchable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to filter by exact_searchable.
        /// </summary>
        public bool? ExactSearchable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to filter by displayable.
        /// </summary>
        public bool? Displayable { get; set; }

        /// <summary>
        /// Gets or sets the match type for filters. Valid values are "and" or "or". Default is "and".
        /// </summary>
        public string MatchType { get; set; }

        /// <summary>
        /// Gets or sets the criteria by which searchability results should be sorted. Valid value is "name".
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// Gets or sets the order by which searchability results should be sorted. Valid values are "descending" or "ascending". Default is "ascending".
        /// </summary>
        public string SortOrder { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetrieveSearchabilitiesV2Request"/> class.
        /// </summary>
        public RetrieveSearchabilitiesV2Request()
        {
        }

        /// <summary>
        /// Get request parameters.
        /// </summary>
        /// <returns>Hashtable of request parameters.</returns>
        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();

            if (!string.IsNullOrEmpty(this.Name))
            {
                parameters.Add("name", this.Name);
            }

            if (this.Page > 0)
            {
                parameters.Add(Constants.PAGE, this.Page);
            }

            if (this.Offset > 0)
            {
                parameters.Add(Constants.OFFSET, this.Offset);
            }

            if (this.NumResultsPerPage > 0)
            {
                parameters.Add(Constants.RESULTS_PER_PAGE, this.NumResultsPerPage);
            }

            if (this.FuzzySearchable.HasValue)
            {
                parameters.Add("fuzzy_searchable", this.FuzzySearchable.Value.ToString().ToLower());
            }

            if (this.ExactSearchable.HasValue)
            {
                parameters.Add("exact_searchable", this.ExactSearchable.Value.ToString().ToLower());
            }

            if (this.Displayable.HasValue)
            {
                parameters.Add("displayable", this.Displayable.Value.ToString().ToLower());
            }

            if (!string.IsNullOrEmpty(this.MatchType))
            {
                parameters.Add("match_type", this.MatchType);
            }

            if (!string.IsNullOrEmpty(this.SortBy))
            {
                parameters.Add(Constants.SORT_BY, this.SortBy);
            }

            if (!string.IsNullOrEmpty(this.SortOrder))
            {
                parameters.Add(Constants.SORT_ORDER, this.SortOrder);
            }

            return parameters;
        }

        /// <summary>
        /// Get request headers.
        /// </summary>
        /// <returns>Dictionary of request headers.</returns>
        public Dictionary<string, string> GetRequestHeaders()
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            return requestHeaders;
        }
    }
}
