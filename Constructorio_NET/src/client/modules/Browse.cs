using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Constructorio_NET
{
    public class Browse : Helpers
    {
        private Hashtable Options;

        /// <summary>
        /// Interface for browse related API calls
        /// </summary>
        internal Browse(Hashtable options)
        {
            this.Options = options;
        }
        internal string CreateBrowseUrl(BrowseRequest req)
        {
            Hashtable queryParams = req.GetUrlParameters();
            List<string> paths = new List<string> { "browse", req.filterName, req.filterValue };

            return Helpers.MakeUrl(this.Options, paths, queryParams);
        }

        /// <summary>
        /// Retrieve browse results from API
        /// </summary>
        /// <param name="browseRequest">Constructorio's request object</param>
        /// <returns>Constructorio's response object</returns>
        public BrowseResponse GetBrowseResults(BrowseRequest browseRequest)
        {
            string url;
            Task<string> task;
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            try
            {
                url = CreateBrowseUrl(browseRequest);
                requestHeaders = browseRequest.GetRequestHeaders();
                
                task = Helpers.MakeHttpRequest(HttpMethod.Get, url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (task.Result != null)
            {
                return JsonConvert.DeserializeObject<BrowseResponse>(task.Result);
            }

            throw new ConstructorException("GetBrowseResults response data is malformed");
        }

        internal string CreateBrowseItemsUrl(BrowseItemsRequest req)
        {
            Hashtable queryParams = req.GetUrlParameters();
            List<string> paths = new List<string> { "browse", "items" };

            return Helpers.MakeUrl(this.Options, paths, queryParams);
        }

        /// <summary>
        /// Retrieve browse items result from API
        /// </summary>
        /// <param name="browseItemsRequest">Constructorio's request object</param>
        /// <returns>Constructorio's response object</returns>
        public BrowseResponse GetBrowseItemsResult(BrowseItemsRequest browseItemsRequest)
        {
            string url;
            Task<string> task;
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            try
            {
                url = CreateBrowseItemsUrl(browseItemsRequest);
                requestHeaders = browseItemsRequest.GetRequestHeaders();
                task = Helpers.MakeHttpRequest(HttpMethod.Get, url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (task.Result != null)
            {
                return JsonConvert.DeserializeObject<BrowseResponse>(task.Result);
            }

            throw new ConstructorException("GetBrowseItemsResult response data is malformed");
        }

        internal string CreateBrowseFacetsUrl(BrowseFacetsRequest req)
        {
            Hashtable queryParams = req.GetUrlParameters();
            List<string> paths = new List<string> { "browse", "facets" };

            return Helpers.MakeUrl(this.Options, paths, queryParams, false);
        }

        /// <summary>
        /// Retrieve browse facets result from API
        /// </summary>
        /// <param name="browseFacetsRequest">Constructorio's request object</param>
        /// <returns>Constructorio's response object</returns>
        public BrowseFacetsResponse GetBrowseFacetsResult(BrowseFacetsRequest browseFacetsRequest)
        {
            string url;
            Task<string> task;
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            try
            {
                url = CreateBrowseFacetsUrl(browseFacetsRequest);
                requestHeaders = browseFacetsRequest.GetRequestHeaders();
                Helpers.addAuthorizationHeaders(Options, requestHeaders);
                task = Helpers.MakeHttpRequest(HttpMethod.Get, url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (task.Result != null)
            {
                return JsonConvert.DeserializeObject<BrowseFacetsResponse>(task.Result);
            }

            throw new ConstructorException("GetBrowseFacetsResult response data is malformed");
        }

        internal string CreateBrowseFacetOptionsUrl(BrowseFacetOptionsRequest req)
        {
            Hashtable queryParams = req.GetUrlParameters();
            List<string> paths = new List<string> { "browse", "facet_options"};

            return Helpers.MakeUrl(this.Options, paths, queryParams, false);
        }

        /// <summary>
        /// Retrieve browse facet options result from API
        /// </summary>
        /// <param name="browseFacetOptionsRequest">Constructorio's request object</param>
        /// <returns>Constructorio's response object</returns>
        public BrowseFacetOptionsResponse GetBrowseFacetOptionsResult(BrowseFacetOptionsRequest browseFacetOptionsRequest)
        {
            string url;
            Task<string> task;
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            try
            {
                url = CreateBrowseFacetOptionsUrl(browseFacetOptionsRequest);
                requestHeaders = browseFacetOptionsRequest.GetRequestHeaders();
                Helpers.addAuthorizationHeaders(Options, requestHeaders);
                task = Helpers.MakeHttpRequest(HttpMethod.Get, url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (task.Result != null)
            {
                return JsonConvert.DeserializeObject<BrowseFacetOptionsResponse>(task.Result);
            }

            throw new ConstructorException("GetBrowseFacetOptionsResult response data is malformed");
        }
    }
}
