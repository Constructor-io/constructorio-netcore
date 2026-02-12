using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Browse Facet Option Request Class.
    /// </summary>
    public class BrowseFacetOptionsRequest
    {
        public string FacetName { get; set; }
        public bool ShowHiddenFacets { get; set; }
        public bool ShowProtectedFacets { get; set; }
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseFacetOptionsRequest"/> class.
        /// </summary>
        /// <param name="facetName">Facet to use for the request.</param>
        public BrowseFacetOptionsRequest(string facetName)
        {
            this.FacetName = facetName;
        }

        /// <summary>
        /// Get request parameters.
        /// </summary>
        /// <returns>Hashtable of request parameters.</returns>
        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();

            if (!string.IsNullOrEmpty(this.FacetName))
            {
                parameters.Add(Constants.FACET_NAME, this.FacetName);
            }

            FmtOptions fmtOptions = null;
            if (this.ShowHiddenFacets)
            {
                fmtOptions ??= new FmtOptions();
                fmtOptions.ShowHiddenFacets = this.ShowHiddenFacets;
            }

            if (this.ShowProtectedFacets)
            {
                fmtOptions ??= new FmtOptions();
                fmtOptions.ShowProtectedFacets = this.ShowProtectedFacets;
            }

            if (fmtOptions != null)
            {
                parameters.Add(Constants.FMT_OPTIONS, fmtOptions);
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
