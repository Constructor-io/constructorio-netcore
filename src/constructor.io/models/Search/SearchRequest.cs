﻿using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Search Request Class.
    /// </summary>
    public class SearchRequest : IPlpRequest
    {
        /// <summary>
        /// Gets or sets the query used to refine results.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets filters used to refine results.
        /// </summary>
        public Dictionary<string, List<string>> Filters { get; set; }

        /// <summary>
        /// Gets or sets the format options used to refine result groups.
        /// </summary>
        public Dictionary<string, string> FmtOptions { get; set; }

        /// <summary>
        /// Gets or sets the filtering expression used to scope search results.
        /// </summary>
        public PreFilterExpression PreFilterExpression { get; set; }

        /// <summary>
        /// Gets or sets hidden metadata fields to return.
        /// </summary>
        public List<string> HiddenFields { get; set; }

        /// <summary>
        /// Gets or sets hidden facets fields to return.
        /// </summary>
        public List<string> HiddenFacets { get; set; }

        /// <summary>
        /// Gets or sets the number of results to skip from the beginning.
        /// Can't be used together with <see cref="Page"/>.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets the page number of the results to return.
        /// Can't be used together with <see cref="Offset"/>.
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
        /// Initializes a new instance of the <see cref="SearchRequest"/> class.
        /// </summary>
        /// <param name="query">Query to use for the request.</param>
        public SearchRequest(string query)
        {
            this.Query = query ?? throw new ArgumentException("query is required");
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

            if (this.PreFilterExpression != null)
            {
                string preFilterJson = this.PreFilterExpression.GetExpression();
                parameters.Add(Constants.PRE_FILTER_EXPRESSION, preFilterJson);
            }

            if (this.FmtOptions != null)
            {
                parameters.Add(Constants.FMT_OPTIONS, this.FmtOptions);
            }

            if (this.HiddenFields != null)
            {
                parameters.Add(Constants.HIDDEN_FIELDS, this.HiddenFields);
            }

            if (this.HiddenFacets != null)
            {
                parameters.Add(Constants.HIDDEN_FACETS, this.HiddenFacets);
            }

            if (this.Offset != 0)
            {
                parameters.Add(Constants.OFFSET, this.Offset);
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
