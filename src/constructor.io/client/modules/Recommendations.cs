using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using Newtonsoft.Json;

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
            List<string> paths = new List<string> { "recommendations", "v1", "pods", req.PodId };

            return MakeUrl(this.Options, paths, queryParams);
        }

        /// <summary>
        /// Retrieve recommendations results from API.
        /// </summary>
        /// <param name="recommendationsRequest">Constructorio's recommendations request object.</param>
        /// <returns>Constructorio's recommendations response object.</returns>
        public async Task<RecommendationsResponse> GetRecommendationsResults(RecommendationsRequest recommendationsRequest)
        {
            string url;
            string result;
            Dictionary<string, string> requestHeaders;

            url = CreateRecommendationsUrl(recommendationsRequest);
            requestHeaders = recommendationsRequest.GetRequestHeaders();
            result = await MakeHttpRequest(this.Options, HttpMethod.Get, url, requestHeaders);

            if (result != null)
            {
                return JsonConvert.DeserializeObject<RecommendationsResponse>(result);
            }

            throw new ConstructorException("GetRecommendationsResults response data is malformed");
        }
    }
}
