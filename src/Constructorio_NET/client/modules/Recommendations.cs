using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using Newtonsoft.Json;

namespace Constructorio_NET
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
            Hashtable queryParams = req.GetUrlParameters();
            List<string> paths = new List<string> { "recommendations", "v1", "pods", req.PodId };

            return MakeUrl(this.Options, paths, queryParams);
        }

        /// <summary>
        /// Retrieve recommendations results from API.
        /// </summary>
        /// <param name="recommendationsRequest">Constructorio's request object</param>
        /// <returns>Constructorio's response object</returns>
        public RecommendationsResponse GetRecommendationsResults(RecommendationsRequest recommendationsRequest)
        {
            string url;
            Task<string> task;
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            try
            {
                url = CreateRecommendationsUrl(recommendationsRequest);
                requestHeaders = recommendationsRequest.GetRequestHeaders();
                task = MakeHttpRequest(HttpMethod.Get, url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (task.Result != null)
            {
                return JsonConvert.DeserializeObject<RecommendationsResponse>(task.Result);
            }

            throw new ConstructorException("GetRecommendationsResults response data is malformed");
        }
    }
}
