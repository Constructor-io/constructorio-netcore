using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary
    /// Constructor.io Search Request Class.
    /// </summary
    public class SearchRequest
    {
        /// <summary>
        /// Gets or sets client ID, utilized to personalize results.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets filters used to refine results.
        /// </summary>
        public Dictionary<string, List<string>> Filters { get; set; }

        /// <summary>
        /// Gets or sets the format options used to refine result groups.
        /// </summary>
        public Dictionary<string, string> FmtOptions { get; set; }

        /// <summary>
        /// Gets or sets hidden metadata fields to return.
        /// </summary>
        public List<string> HiddenFields { get; set; }

        /// <summary>
        /// Gets or sets the page number of the results.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the term to search for.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the number of results per page to return.
        /// </summary>
        public int ResultsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Gets or sets constructor security token.
        /// </summary>
        public string SecurityToken { get; set; }

        /// <summary>
        /// Gets or sets user segments.
        /// </summary>
        public List<string> Segments { get; set; }

        /// <summary>
        /// Gets or sets session ID, utilized to personalize results.
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// Gets or sets the sort method for results.
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// Gets or sets the sort order for results.
        /// </summary>
        public string SortOrder { get; set; }

        /// <summary>
        /// Gets or sets user test cells.
        /// </summary>
        public Dictionary<string, string> TestCells { get; set; }

        /// <summary>
        /// Gets or sets collection of user related data.
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchRequest"/> class.
        /// Creates an search request.
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

            if (this.ClientId != null)
            {
                parameters.Add(Constants.CLIENT_ID, this.ClientId);
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

            if (this.Segments != null)
            {
                parameters.Add(Constants.SEGMENTS, this.Segments);
            }

            if (this.SessionId != 0)
            {
                parameters.Add(Constants.SESSION_ID, this.SessionId);
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
