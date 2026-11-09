using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Modules
{
    public class Tracker : Helpers
    {
        private readonly Hashtable Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tracker"/> class.
        /// Interface for behavioral tracking API calls.
        /// </summary>
        /// <param name="options">Hashtable of options from Constructorio instantiation.</param>
        internal Tracker(Hashtable options)
        {
            this.Options = options;
        }

        internal string CreateSearchResultsLoadedUrl(SearchResultsLoadedRequest req)
        {
            List<string> paths = new List<string> { "v2", "behavioral_action", "search_result_load" };

            return MakeUrl(this.Options, paths, req.GetRequestParameters());
        }

        /// <summary>
        /// Send a search results loaded event to the API.
        /// </summary>
        /// <param name="searchResultsLoadedRequest">Constructorio's search results loaded request object.</param>
        /// <param name="cancellationToken">The cancellation token to terminate the request.</param>
        /// <returns>True when the event was accepted.</returns>
        public async Task<bool> TrackSearchResultsLoaded(SearchResultsLoadedRequest searchResultsLoadedRequest, CancellationToken cancellationToken = default)
        {
            if (searchResultsLoadedRequest == null)
            {
                throw new ArgumentNullException(nameof(searchResultsLoadedRequest));
            }

            UserInfo userInfo = searchResultsLoadedRequest.UserInfo;

            if (userInfo == null || string.IsNullOrEmpty(userInfo.GetClientId()) || userInfo.GetSessionId() <= 0)
            {
                throw new ConstructorException("UserInfo with a client id and session id is required");
            }

            var url = CreateSearchResultsLoadedUrl(searchResultsLoadedRequest);
            var requestHeaders = searchResultsLoadedRequest.GetRequestHeaders();
            var requestBody = searchResultsLoadedRequest.GetRequestBody();
            await MakeHttpRequest(Options, HttpMethod.Post, url, requestHeaders, requestBody, cancellationToken: cancellationToken).ConfigureAwait(false);

            return true;
        }
    }
}
