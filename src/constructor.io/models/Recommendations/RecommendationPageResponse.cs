using System.Collections.Generic;
using Newtonsoft.Json;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Recommendation Page Response.
    /// <see cref="ResultId"/> identifies the page request and is not a tracking id:
    /// send each pod's own <see cref="RecommendationPagePod.ResultId"/> with that pod's tracking events.
    /// </summary>
    public class RecommendationPageResponse
    {
        /// <summary>
        /// Gets or sets the page request's result id. Not a tracking id: use each pod's <see cref="RecommendationPagePod.ResultId"/>.
        /// </summary>
        [JsonProperty("result_id")]
        public string ResultId { get; set; }

        [JsonProperty("response")]
        public RecommendationPageResponseInner Response { get; set; }

        [JsonProperty("request")]
        public Dictionary<string, object> Request { get; set; }
    }

    /// <summary>
    /// Constructor.io Recommendation Page Response Inner.
    /// </summary>
    public class RecommendationPageResponseInner
    {
        [JsonProperty("page_id")]
        public string PageId { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("page_type")]
        public string PageType { get; set; }

        /// <summary>
        /// Gets or sets the pods, in the page's configured order.
        /// An item appears in at most one pod on the page.
        /// </summary>
        [JsonProperty("pods")]
        public List<RecommendationPagePod> Pods { get; set; }
    }

    /// <summary>
    /// One pod of a Constructor.io Recommendation Page Response.
    /// </summary>
    public class RecommendationPagePod
    {
        [JsonProperty("pod_id")]
        public string PodId { get; set; }

        /// <summary>
        /// Gets or sets the pod's effective request: page-wide parameters with this pod's overrides applied.
        /// </summary>
        [JsonProperty("request")]
        public Dictionary<string, object> Request { get; set; }

        /// <summary>
        /// Gets or sets the pod's response, in the same shape as a single-pod recommendations response.
        /// </summary>
        [JsonProperty("response")]
        public RecommendationsResponseInner Response { get; set; }

        /// <summary>
        /// Gets or sets the pod's result id. Send this with the pod's recommendation view and click events.
        /// </summary>
        [JsonProperty("result_id")]
        public string ResultId { get; set; }
    }
}
