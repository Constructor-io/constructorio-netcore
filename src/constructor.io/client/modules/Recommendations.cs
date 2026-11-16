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
    public class Recommendations : Helpers
    {
        private readonly Hashtable Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="Recommendations"/> class.
        /// Interface for recommendations related API calls.
        /// </summary>
        /// <param name="options">Hashtable of options from Constructorio instantiation.</param>
        internal Recommendations(Hashtable options)
        {
            this.Options = options;
        }

        internal string CreateRecommendationsUrl(RecommendationsRequest req)
        {
            Hashtable queryParams = req.GetRequestParameters();
            List<string> paths = new List<string>(capacity: 4) { "recommendations", "v1", "pods", req.PodId };

            return MakeUrl(this.Options, paths, queryParams);
        }

        /// <summary>
        /// Retrieve recommendations results from API.
        /// </summary>
        /// <param name="recommendationsRequest">Constructorio's recommendations request object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Constructorio's recommendations response object.</returns>
        public async Task<RecommendationsResponse> GetRecommendationsResults(RecommendationsRequest recommendationsRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = CreateRecommendationsUrl(recommendationsRequest);
                var requestHeaders = recommendationsRequest.GetRequestHeaders();
                var result = await MakeHttpRequest<RecommendationsResponse>(Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);

                return result ?? throw new ConstructorException("GetRecommendationsResults response data is malformed");
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
        }

        internal string CreateRecommendationPageUrl(RecommendationPageRequest req)
        {
            Hashtable queryParams = req.GetRequestParameters();
            List<string> paths = new List<string>(capacity: 4) { "recommendations", "v1", "pages", req.PageId };

            return MakeUrl(this.Options, paths, queryParams);
        }

        /// <summary>
        /// Retrieve recommendation results for every pod on a page from API.
        /// Beta: available to a small group of users and may change. Contact your Customer Success
        /// Manager to request access.
        /// The response's top-level result id identifies the page request and is not a tracking id:
        /// send each pod's own result id with that pod's tracking events.
        /// </summary>
        /// <param name="recommendationPageRequest">Constructorio's recommendation page request object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Constructorio's recommendation page response object.</returns>
        public async Task<RecommendationPageResponse> GetRecommendationPageResults(RecommendationPageRequest recommendationPageRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = CreateRecommendationPageUrl(recommendationPageRequest);
                var requestHeaders = recommendationPageRequest.GetRequestHeaders();
                var result = await MakeHttpRequest<RecommendationPageResponse>(Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);

                return result?.Response?.Pods != null ? result : throw new ConstructorException("GetRecommendationPageResults response data is malformed");
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
        }
    }
}
