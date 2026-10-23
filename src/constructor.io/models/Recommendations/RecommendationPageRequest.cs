using System;
using System.Collections;
using System.Collections.Generic;

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
        /// Gets or sets variation ids. Requires <see cref="ItemIds"/>.
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

            RecommendationsRequestParameters.AddSharedParameters(parameters, this.UserInfo, this.Section, this.TestCells, this.ItemIds, this.VariationIds, this.Term);

            this.AddOverridableParameters(parameters, string.Empty);

            if (this.PodOverrides != null)
            {
                foreach (var podOverride in this.PodOverrides)
                {
                    if (podOverride.Value is RecommendationPageRequest)
                    {
                        throw new ArgumentException($"PodOverrides.{podOverride.Key} must be a RecommendationPagePodOverride; page-wide values can't be set per pod");
                    }

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
            return RecommendationsRequestParameters.GetRequestHeaders(this.UserInfo);
        }
    }
}
