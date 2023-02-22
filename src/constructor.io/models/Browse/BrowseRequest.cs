using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Browse Request Class.
    /// </summary>
    public class BrowseRequest
    {
        /// <summary>
        /// Gets or sets the filter name used to refine results.
        /// </summary>
        public string FilterName { get; set; }

        /// <summary>
        /// Gets or sets the filter value used to refine results.
        /// </summary>
        public string FilterValue { get; set; }

        /// <summary>
        /// Gets or sets the format options used to refine result groups.
        /// </summary>
        public Dictionary<string, string> FmtOptions { get; set; }

        /// <summary>
        /// Gets or sets the faceting expression used to scope search results.
        /// </summary>
        public JObject PreFilterExpression { get; set; }

        /// <summary>
        /// Gets or sets hidden metadata fields to return.
        /// </summary>
        public List<string> HiddenFields { get; set; }

        /// <summary>
        /// Gets or sets the page number of the results.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the number of results per page to return.
        /// </summary>
        public int ResultsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the sort method for results.
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// Gets or sets the sort order for results.
        /// </summary>
        public string SortOrder { get; set; }

        /// <summary>
        /// Gets or sets filters used to refine results.
        /// </summary>
        public Dictionary<string, List<string>> Filters { get; set; }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Gets or sets user test cells.
        /// </summary>
        public Dictionary<string, string> TestCells { get; set; }

        /// <summary>
        /// Gets or sets collection of user related data.
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// Gets or sets how to return variation data.
        /// </summary>
        public VariationsMap VariationsMap { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseRequest"/> class.
        /// </summary>
        /// <param name="filterName">filter name to use for the request.</param>
        /// <param name="filterValue">filter value to use for the request.</param>
        public BrowseRequest(string filterName, string filterValue)
        {
            if (filterName == null || filterValue == null)
            {
                throw new ArgumentException("filterName and filterValue are required parameters");
            }

            this.FilterName = filterName;
            this.FilterValue = filterValue;
        }

        /// <summary>
        /// Get request parameters.
        /// </summary>
        /// <returns>Hashtable of request parameters.</returns>
        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();
            if (this.UserInfo != null)
            {
                if (this.UserInfo.GetUserId() != null)
                {
                    parameters.Add(Constants.USER_ID, this.UserInfo.GetUserId());
                }

                if (this.UserInfo.GetClientId() != null)
                {
                    parameters.Add(Constants.CLIENT_ID, this.UserInfo.GetClientId());
                }

                if (this.UserInfo.GetSessionId() != 0)
                {
                    parameters.Add(Constants.SESSION_ID, this.UserInfo.GetSessionId());
                }

                if (this.UserInfo.GetUserSegments() != null)
                {
                    parameters.Add(Constants.USER_SEGMENTS, this.UserInfo.GetUserSegments());
                }
            }

            if (this.Filters != null)
            {
                parameters.Add(Constants.FILTERS, this.Filters);
            }

            if (this.FmtOptions != null)
            {
                parameters.Add(Constants.FMT_OPTIONS, this.FmtOptions);
            }

            if (this.PreFilterExpression != null)
            {
                string preFilterJson = JsonConvert.SerializeObject(this.PreFilterExpression);
                parameters.Add(Constants.PRE_FILTER_EXPRESSION, preFilterJson);
            }

            if (this.HiddenFields != null)
            {
                parameters.Add(Constants.HIDDEN_FIELDS, this.HiddenFields);
            }

            if (this.Page != 0)
            {
                parameters.Add(Constants.PAGE, this.Page);
            }

            if (this.ResultsPerPage != 0)
            {
                parameters.Add(Constants.RESULTS_PER_PAGE, this.ResultsPerPage);
            }

            if (this.Section != null)
            {
                parameters.Add(Constants.SECTION, this.Section);
            }

            if (this.SortBy != null)
            {
                parameters.Add(Constants.SORT_BY, this.SortBy);
            }

            if (this.SortOrder != null)
            {
                parameters.Add(Constants.SORT_ORDER, this.SortOrder);
            }

            if (this.TestCells != null)
            {
                parameters.Add(Constants.TEST_CELLS, this.TestCells);
            }

            if (this.VariationsMap != null && this.VariationsMap.Values.Count > 0)
            {
                string serializedJson = JsonConvert.SerializeObject(this.VariationsMap);
                parameters.Add(Constants.VARIATIONS_MAP, serializedJson);
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

            if (this.UserInfo != null)
            {
                if (this.UserInfo.GetForwardedFor() != null)
                {
                    requestHeaders.Add(Constants.USER_IP, this.UserInfo.GetForwardedFor());
                }

                if (this.UserInfo.GetUserAgent() != null)
                {
                    requestHeaders.Add(Constants.USER_AGENT, this.UserInfo.GetUserAgent());
                }
            }

            return requestHeaders;
        }
    }
}
