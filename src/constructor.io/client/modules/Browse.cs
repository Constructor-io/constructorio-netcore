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
    public class Browse : Helpers
    {
        private readonly Hashtable Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="Browse"/> class.
        /// Interface for browse related API calls.
        /// </summary>
        /// <param name="options">Hashtable of options from Constructorio instantiation.</param>
        internal Browse(Hashtable options)
        {
            this.Options = options;
        }

        internal string CreateBrowseUrl(BrowseRequest req)
        {
            Hashtable queryParams = req.GetRequestParameters();
            List<string> paths = new List<string> { "browse", req.FilterName, req.FilterValue };

            return MakeUrl(this.Options, paths, queryParams);
        }

        /// <summary>
        /// Retrieve browse results from API.
        /// </summary>
        /// <param name="browseRequest">Constructorio's browse request object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Constructorio's browse response object.</returns>
        public async Task<BrowseResponse> GetBrowseResults(BrowseRequest browseRequest, CancellationToken cancellationToken = default)
        {
            var url = CreateBrowseUrl(browseRequest);
            Dictionary<string, string> requestHeaders = browseRequest.GetRequestHeaders();
            var result = await MakeHttpRequest(this.Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);

            if (result != null)
            {
                return JsonConvert.DeserializeObject<BrowseResponse>(result);
            }

            throw new ConstructorException("GetBrowseResults response data is malformed");
        }

        internal string CreateBrowseItemsUrl(BrowseItemsRequest req)
        {
            Hashtable queryParams = req.GetRequestParameters();
            List<string> paths = new List<string> { "browse", "items" };

            return MakeUrl(this.Options, paths, queryParams);
        }

        /// <summary>
        /// Retrieve browse items result from API.
        /// </summary>
        /// <param name="browseItemsRequest">Constructorio's browse request object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Constructorio's browse response object.</returns>
        public async Task<BrowseResponse> GetBrowseItemsResult(BrowseItemsRequest browseItemsRequest, CancellationToken cancellationToken = default)
        {
            var url = CreateBrowseItemsUrl(browseItemsRequest);
            Dictionary<string, string> requestHeaders = browseItemsRequest.GetRequestHeaders();
            var result = await MakeHttpRequest(this.Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);

            if (result != null)
            {
                return JsonConvert.DeserializeObject<BrowseResponse>(result);
            }

            throw new ConstructorException("GetBrowseItemsResult response data is malformed");
        }

        internal string CreateBrowseFacetsUrl(BrowseFacetsRequest req)
        {
            Hashtable queryParams = req.GetRequestParameters();
            List<string> paths = new List<string> { "browse", "facets" };
            Dictionary<string, bool> omittedQueryParams = new Dictionary<string, bool>()
            {
                { "_dt", true },
            };

            return MakeUrl(this.Options, paths, queryParams, omittedQueryParams);
        }

        /// <summary>
        /// Retrieve browse facets result from API.
        /// </summary>
        /// <param name="browseFacetsRequest">Constructorio's browse request object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Constructorio's browse facets response object.</returns>
        public async Task<BrowseFacetsResponse> GetBrowseFacetsResult(BrowseFacetsRequest browseFacetsRequest, CancellationToken cancellationToken = default)
        {
            var url = CreateBrowseFacetsUrl(browseFacetsRequest);
            Dictionary<string, string> requestHeaders = browseFacetsRequest.GetRequestHeaders();
            AddAuthHeaders(this.Options, requestHeaders);
            var result = await MakeHttpRequest(this.Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);

            if (result != null)
            {
                return JsonConvert.DeserializeObject<BrowseFacetsResponse>(result);
            }

            throw new ConstructorException("GetBrowseFacetsResult response data is malformed");
        }

        internal string CreateBrowseFacetOptionsUrl(BrowseFacetOptionsRequest req)
        {
            Hashtable queryParams = req.GetRequestParameters();
            List<string> paths = new List<string> { "browse", "facet_options" };
            Dictionary<string, bool> omittedQueryParams = new Dictionary<string, bool>()
            {
                { "_dt", true },
            };

            return MakeUrl(this.Options, paths, queryParams, omittedQueryParams);
        }

        /// <summary>
        /// Retrieve browse facet options result from API.
        /// </summary>
        /// <param name="browseFacetOptionsRequest">Constructorio's browse request object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Constructorio's browse facet options response object.</returns>
        public async Task<BrowseFacetOptionsResponse> GetBrowseFacetOptionsResult(BrowseFacetOptionsRequest browseFacetOptionsRequest, CancellationToken cancellationToken = default)
        {
            var url = CreateBrowseFacetOptionsUrl(browseFacetOptionsRequest);
            Dictionary<string, string> requestHeaders = browseFacetOptionsRequest.GetRequestHeaders();
            AddAuthHeaders(this.Options, requestHeaders);
            var result = await MakeHttpRequest(this.Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);

            if (result != null)
            {
                return JsonConvert.DeserializeObject<BrowseFacetOptionsResponse>(result);
            }

            throw new ConstructorException("GetBrowseFacetOptionsResult response data is malformed");
        }
    }
}
