using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /**
     * Constructor.io Browse Facet Options Request
     */
    public class BrowseFacetOptionsRequest
    {
        public string FacetName { get; set; }
        public bool ShowHiddenFacets { get; set; }
        public bool ShowProtectedFacets { get; set; }
        public UserInfo UserInfo { get; set; }
        private Dictionary<string, string> FmtOptions { get; set; }

        /// <summary>
        /// Creates a browse facet options request
        /// </summary>
        /// <param name="facetName"></param>
        public BrowseFacetOptionsRequest(string facetName)
        {
            FmtOptions = new Dictionary<string, string>();
            this.FacetName = facetName;
        }

        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();

            if (!string.IsNullOrEmpty(this.FacetName))
            {
                parameters.Add(Constants.FACET_NAME, this.FacetName);
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
