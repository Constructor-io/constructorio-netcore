using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /**
     * Constructor.io Search Request
     */
    public class SearchRequest
    {
        /// <value>
        /// Client ID, utilized to personalize results
        /// </value>
        public string ClientId { get; set; }

        /// <value>
        /// Filters used to refine results
        /// </value>
        public Dictionary<string, List<string>> Filters { get; set; }

        /// <value>
        /// The format options used to refine result groups
        /// </value>
        public Dictionary<string, string> FmtOptions { get; set; }

        /// <value>
        /// Hidden metadata fields to return
        /// </value>
        public List<string> HiddenFields { get; set; }

        /// <value>
        /// The page number of the results
        /// </value>
        public int Page { get; set; }

        /// <value>
        /// The term to search for
        /// </value>
        public string Query { get; set; }

        /// <value>
        /// The number of results per page to return
        /// </value>
        public int ResultsPerPage { get; set; }

        /// <value>
        /// The name of the section 
        /// </value>
        public string Section { get; set; }

        /// <value>
        /// Constructor security token
        /// </value>
        public string SecurityToken { get; set; }

        /// <value>
        /// User segments
        /// </value>
        public List<string> Segments { get; set; }

        /// <value>
        /// Session ID, utilized to personalize results
        /// </value>
        public int SessionId { get; set; }

        /// <value>
        /// The sort method for results
        /// </value>
        public string SortBy { get; set; }

        /// <value>
        /// The sort order for results
        /// </value>
        public string SortOrder { get; set; }

        /// <value>
        /// User test cells
        /// </value>
        public Dictionary<string, string> TestCells { get; set; }

        /// <value>
        /// Origin user agent, from client
        /// </value>
        public string UserAgent { get; set; }

        /// <value>
        /// User ID, utilized to personalize results
        /// </value>
        public string UserId { get; set; }

        /// <value>
        /// Origin user IP, from client
        /// </value>
        public string UserIp { get; set; }

        /// <summary>
        /// Creates a search request
        /// </summary>
        /// <param name="query">The term used to query against</param>
        public SearchRequest(string query)
        {
            if (query == null)
            {
                throw new ArgumentException("query is required");
            }

            this.Query = query;
        }

        public Hashtable GetUrlParameters()
        {
            Hashtable parameters = new Hashtable();

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

            if (this.UserId != null)
            {
                parameters.Add(Constants.USER_ID, this.UserId);
            }

            return parameters;
        }

        public Dictionary<string, string> GetRequestHeaders()
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            if (this.UserIp != null)
            {
                requestHeaders.Add(Constants.USER_IP, this.UserIp);
            }

            if (this.UserAgent != null)
            {
                requestHeaders.Add(Constants.USER_AGENT, this.UserAgent);
            }

            if (this.SecurityToken != null)
            {
                requestHeaders.Add(Constants.SECURITY_TOKEN, this.SecurityToken);
            }

            return requestHeaders;
        }
    }
}
