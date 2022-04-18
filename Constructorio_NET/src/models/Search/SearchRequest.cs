using System;
using System.Collections;
using System.Collections.Generic;

namespace Constructorio_NET
{
    /**
     * Constructor.io Search Request
     */
    public class SearchRequest
    {
        public string ClientId { get; set; }
        public Dictionary<string, List<string>> Filters { get; set; }
        public Dictionary<string, string> FmtOptions { get; set; }
        public List<string> HiddenFields { get; set; }
        public int Page { get; set; }
        public string Query { get; set;}
        public int ResultsPerPage { get; set; }
        public string Section { get; set; }
        public List<string> Segments { get; set; }
        public int SessionId { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public Dictionary<string, string> TestCells { get; set; }
        public string UserId { get; set; }

        /// <summary>
        /// Creates a search request
        /// </summary>
        /// <param name="query"></param>
        public SearchRequest(string query)
        {
            if (query == null)
            {
                throw new ArgumentException("query is required");
            }

            this.Query = query;
        }
        public Hashtable getParameters()
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
    }
}