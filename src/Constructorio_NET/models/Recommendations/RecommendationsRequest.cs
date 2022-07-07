using Constructorio_NET.Utils;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Constructorio_NET.Models
{
    /**
     * Constructor.io Recommendation Request
     */
    public class RecommendationsRequest
    {
        public string PodId { get; set; }
        public int NumResults { get; set; }
        public List<String> ItemId { get; set; }
        public Dictionary<string, List<string>> Filters { get; set; }
        public string Section { get; set; }
        public string SecurityToken { get; set; }
        public Dictionary<string, string> TestCells { get; set; }
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// Creates a recommendation request
        /// </summary>
        /// <param name="PodId"></param>
        public RecommendationsRequest(string PodId)
        {
            if (PodId == null)
            {
                throw new ArgumentException("PodId is required");
            }
            this.PodId = PodId;
        }

        public Hashtable GetUrlParameters()
        {
            Hashtable parameters = new Hashtable();
            if (this.UserInfo != null)
            {
                if (this.UserInfo.getUserId() != null)
                {
                    parameters.Add(Constants.USER_ID, this.UserInfo.getUserId());
                }
                if (this.UserInfo.getClientId() != null)
                {
                    parameters.Add(Constants.CLIENT_ID, this.UserInfo.getClientId());
                }
                if (this.UserInfo.getSessionId() != 0)
                {
                    parameters.Add(Constants.SESSION_ID, this.UserInfo.getSessionId());
                }
                if (this.UserInfo.getUserSegments() != null)
                {
                    parameters.Add(Constants.SEGMENTS, this.UserInfo.getUserSegments());
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
            if (this.ItemId.Count != 0)
            {
                parameters.Add("item_id", this.ItemId);
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
