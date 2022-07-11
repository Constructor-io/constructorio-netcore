using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;
using Newtonsoft.Json;

namespace Constructorio_NET.Models
{
    /// <summary
    /// Constructor.io Browse Request Class.
    /// </summary
    public class BrowseRequest
    {
        public string FilterName { get; set; }
        public string FilterValue { get; set; }
        public Dictionary<string, List<string>> Filters { get; set; }
        public Dictionary<string, string> FmtOptions { get; set; }
        public List<string> HiddenFields { get; set; }
        public int Page { get; set; }
        public int ResultsPerPage { get; set; }
        public string Section { get; set; }
        public string SecurityToken { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public Dictionary<string, string> TestCells { get; set; }
        public UserInfo UserInfo { get; set; }
        public VariationsMap VariationMap { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseRequest"/> class.
        /// Creates an browse request.
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
        public Hashtable GetUrlParameters()
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
                    parameters.Add(Constants.SEGMENTS, this.UserInfo.GetUserSegments());
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

            if (this.VariationMap != null && this.VariationMap.GroupBy.Count > 0 && this.VariationMap.Values.Count > 0)
            {
                string serializedJson = JsonConvert.SerializeObject(this.VariationMap);
                parameters.Add(Constants.VARIATIONS_MAP, serializedJson);
            }

            return parameters;
        }

        /// <summary>
        /// Get request headers.
        /// </summary>
        /// <returns>Hashtable of request headers.</returns>
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

            if (this.SecurityToken != null)
            {
                requestHeaders.Add(Constants.SECURITY_TOKEN, this.SecurityToken);
            }

            return requestHeaders;
        }
    }
}
