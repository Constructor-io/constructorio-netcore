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
        /// <returns>Constructorio's search response object.</returns>
        public SearchResponse GetSearchResults(SearchRequest searchRequest)
        {
            string url;
            Task<string> task;
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            try
            {
                url = CreateSearchUrl(searchRequest);
                requestHeaders = searchRequest.GetRequestHeaders();
                task = MakeHttpRequest(HttpMethod.Get, url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (task.Result != null)
            {
                return JsonConvert.DeserializeObject<SearchResponse>(task.Result);
            }

            throw new ConstructorException("GetSearchResults response data is malformed");
        }
    }
}
