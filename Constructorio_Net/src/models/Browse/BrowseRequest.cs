﻿using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Constructorio_NET
{
    /**
     * Constructor.io Browse Request
     */
    public class BrowseRequest
    {
        public string filterName { get; set; }
        public string filterValue { get; set; }
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
        /// Creates a browse request
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="filterValue"></param>
        public BrowseRequest(string filterName, string filterValue)
        {
            if (filterName == null || filterValue == null)
            {
                throw new ArgumentException("filterName and filterValue are required parameters");
            }

            this.filterName = filterName;
            this.filterValue = filterValue;
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
