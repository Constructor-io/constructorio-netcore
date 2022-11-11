using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models.Recommendations
{
    /// <summary>
    /// Constructor.io Recommendations Request Class.
    /// </summary>
    public class RecommendationsRequest
    {
        /// <summary>
        /// Gets or sets pod id.
        /// </summary>
        public string PodId { get; set; }

        /// <summary>
        /// Gets or sets number of results to return.
        /// </summary>
        public int NumResults { get; set; }

        /// <summary>
        /// Gets or sets item id.
        /// </summary>
        public List<string> ItemIds { get; set; }

        /// <summary>
        /// Gets or sets filters used to refine results.
        /// </summary>
        public Dictionary<string, List<string>> Filters { get; set; }

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
        /// Initializes a new instance of the <see cref="RecommendationsRequest"/> class.
        /// </summary>
        /// <param name="podId">Pod id to use for the request.</param>
        public RecommendationsRequest(string podId)
        {
            if (podId == null)
            {
                throw new ArgumentException("PodId is required");
            }

            this.PodId = podId;
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

            if (this.NumResults != 0)
            {
                parameters.Add("num_results", this.NumResults);
            }

            if (this.Filters != null)
            {
                parameters.Add(Constants.FILTERS, this.Filters);
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
