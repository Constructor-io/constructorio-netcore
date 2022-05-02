using System;
using System.Collections;
using System.Collections.Generic;

namespace Constructorio_NET
{
    /**
     * Constructor.io Recommendation Request
     */
    public class RecommendationsRequest
    {
        public string PodId { get; set; }
        public int NumResults { get; set; }
        public List<String> ItemId { get; set; }
        public string ClientId { get; set; }
        public Dictionary<string, List<string>> Filters { get; set; }
        public string Section { get; set; }
        public string SecurityToken { get; set; }
        public List<string> Segments { get; set; }
        public int SessionId { get; set; }
        public Dictionary<string, string> TestCells { get; set; }
        public string UserAgent { get; set; }
        public string UserId { get; set; }
        public string UserIp { get; set; }

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

            if (this.ClientId != null)
            {
                parameters.Add(Constants.CLIENT_ID, this.ClientId);
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
            if (this.Segments != null)
            {
                parameters.Add(Constants.SEGMENTS, this.Segments);
            }
            if (this.SessionId != 0)
            {
                parameters.Add(Constants.SESSION_ID, this.SessionId);
            }
            if (this.TestCells != null)
            {
                parameters.Add(Constants.TEST_CELLS, this.TestCells);
            }
            if (this.UserId != null)
            {
                parameters.Add(Constants.USER_ID, this.UserId);
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

            if (this.UserIp != null)
            {
                requestHeaders.Add(Constants.USER_IP, this.UserIp);
            }
            if (this.UserAgent != null)
            {
                requestHeaders.Add(Constants.USER_AGENT, this.UserAgent);
            }
            if (this.SecurityToken != null)
            {
                requestHeaders.Add(Constants.SECURITY_TOKEN, this.SecurityToken);
            }

            return requestHeaders;
        }
    }
}
