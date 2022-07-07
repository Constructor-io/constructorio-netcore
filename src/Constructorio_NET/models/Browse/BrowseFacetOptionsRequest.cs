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
        public bool showHiddenFacets { get; set; }
        public bool showProtectedFacets { get; set; }
        public string SecurityToken { get; set; }
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

        public Hashtable GetUrlParameters()
        {
            Hashtable parameters = new Hashtable();

            if (!string.IsNullOrEmpty(this.FacetName))
            {
                parameters.Add(Constants.FACET_NAME, this.FacetName);
            }

            if (this.showHiddenFacets)
            {
                this.FmtOptions.Add(Constants.SHOW_HIDDEN_FACETS, this.showHiddenFacets.ToString());
            }

            if (this.showProtectedFacets)
            {
                this.FmtOptions.Add(Constants.SHOW_PROTECTED_FACETS, this.showProtectedFacets.ToString());
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
