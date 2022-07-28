using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /**
     * Constructor.io Browse Facets Request
     */
    public class BrowseFacetsRequest
    {
        public int Page { get; set; }
        public int ResultsPerPage { get; set; }
        public bool ShowHiddenFacets { get; set; }
        public bool ShowProtectedFacets { get; set; }
        public UserInfo UserInfo { get; set; }
        private Dictionary<string, string> FmtOptions { get; set; }

        /// <summary>
        /// Creates a browse facets request
        /// </summary>
        public BrowseFacetsRequest()
        {
            FmtOptions = new Dictionary<string, string>();
        }

        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();

            if (this.Page != 0)
            {
                parameters.Add(Constants.PAGE, this.Page);
            }

            if (this.ResultsPerPage != 0)
            {
                parameters.Add(Constants.RESULTS_PER_PAGE, this.ResultsPerPage);
            }

            if (this.ShowHiddenFacets)
            {
                this.FmtOptions.Add(Constants.SHOW_HIDDEN_FACETS, this.ShowHiddenFacets.ToString());
            }

            if (this.ShowProtectedFacets)
            {
                this.FmtOptions.Add(Constants.SHOW_PROTECTED_FACETS, this.ShowProtectedFacets.ToString());
            }

            if (this.FmtOptions != null && this.FmtOptions.Count != 0)
            {
                parameters.Add(Constants.FMT_OPTIONS, this.FmtOptions);
            }

            return parameters;
        }

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
