using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Recommendation Page Request Class.
    /// Page-wide values apply to every pod on the page; use <see cref="PodOverrides"/> to give one pod its own values.
    /// </summary>
    public class RecommendationPageRequest : RecommendationPagePodOverride
    {
        /// <summary>
        /// Gets or sets page id.
        /// </summary>
        public string PageId { get; set; }

        /// <summary>
        /// Gets or sets item ids.
        /// </summary>
        public List<string> ItemIds { get; set; }

        /// <summary>
        /// Gets or sets variation ids.
        /// </summary>
        public List<string> VariationIds { get; set; }

        /// <summary>
        /// Gets or sets term.
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Gets or sets user test cells.
        /// </summary>
        public Dictionary<string, string> TestCells { get; set; }

        /// <summary>
        /// Gets or sets collection of user related data.
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// Gets or sets per-pod values keyed by pod id. Each replaces (is not merged with) the page-wide value for that pod.
        /// </summary>
        public Dictionary<string, RecommendationPagePodOverride> PodOverrides { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationPageRequest"/> class.
        /// </summary>
        /// <param name="pageId">Page id to use for the request.</param>
        public RecommendationPageRequest(string pageId)
        {
            if (pageId == null)
            {
                throw new ArgumentException("PageId is required");
            }

            this.PageId = pageId;
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

            if (this.Section != null)
            {
                parameters.Add(Constants.SECTION, this.Section);
            }

            if (this.TestCells != null)
            {
                parameters.Add(Constants.TEST_CELLS, this.TestCells);
            }

            if (this.ItemIds != null && this.ItemIds.Count != 0)
            {
                parameters.Add("item_id", this.ItemIds);
            }

            if (this.VariationIds != null && this.VariationIds.Count != 0)
            {
                parameters.Add("variation_id", this.VariationIds);
            }

            if (this.Term != null)
            {
                parameters.Add("term", this.Term);
            }

            this.AddOverridableParameters(parameters, string.Empty);

            if (this.PodOverrides != null)
            {
                foreach (var podOverride in this.PodOverrides)
                {
                    podOverride.Value?.AddOverridableParameters(parameters, $"pod_overrides[{podOverride.Key}]");
                }
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
