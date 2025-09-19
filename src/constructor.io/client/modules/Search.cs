using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using Newtonsoft.Json;

namespace Constructorio_NET.Modules
{
    public class Search : Helpers
    {
        private readonly Hashtable Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="Search"/> class.
        /// Interface for search related API calls.
        /// </summary>
        /// <param name="options">Hashtable of options from Constructorio instantiation.</param>
        internal Search(Hashtable options)
        {
            this.Options = options;
        }

        internal string CreateSearchUrl(SearchRequest req)
        {
            Hashtable queryParams = req.GetRequestParameters();
            List<string> paths = new List<string> { "search", req.Query };

            return MakeUrl(this.Options, paths, queryParams);
        }

        /// <summary>
        /// Retrieve search results from API.
        /// </summary>
        /// <param name="searchRequest">Constructorio's search request object.</param>
        /// <param name="cancellationToken">The cancellation token to terminate the request.</param>
        /// <returns>Constructorio's search response object.</returns>
        public async Task<SearchResponse> GetSearchResults(SearchRequest searchRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = CreateSearchUrl(searchRequest);
                var requestHeaders = searchRequest.GetRequestHeaders();
                var result = await MakeHttpRequest(this.Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);

                if (result != null)
                {
                    return JsonConvert.DeserializeObject<SearchResponse>(result);
                }

                throw new ConstructorException("GetSearchResults response data is malformed");
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
        }
    }
}
