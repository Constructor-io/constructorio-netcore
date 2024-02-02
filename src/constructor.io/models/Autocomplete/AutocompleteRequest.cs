using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;
using Newtonsoft.Json;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Autocomplete Request Class.
    /// </summary>
    public class AutocompleteRequest : IFilterable, IUserDetails
    {
        /// <summary>
        /// Gets or sets the query used to refine results.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets how to many results to return and what sections.
        /// </summary>
        public Dictionary<string, int> ResultsPerSection { get; set; }

        /// <summary>
        /// Gets or sets filters used to refine results.
        /// </summary>
        public Dictionary<string, List<string>> Filters { get; set; }

        /// <summary>
        /// Gets or sets hidden metadata fields to return.
        /// </summary>
        public List<string> HiddenFields { get; set; }

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
        /// Initializes a new instance of the <see cref="AutocompleteRequest"/> class.
        /// </summary>
        /// <param name="query">Query to use for the request.</param>
        public AutocompleteRequest(string query)
        {
            if (query == null)
            {
                throw new ArgumentException("query is required");
            }

            this.Query = query;
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

            if (this.TestCells != null)
            {
                parameters.Add(Constants.TEST_CELLS, this.TestCells);
            }

            if (this.VariationsMap != null && this.VariationsMap.Values.Count > 0)
            {
                string serializedJson = JsonConvert.SerializeObject(this.VariationsMap);
                parameters.Add(Constants.VARIATIONS_MAP, serializedJson);
            }

            if (this.ResultsPerSection != null)
            {
                foreach (KeyValuePair<string, int> keyValue in this.ResultsPerSection)
                {
                    string section = keyValue.Key;
                    int numResults = keyValue.Value;
                    parameters.Add("num_results_" + section, numResults);
                }
            }

            if (this.HiddenFields != null)
            {
                parameters.Add(Constants.HIDDEN_FIELDS, this.HiddenFields);
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
