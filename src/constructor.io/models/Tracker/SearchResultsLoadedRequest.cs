using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Search Results Loaded tracking request class.
    /// </summary>
    public class SearchResultsLoadedRequest
    {
        /// <summary>
        /// Maximum number of items sent with the event.
        /// </summary>
        public const int MaxItems = 100;

        /// <summary>
        /// Gets or sets the search term the results were loaded for.
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// Gets or sets the total number of results.
        /// </summary>
        public int ResultCount { get; set; }

        /// <summary>
        /// Gets or sets the result id returned in the search response (<see cref="SearchResponse.ResultId"/>).
        /// </summary>
        public string ResultId { get; set; }

        /// <summary>
        /// Gets or sets the items shown to the user. Only the first 100 are sent.
        /// </summary>
        public List<TrackingItem> Items { get; set; }

        /// <summary>
        /// Gets or sets the page number of the results.
        /// </summary>
        public int ResultPage { get; set; }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Gets or sets the url of the page where the results were shown.
        /// </summary>
        public string Url { get; set; } = "N/A";

        /// <summary>
        /// Gets or sets collection of user related data. Client id and session id are required.
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultsLoadedRequest"/> class.
        /// </summary>
        /// <param name="term">Search term the results were loaded for.</param>
        /// <param name="resultCount">Total number of results.</param>
        public SearchResultsLoadedRequest(string term, int resultCount)
        {
            if (string.IsNullOrEmpty(term))
            {
                throw new ArgumentException("term is required");
            }

            this.Term = term;
            this.ResultCount = resultCount;
        }

        /// <summary>
        /// Get request parameters.
        /// </summary>
        /// <returns>Hashtable of request parameters.</returns>
        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable
            {
                { "beacon", "true" },
            };

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

        /// <summary>
        /// Get request body.
        /// </summary>
        /// <returns>Dictionary of request body fields.</returns>
        public Dictionary<string, object> GetRequestBody()
        {
            Dictionary<string, object> body = new Dictionary<string, object>
            {
                { "search_term", this.Term },
                { "result_count", this.ResultCount },
                { "url", this.Url },
            };

            if (this.ResultId != null)
            {
                body.Add("result_id", this.ResultId);
            }

            if (this.Items != null && this.Items.Count > 0)
            {
                body.Add("items", this.Items.Take(MaxItems).ToList());
            }

            if (this.ResultPage != 0)
            {
                body.Add("result_page", this.ResultPage);
            }

            if (this.Section != null)
            {
                body.Add(Constants.SECTION, this.Section);
            }

            return body;
        }
    }
}
