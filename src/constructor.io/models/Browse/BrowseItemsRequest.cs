using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;
using Newtonsoft.Json;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Browse Items Request Class.
    /// </summary>
    public class BrowseItemsRequest
    {
        public List<string> ItemIds { get; set; }
        public Dictionary<string, List<string>> Filters { get; set; }
        public Dictionary<string, string> FmtOptions { get; set; }
        public List<string> HiddenFields { get; set; }
        public List<string> HiddenFacets { get; set; }
        public int Offset { get; set; }
        public int Page { get; set; }
        public int ResultsPerPage { get; set; }
        public string Section { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public Dictionary<string, string> TestCells { get; set; }
        public UserInfo UserInfo { get; set; }
        public VariationsMap VariationsMap { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseItemsRequest"/> class.
        /// </summary>
        /// <param name="itemIds">List of item IDs to use for the request.</param>
        public BrowseItemsRequest(List<string> itemIds)
        {
            if (itemIds == null || itemIds.Count == 0)
            {
                throw new ArgumentException("itemIds is a required parameters");
            }

            this.ItemIds = itemIds;
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

            if (this.ItemIds != null)
            {
                parameters.Add(Constants.ITEM_IDS, this.ItemIds);
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
