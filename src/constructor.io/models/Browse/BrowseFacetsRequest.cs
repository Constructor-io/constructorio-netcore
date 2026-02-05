using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Browse Facets Request Class.
    /// </summary>
    public class BrowseFacetsRequest
    {
        public int Page { get; set; }
        public int ResultsPerPage { get; set; }
        public int Offset { get; set; }
        public bool ShowHiddenFacets { get; set; }
        public bool ShowProtectedFacets { get; set; }
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseFacetsRequest"/> class.
        /// </summary>
        public BrowseFacetsRequest()
        {
        }

        /// <summary>
        /// Get request parameters.
        /// </summary>
        /// <returns>Hashtable of request parameters.</returns>
        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();

            if (this.Page != 0)
            {
                parameters.Add(Constants.PAGE, this.Page);
            }

            if (this.Offset != 0)
            {
                parameters.Add(Constants.OFFSET, this.Offset);
            }

            if (this.ResultsPerPage != 0)
            {
                parameters.Add(Constants.RESULTS_PER_PAGE, this.ResultsPerPage);
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
