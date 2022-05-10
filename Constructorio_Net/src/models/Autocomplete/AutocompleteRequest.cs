using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Constructorio_NET
{
    /**
     * Constructor.io Autocomplete Request
     */
    public class AutocompleteRequest
    {
        public string Query { get; set; }
        public Dictionary<string, int> ResultsPerSection { get; set; }
        public Dictionary<string, List<string>> Filters { get; set; }
        public List<string> HiddenFields { get; set; }
        public string SecurityToken { get; set; }
        public Dictionary<string, string> TestCells { get; set; }
        public UserInfo UserInfo { get; set; }
        public VariationsMap VariationMap { get; set; }

        /// <summary>
        /// Creates a autocomplete request
        /// </summary>
        /// <param name="Query"></param>
        public AutocompleteRequest(string Query)
        {
            if (Query == null)
            {
                throw new ArgumentException("Query is required");
            }
            this.Query = Query;
        }

        public Hashtable GetUrlParameters()
        {
            Hashtable parameters = new Hashtable();
            if (this.UserInfo != null)
            {
                if (this.UserInfo.getUserId() != null)
                {
                    parameters.Add(Constants.USER_ID, this.UserInfo.getUserId());
                }
                if (this.UserInfo.getClientId() != null)
                {
                    parameters.Add(Constants.CLIENT_ID, this.UserInfo.getClientId());
                }
                if (this.UserInfo.getSessionId() != 0)
                {
                    parameters.Add(Constants.SESSION_ID, this.UserInfo.getSessionId());
                }
                if (this.UserInfo.getUserSegments() != null)
                {
                    parameters.Add(Constants.SEGMENTS, this.UserInfo.getUserSegments());
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
            if (this.VariationMap != null && this.VariationMap.GroupBy.Count > 0 && this.VariationMap.Values.Count > 0)
            {
                string serializedJson = JsonConvert.SerializeObject(this.VariationMap);
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

        public Dictionary<string, string> GetRequestHeaders()
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            if (this.UserInfo != null)
            {
                if (this.UserInfo.getForwardedFor() != null)
                {
                    requestHeaders.Add(Constants.USER_IP, this.UserInfo.getForwardedFor());
                }
                if (this.UserInfo.getUserAgent() != null)
                {
                    requestHeaders.Add(Constants.USER_AGENT, this.UserInfo.getUserAgent());
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
