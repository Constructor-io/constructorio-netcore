using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Retrieve Searchabilities Request Class.
    /// </summary>
    public class RetrieveSearchabilitiesRequest
    {
        /// <summary>
        /// Gets or sets name of searchability field to retrieve.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the page of searchability results to retreive. Can't be used with Offset.
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
        /// Gets or sets the number of results per page to retrieve.
        /// </summary>
        public int NumResultsPerPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to retrieve only results which are either exact_searchable or fuzzy_searchable.
        /// </summary>
        public bool Searchable { get; set; }

        /// <summary>
        /// Gets or sets a the criteria by which searchability results should be sorted. Valid criteria is "name".
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// Gets or sets a the order by which searchability results should be sorted. Valid criteria is "descending" or "ascending".
        /// </summary>
        public string SortOrder { get; set; }

        /// <summary>
        /// Gets or sets filters used to refine results. Valid keys are "name", "exact_searchable", or "fuzzy_searchable".
        /// </summary>
        public Dictionary<string, string> Filters { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="RetrieveSearchabilitiesRequest"/> class.
        /// </summary>
        public RetrieveSearchabilitiesRequest()
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
                if (this.Filters != null)
                {
                    Filters.Add("name", this.Name);
                }
                else
                {
                    Filters = new Dictionary<string, string> { { "name", this.Name } };
                }
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

            if (this.Filters != null)
            {
                Dictionary<string, List<string>> formattedFilters = new Dictionary<string, List<string>>();

                foreach (var filter in this.Filters)
                {
                    formattedFilters.Add(filter.Key, new List<string> { filter.Value });
                }

                parameters.Add(Constants.FILTERS, formattedFilters);
            }

            if (this.Searchable)
            {
                parameters.Add(Constants.SEARCHABLE, this.Searchable);
            }

            if (!string.IsNullOrEmpty(this.SortBy))
            {
                parameters.Add(Constants.SORT_BY, this.SortBy);
            }

            if (!string.IsNullOrEmpty(this.Section))
            {
                this.Section = "Products";
            }

            if (!string.IsNullOrEmpty(this.SortOrder))
            {
                parameters.Add(Constants.SORT_ORDER, this.SortOrder);
            }

            parameters.Add(Constants.SECTION, this.Section);

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
