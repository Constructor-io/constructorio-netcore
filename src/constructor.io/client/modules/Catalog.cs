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
    public class Catalog : Helpers
    {
        private readonly Hashtable Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="Catalog"/> class.
        /// Interface for catalog related API calls.
        /// </summary>
        /// <param name="options">Hashtable of options from Constructorio instantiation.</param>
        public Catalog(Hashtable options)
        {
            this.Options = options;
        }

        internal string CreateCatalogUrl(CatalogRequest req)
        {
            List<string> paths = new List<string> { "v1", "catalog" };
            Hashtable queryParams = req.GetRequestParameters();
            string url = MakeUrl(this.Options, paths, queryParams, OmitDtAndCQueryParams);

            return url;
        }

        internal string CreateItemGroupsUrl(ItemGroupsRequest req, List<string> additionalPaths = null)
        {
            List<string> paths = new List<string> { "v1", "item_groups" };

            if (additionalPaths != null)
            {
                paths.AddRange(additionalPaths);
            }

            Hashtable queryParams = req.GetRequestParameters();
            string url = MakeUrl(this.Options, paths, queryParams, OmitDtQueryParam);

            return url;
        }

        internal string CreateRetrieveSearchabilitiesUrl(RetrieveSearchabilitiesRequest req)
        {
            List<string> paths = new List<string> { "v1", "searchabilities" };
            Hashtable queryParams = req.GetRequestParameters();
            string url = MakeUrl(this.Options, paths, queryParams, OmitDtAndCQueryParams);

            return url;
        }

        internal string CreatePatchSearchabilitiesUrl(PatchSearchabilitiesRequest req)
        {
            List<string> paths = new List<string> { "v1", "searchabilities" };
            Hashtable queryParams = req.GetRequestParameters();
            string url = MakeUrl(this.Options, paths, queryParams, OmitDtAndCQueryParams);

            return url;
        }

        /// <summary>
        /// Send full catalog files to replace the current catalog.
        /// </summary>
        /// <param name="catalogRequest">Constructorio's catalog request object.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<CatalogResponse> ReplaceCatalog(CatalogRequest catalogRequest, CancellationToken cancellationToken = default)
        {
            CatalogResponse result;

            try
            {
                var url = CreateCatalogUrl(catalogRequest);
                Dictionary<string, string> requestHeaders = catalogRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest<CatalogResponse>(
                    this.Options,
                    HttpMethod.Put,
                    url,
                    requestHeaders,
                    null,
                    catalogRequest.Files,
                    cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("ReplaceCatalog response data is malformed");
        }

        /// <summary>
        /// Send full catalog files to update the current catalog.
        /// </summary>
        /// <param name="catalogRequest">Constructorio's catalog request object.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<CatalogResponse> UpdateCatalog(CatalogRequest catalogRequest, CancellationToken cancellationToken = default)
        {
            CatalogResponse result;

            try
            {
                var url = CreateCatalogUrl(catalogRequest);
                Dictionary<string, string> requestHeaders = catalogRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest<CatalogResponse>(Options, HttpMethodPatch, url, requestHeaders, null, catalogRequest.Files, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("UpdateCatalog response data is malformed");
        }

        /// <summary>
        /// Send full catalog files to patch update the current catalog.
        /// </summary>
        /// <param name="catalogRequest">Constructorio's catalog request object.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<CatalogResponse> PatchCatalog(CatalogRequest catalogRequest, CancellationToken cancellationToken = default)
        {
            CatalogResponse result;

            try
            {
                var url = CreateCatalogUrl(catalogRequest);
                url += "&patch_delta=true";
                Dictionary<string, string> requestHeaders = catalogRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest<CatalogResponse>(Options, HttpMethodPatch, url, requestHeaders, null, catalogRequest.Files, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("PatchCatalog response data is malformed");
        }

        /// <summary>
        /// Add item group to a catalog.
        /// </summary>
        /// <param name="itemGroupsRequest">Constructorio's item groups request object.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's item group.</returns>
        public async Task<ConstructorItemGroup> AddItemGroup(ItemGroupsRequest itemGroupsRequest, CancellationToken cancellationToken = default)
        {
            ConstructorItemGroup result;

            try
            {
                ConstructorItemGroup itemGroup = itemGroupsRequest.ItemGroups[0];
                var url = CreateItemGroupsUrl(itemGroupsRequest, new List<string> { itemGroup.Id });
                Hashtable requestBody = new Hashtable();

                if (itemGroup.Name != null)
                {
                    requestBody.Add("name", itemGroup.Name);
                }

                if (itemGroup.ParentId != null)
                {
                    requestBody.Add("parent_id", itemGroup.ParentId);
                }

                if (itemGroup.Data != null)
                {
                    requestBody.Add("data", itemGroup.Data);
                }

                Dictionary<string, string> requestHeaders = itemGroupsRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest<ConstructorItemGroup>(Options, HttpMethod.Put, url, requestHeaders, requestBody, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("AddItemGroup response data is malformed");
        }

        /// <summary>
        /// Update item group in a catalog.
        /// </summary>
        /// <param name="itemGroupsRequest">Constructorio's item groups request object.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's item group.</returns>
        public async Task<ConstructorItemGroup> UpdateItemGroup(ItemGroupsRequest itemGroupsRequest, CancellationToken cancellationToken = default)
        {
            ConstructorItemGroup result;

            try
            {
                ConstructorItemGroup itemGroup = itemGroupsRequest.ItemGroups[0];
                var url = CreateItemGroupsUrl(itemGroupsRequest, new List<string>(capacity: 1) { itemGroup.Id });
                Hashtable requestBody = new Hashtable();

                if (itemGroup.Name != null)
                {
                    requestBody.Add("name", itemGroup.Name);
                }

                if (itemGroup.ParentId != null)
                {
                    requestBody.Add("parent_id", itemGroup.ParentId);
                }

                if (itemGroup.Data != null)
                {
                    requestBody.Add("data", itemGroup.Data);
                }

                Dictionary<string, string> requestHeaders = itemGroupsRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest<ConstructorItemGroup>(Options, HttpMethod.Put, url, requestHeaders, requestBody, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("UpdateItemGroup response data is malformed");
        }

        /// <summary>
        /// Add item group(s) to a catalog (limit of 1,000).
        /// </summary>
        /// <param name="itemGroupsRequest">Constructorio's item groups request object.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's item group response object.</returns>
        public async Task<ItemGroupsResponse> AddItemGroups(ItemGroupsRequest itemGroupsRequest, CancellationToken cancellationToken = default)
        {
            ItemGroupsResponse result;

            try
            {
                var url = CreateItemGroupsUrl(itemGroupsRequest);
                Hashtable requestBody = new Hashtable
                {
                    { "item_groups", itemGroupsRequest.ItemGroups }
                };
                Dictionary<string, string> requestHeaders = itemGroupsRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest<ItemGroupsResponse>(Options, HttpMethod.Post, url, requestHeaders, requestBody, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("AddItemGroups response data is malformed");
        }

        /// <summary>
        /// Update item group(s) in a catalog (limit of 1,000).
        /// </summary>
        /// <param name="itemGroupsRequest">Constructorio's item groups request object.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's item group response object.</returns>
        public async Task<ItemGroupsResponse> UpdateItemGroups(ItemGroupsRequest itemGroupsRequest, CancellationToken cancellationToken = default)
        {
            ItemGroupsResponse result;

            try
            {
                var url = CreateItemGroupsUrl(itemGroupsRequest);
                Hashtable requestBody = new Hashtable
                {
                    { "item_groups", itemGroupsRequest.ItemGroups }
                };
                Dictionary<string, string> requestHeaders = itemGroupsRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest<ItemGroupsResponse>(Options, HttpMethodPatch, url, requestHeaders, requestBody, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("UpdateItemGroups response data is malformed");
        }

        /// <summary>
        /// Retrieves item group(s) in a tree structure.
        /// </summary>
        /// <param name="itemGroupsRequest">Constructorio's item groups request object.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's item group response object.</returns>
        public async Task<ItemGroupsGetResponse> GetItemGroup(ItemGroupsRequest itemGroupsRequest, CancellationToken cancellationToken = default)
        {
            ItemGroupsGetResponse result;

            try
            {
                string url;
                if (itemGroupsRequest.ItemGroupId != null)
                {
                    url = CreateItemGroupsUrl(itemGroupsRequest, new List<string>(capacity: 1) { itemGroupsRequest.ItemGroupId });
                }
                else
                {
                    url = CreateItemGroupsUrl(itemGroupsRequest);
                }

                Hashtable requestBody = new Hashtable();
                Dictionary<string, string> requestHeaders = itemGroupsRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest<ItemGroupsGetResponse>(Options, HttpMethod.Get, url, requestHeaders, requestBody, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("GetItemGroup response data is malformed");
        }

        /// <summary>
        /// Delete all item groups.
        /// </summary>
        /// <param name="itemGroupsRequest">Constructorio's item groups request object.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's confirmation message.</returns>
        public async Task<string> DeleteItemGroups(ItemGroupsRequest itemGroupsRequest, CancellationToken cancellationToken = default)
        {
            ItemGroupsResponse result;

            try
            {
                var url = CreateItemGroupsUrl(itemGroupsRequest);
                Dictionary<string, string> requestHeaders = itemGroupsRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest<ItemGroupsResponse>(Options, HttpMethod.Delete, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result?.Message ?? throw new ConstructorException("DeleteItemGroups response data is malformed");
        }

        // Facets
        internal string CreateFacetUrl(string section = "Products", string facetGroup = null, Hashtable queryParams = null)
        {
            List<string> paths = new List<string> { "v1", "facets" };
            if (facetGroup != null)
            {
                paths.Add(facetGroup);
            }

            if (queryParams == null)
            {
                queryParams = new Hashtable();
            }

            if (!string.IsNullOrEmpty(section))
            {
                queryParams.Add(Constants.SECTION, section);
            }

            string url = MakeUrl(this.Options, paths, queryParams, OmitDtAndCQueryParams);

            return url;
        }

        /// <summary>
        /// Creates a Facet Configuration.
        /// </summary>
        /// <param name="facet">New Facet Configuration to be created.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Facet object representing the facet created.</returns>
        public async Task<Facet> CreateFacetConfig(Facet facet, string section = "Products", CancellationToken cancellationToken = default)
        {
            string url = CreateFacetUrl(section);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            Facet result;
            try
            {
                result = await MakeHttpRequest<Facet>(Options, HttpMethod.Post, url, requestHeaders, facet, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("CreateFacetConfig response data is malformed");
        }

        /// <summary>
        /// Gets all facets in a particular section.
        /// </summary>
        /// <param name="paginationOptions">PaginationOptions object for pagination.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>List of Facets in a given section.</returns>
        public async Task<FacetGetAllResponse> GetAllFacetConfigs(PaginationOptions paginationOptions = null, string section = "Products", CancellationToken cancellationToken = default)
        {
            string url;
            if (paginationOptions != null)
            {
                url = CreateFacetUrl(section, queryParams: paginationOptions.GetQueryParameters());
            }
            else
            {
                url = CreateFacetUrl(section);
            }

            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            FacetGetAllResponse result;
            try
            {
                result = await MakeHttpRequest<FacetGetAllResponse>(Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("GetAllFacetConfigs response data is malformed");
        }

        /// <summary>
        /// Gets a facet given the facet name and section.
        /// </summary>
        /// <param name="facetName">Name of the facet.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Facet object representing the facet.</returns>
        public async Task<Facet> GetFacetConfig(string facetName, string section = "Products", CancellationToken cancellationToken = default)
        {
            string url = CreateFacetUrl(section, facetName);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            Facet result;
            try
            {
                result = await MakeHttpRequest<Facet>(Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("GetFacetConfig response data is malformed");
        }

        /// <summary>
        /// Partially updates a list of facet configurations.
        /// </summary>
        /// <param name="facetFieldsList">List of facets fields to be updated.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>List of updated facet configurations.</returns>
        public async Task<List<Facet>> BatchPartiallyUpdateFacetConfigs(List<Facet> facetFieldsList, string section = "Products", CancellationToken cancellationToken = default)
        {
            string url = CreateFacetUrl(section);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            List<Facet> result;
            try
            {
                result = await MakeHttpRequest<List<Facet>>(Options, HttpMethodPatch, url, requestHeaders, facetFieldsList, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("BatchPartiallyUpdateFacetConfigs response data is malformed");
        }

        /// <summary>
        /// Partially updates a specifc facet configuration by facet name.
        /// </summary>
        /// <param name="facetFields">Facets fields to be updated.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Updated facet configuration.</returns>
        public async Task<Facet> PartiallyUpdateFacetConfig(Facet facetFields, string section = "Products", CancellationToken cancellationToken = default)
        {
            string url = CreateFacetUrl(section, facetFields.Name);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            Facet result;
            try
            {
                result = await MakeHttpRequest<Facet>(Options, HttpMethodPatch, url, requestHeaders, facetFields, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("PartiallyUpdateFacetConfig response data is malformed");
        }

        /// <summary>
        /// Replace an existing facet configuration by facet name.
        /// </summary>
        /// <param name="facet">New Facet Configuration to be created.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Updated facet configuration.</returns>
        public async Task<Facet> UpdateFacetConfig(Facet facet, string section = "Products", CancellationToken cancellationToken = default)
        {
            string url = CreateFacetUrl(section, facet.Name);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            Facet result;
            try
            {
                result = await MakeHttpRequest<Facet>(Options, HttpMethod.Put, url, requestHeaders, facet, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("UpdateFacetConfig response data is malformed");
        }

        /// <summary>
        /// Deletes a Facet Configuration.
        /// </summary>
        /// <param name="facetName">Name of the facet configuration/facet group.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Facet object representing the facet configuration deleted.</returns>
        public async Task<Facet> DeleteFacetConfig(string facetName, string section = "Products", CancellationToken cancellationToken = default)
        {
            string url = CreateFacetUrl(section, facetName);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            Facet result;
            try
            {
                result = await MakeHttpRequest<Facet>(Options, HttpMethod.Delete, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("DeleteFacetConfig response data is malformed");
        }

        // Facet Options
        internal string CreateFacetOptionsUrl(string facetGroupName, string section = "Products", string facetOptionValue = null, Hashtable queryParams = null)
        {
            List<string> paths = new List<string> { "v1", "facets", facetGroupName, "options" };

            if (facetOptionValue != null)
            {
                paths.Add(facetOptionValue);
            }

            if (queryParams == null)
            {
                queryParams = new Hashtable();
            }

            if (!string.IsNullOrEmpty(section))
            {
                queryParams.Add(Constants.SECTION, section);
            }

            string url = MakeUrl(this.Options, paths, queryParams, OmitDtAndCQueryParams);

            return url;
        }

        /// <summary>
        /// Creates a Facet Option given a facet group name.
        /// </summary>
        /// <param name="facetOption">New Facet Option to be created.</param>
        /// <param name="facetGroupName">Facet Group where the Facet Option should be created.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Created facet option.</returns>
        public async Task<FacetOption> CreateFacetOption(FacetOption facetOption, string facetGroupName, string section = "Products", CancellationToken cancellationToken = default)
        {
            string url = CreateFacetOptionsUrl(facetGroupName, section);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            FacetOption result;
            try
            {
                result = await MakeHttpRequest<FacetOption>(Options, HttpMethod.Post, url, requestHeaders, facetOption, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("CreateFacetOption response data is malformed");
        }

        /// <summary>
        /// Gets all Facet Options in a facet group.
        /// </summary>
        /// <param name="facetGroupName">Facet Group to retrieve the Facet Options from.</param>
        /// <param name="paginationOptions">PaginationOptions object for pagination.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Response containing all facet options in the specified group.</returns>
        public async Task<FacetOptionsGetAllResponse> GetAllFacetOptions(string facetGroupName, PaginationOptions paginationOptions = null, string section = "Products", CancellationToken cancellationToken = default)
        {
            string url;
            if (paginationOptions != null)
            {
                url = CreateFacetOptionsUrl(facetGroupName, section, queryParams: paginationOptions.GetQueryParameters());
            }
            else
            {
                url = CreateFacetOptionsUrl(facetGroupName, section);
            }

            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            FacetOptionsGetAllResponse result;
            try
            {
                result = await MakeHttpRequest<FacetOptionsGetAllResponse>(Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("GetAllFacetOptions response data is malformed");
        }

        /// <summary>
        /// Gets a specific Facet Option given the facet option value and facet group.
        /// </summary>
        /// <param name="facetOptionValue">Facet Option value to identify the Facet Option to be retrieved.</param>
        /// <param name="facetGroupName">Facet Group where the Facet Option resides.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>The requested facet option.</returns>
        public async Task<FacetOption> GetFacetOption(string facetOptionValue, string facetGroupName, string section = "Products", CancellationToken cancellationToken = default)
        {
            string url = CreateFacetOptionsUrl(facetGroupName, section, facetOptionValue);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            FacetOption result;
            try
            {
                result = await MakeHttpRequest<FacetOption>(Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("GetFacetOption response data is malformed");
        }

        /// <summary>
        /// Creates the Facet Options if they don't exists, or partially updates them if they do.
        /// </summary>
        /// <param name="facetOptions">List of Facet Options to be created or partially updated.</param>
        /// <param name="facetGroupName">Facet Group where the Facet Options reside.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>List of created or updated facet options.</returns>
        public async Task<List<FacetOption>> BatchCreateOrUpdateFacetOptions(List<FacetOption> facetOptions, string facetGroupName, string section = "Products", CancellationToken cancellationToken = default)
        {
            string url = CreateFacetOptionsUrl(facetGroupName, section);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            List<FacetOption> result;
            try
            {
                result = await MakeHttpRequest<List<FacetOption>>(Options, HttpMethodPatch, url, requestHeaders, facetOptions, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("BatchCreateOrUpdateFacetOptions response data is malformed");
        }

        /// <summary>
        /// Replaces an existing Facet Option with a new one.
        /// </summary>
        /// <param name="facetOption">New Facet Option.</param>
        /// <param name="facetGroupName">Facet Group where the Facet Option resides.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>The replaced facet option.</returns>
        public async Task<FacetOption> ReplaceFacetOption(FacetOption facetOption, string facetGroupName, string section = "Products", CancellationToken cancellationToken = default)
        {
            string url = CreateFacetOptionsUrl(facetGroupName, section, facetOption.Value);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            FacetOption result;
            try
            {
                result = await MakeHttpRequest<FacetOption>(Options, HttpMethod.Put, url, requestHeaders, facetOption, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("ReplaceFacetOption response data is malformed");
        }

        /// <summary>
        /// Partially updates an existing Facet Option.
        /// </summary>
        /// <param name="facetOption">Facet Option values for partial updating.</param>
        /// <param name="facetGroupName">Facet Group where the Facet Option resides.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>The updated facet option.</returns>
        public async Task<FacetOption> PartiallyUpdateFacetOption(FacetOption facetOption, string facetGroupName, string section = "Products", CancellationToken cancellationToken = default)
        {
            string url = CreateFacetOptionsUrl(facetGroupName, section, facetOption.Value);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            FacetOption result;
            try
            {
                result = await MakeHttpRequest<FacetOption>(Options, HttpMethodPatch, url, requestHeaders, facetOption, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("PartiallyUpdateFacetOption response data is malformed");
        }

        /// <summary>
        /// Deletes a particular Facet Option given the Facet Option Value and Facet Group.
        /// </summary>
        /// <param name="facetOptionValue">Facet Option value to identify the Facet Option to be deleted.</param>
        /// <param name="facetGroupName">Facet Group where the Facet Option resides.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>The deleted facet option.</returns>
        public async Task<FacetOption> DeleteFacetOption(string facetOptionValue, string facetGroupName, string section = "Products", CancellationToken cancellationToken = default)
        {
            string url = CreateFacetOptionsUrl(facetGroupName, section, facetOptionValue);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            FacetOption result;
            try
            {
                result = await MakeHttpRequest<FacetOption>(Options, HttpMethod.Delete, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("DeleteFacetOption response data is malformed");
        }

        /// <summary>
        /// Retrieves searchabilities with options for filtering, paginating.
        /// </summary>
        /// <param name="retrieveSearchabilitiesRequest">Constructorio's retrieve searchabilities request object.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's Searchability response object.</returns>
        public async Task<SearchabilitiesResponse> RetrieveSearchabilities(RetrieveSearchabilitiesRequest retrieveSearchabilitiesRequest, CancellationToken cancellationToken = default)
        {
            SearchabilitiesResponse result;

            try
            {
                var url = CreateRetrieveSearchabilitiesUrl(retrieveSearchabilitiesRequest);
                Dictionary<string, string> requestHeaders = retrieveSearchabilitiesRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest<SearchabilitiesResponse>(Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("RetrieveSearchabilities response data is malformed");
        }

        /// <summary>
        /// Send single or multiple searchabilities to create or modify.
        /// </summary>
        /// <param name="patchSearchabilitiesRequest">Constructorio's patch searchabilities request object.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's Searchability response object.</returns>
        public async Task<SearchabilitiesResponse> PatchSearchabilities(PatchSearchabilitiesRequest patchSearchabilitiesRequest, CancellationToken cancellationToken = default)
        {
            SearchabilitiesResponse result;
            Hashtable postbody = new Hashtable
            {
                { "searchabilities", patchSearchabilitiesRequest.Searchabilities }
            };

            try
            {
                var url = CreatePatchSearchabilitiesUrl(patchSearchabilitiesRequest);
                Dictionary<string, string> requestHeaders = patchSearchabilitiesRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest<SearchabilitiesResponse>(Options, HttpMethodPatch, url, requestHeaders, postbody, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("PatchSearchabilities response data is malformed");
        }

        // Sort Options
        internal string CreateSortOptionsUrl(SortOptionsRequest req)
        {
            List<string> paths = new List<string>(capacity: 2) { "v1", "sort_options" };
            Hashtable queryParams = req.GetRequestParameters();

            bool toFilterBySortBy = req.SortBy != null;

            if (toFilterBySortBy)
            {
                queryParams.Add("sort_by", req.SortBy);
            }

            string url = MakeUrl(this.Options, paths, queryParams, OmitDtAndCQueryParams);

            return url;
        }

        /// <summary>
        /// Retrieves a list of all Sort Options.
        /// Specify an optional SortBy property to retrieve a specific sort option.
        /// </summary>
        /// <param name="sortOptionsRequest">Constructorio's <see cref="SortOptionsRequest"/> object model.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's <see cref="SortOptionList"/> object.</returns>
        public async Task<SortOptionList> RetrieveSortOptions(SortOptionsRequest sortOptionsRequest, CancellationToken cancellationToken = default)
        {
            string url = CreateSortOptionsUrl(sortOptionsRequest);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            SortOptionList result;
            try
            {
                result = await MakeHttpRequest<SortOptionList>(Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("RetrieveSortOptions response data is malformed");
        }

        /// <summary>
        /// Replaces all existing Sort Options with a new list of Sort Options.
        /// At most 50 Sort Options can be provided.
        /// </summary>
        /// <param name="sortOptionsListRequest">Constructorio's <see cref="SortOptionsListRequest"/> object model.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's <see cref="SortOptionList"/> object.</returns>
        public async Task<SortOptionList> SetSortOptions(SortOptionsListRequest sortOptionsListRequest, CancellationToken cancellationToken = default)
        {
            string url = CreateSortOptionsUrl(sortOptionsListRequest);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            SortOptionList result;
            try
            {
                result = await MakeHttpRequest<SortOptionList>(Options, HttpMethod.Put, url, requestHeaders, sortOptionsListRequest.GetSortOptionsList(), cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("SetSortOptions response data is malformed");
        }

        /// <summary>
        /// Deletes a list of Sort Options, identified by sort_by and sort_order.
        /// </summary>
        /// <param name="sortOptionsListRequest">Constructorio's <see cref="SortOptionsListRequest"/> object model.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>True if delete request succeeds.</returns>
        public async Task<bool> DeleteSortOptions(SortOptionsListRequest sortOptionsListRequest, CancellationToken cancellationToken = default)
        {
            string url = CreateSortOptionsUrl(sortOptionsListRequest);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            try
            {
                await MakeHttpRequest(this.Options, HttpMethod.Delete, url, requestHeaders, sortOptionsListRequest.GetSortOptionListForDeletion(), cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return true;
        }

        internal string CreateSortOptionUrl(SortOptionsRequest req, string filterBySortBy = null, SortOrder? filterBySortOrder = null)
        {
            List<string> paths = new List<string> { "v1", "sort_option" };
            Hashtable queryParams = req.GetRequestParameters();

            bool toTargetSpecificSortOption = filterBySortOrder != null && filterBySortBy != null;

            if (toTargetSpecificSortOption)
            {
                paths.Add(filterBySortBy);
                paths.Add(filterBySortOrder.ToString().ToLower());
            }

            string url = MakeUrl(this.Options, paths, queryParams, OmitDtAndCQueryParams);

            return url;
        }

        /// <summary>
        /// Creates a Sort Option.
        /// </summary>
        /// <param name="sortOptionsSingleRequest">Constructorio's <see cref="SortOptionsSingleRequest"/> object model.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's <see cref="SortOption"/> object.</returns>
        public async Task<SortOption> CreateSortOption(SortOptionsSingleRequest sortOptionsSingleRequest, CancellationToken cancellationToken = default)
        {
            string url = CreateSortOptionUrl(sortOptionsSingleRequest);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            SortOption result;
            try
            {
                result = await MakeHttpRequest<SortOption>(Options, HttpMethod.Post, url, requestHeaders, sortOptionsSingleRequest.SortOption, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("CreateSortOption response data is malformed");
        }

        /// <summary>
        /// Creates or Replace a Sort Option.
        /// </summary>
        /// <param name="sortOptionsSingleRequest">Constructorio's <see cref="SortOptionsSingleRequest"/> object model.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's <see cref="SortOption"/> object.</returns>
        public async Task<SortOption> CreateOrReplaceSortOption(SortOptionsSingleRequest sortOptionsSingleRequest, CancellationToken cancellationToken = default)
        {
            if (sortOptionsSingleRequest.SortOption.SortBy == null)
            {
                throw new ConstructorException("SortBy is a required property for SortOptionsSingleRequest.SortOption.");
            }

            string url = CreateSortOptionUrl(sortOptionsSingleRequest, sortOptionsSingleRequest.SortOption.SortBy, sortOptionsSingleRequest.SortOption.SortOrder);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            SortOption result;
            try
            {
                result = await MakeHttpRequest<SortOption>(Options, HttpMethod.Put, url, requestHeaders, sortOptionsSingleRequest.GetSortOptionDelta(), cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("CreateOrReplaceSortOption response data is malformed");
        }

        /// <summary>
        /// Updates a Sort Option.
        /// </summary>
        /// <param name="sortOptionsSingleRequest">Constructorio's <see cref="SortOptionsSingleRequest"/> object model.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's <see cref="SortOption"/> object.</returns>
        public async Task<SortOption> UpdateSortOption(SortOptionsSingleRequest sortOptionsSingleRequest, CancellationToken cancellationToken = default)
        {
            if (sortOptionsSingleRequest.SortOption.SortBy == null)
            {
                throw new ConstructorException("SortBy is a required property for SortOptionsSingleRequest.SortOption.");
            }

            string url = CreateSortOptionUrl(sortOptionsSingleRequest, sortOptionsSingleRequest.SortOption.SortBy, sortOptionsSingleRequest.SortOption.SortOrder);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            SortOption result;
            try
            {
                result = await MakeHttpRequest<SortOption>(Options, HttpMethodPatch, url, requestHeaders, sortOptionsSingleRequest.GetSortOptionDelta(), cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("UpdateSortOption response data is malformed");
        }

        #region V2 Facets

        internal string CreateFacetUrlV2(string section = "Products", string facetName = null, Hashtable queryParams = null)
        {
            List<string> paths = new List<string> { "v2", "facets" };
            if (facetName != null)
            {
                paths.Add(facetName);
            }

            if (queryParams == null)
            {
                queryParams = new Hashtable();
            }

            if (!string.IsNullOrEmpty(section))
            {
                queryParams.Add(Constants.SECTION, section);
            }

            string url = MakeUrl(this.Options, paths, queryParams, OmitDtAndCQueryParams);

            return url;
        }

        /// <summary>
        /// Creates a v2 Facet Configuration.
        /// </summary>
        /// <param name="facet">New v2 Facet Configuration to be created.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>FacetV2 object representing the facet created.</returns>
        public async Task<FacetV2> CreateFacetConfigV2(FacetV2 facet, string section = "Products", CancellationToken cancellationToken = default)
        {
            if (facet == null)
            {
                throw new ArgumentNullException(nameof(facet));
            }

            string url = CreateFacetUrlV2(section);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            FacetV2 result;
            try
            {
                result = await MakeHttpRequest<FacetV2>(Options, HttpMethod.Post, url, requestHeaders, facet, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("CreateFacetConfigV2 response data is malformed");
        }

        /// <summary>
        /// Gets all v2 facets in a particular section.
        /// </summary>
        /// <param name="paginationOptions">PaginationOptions object for pagination.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>List of v2 Facets in a given section.</returns>
        public async Task<FacetV2GetAllResponse> GetAllFacetConfigsV2(PaginationOptions paginationOptions = null, string section = "Products", CancellationToken cancellationToken = default)
        {
            string url;
            if (paginationOptions != null)
            {
                url = CreateFacetUrlV2(section, queryParams: paginationOptions.GetQueryParameters());
            }
            else
            {
                url = CreateFacetUrlV2(section);
            }

            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            FacetV2GetAllResponse result;
            try
            {
                result = await MakeHttpRequest<FacetV2GetAllResponse>(Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("GetAllFacetConfigsV2 response data is malformed");
        }

        /// <summary>
        /// Gets a v2 facet given the facet name and section.
        /// </summary>
        /// <param name="facetName">Name of the facet.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>FacetV2 object representing the facet.</returns>
        public async Task<FacetV2> GetFacetConfigV2(string facetName, string section = "Products", CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(facetName))
            {
                throw new ArgumentException("facetName is required", nameof(facetName));
            }

            string url = CreateFacetUrlV2(section, facetName);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            FacetV2 result;
            try
            {
                result = await MakeHttpRequest<FacetV2>(Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("GetFacetConfigV2 response data is malformed");
        }

        /// <summary>
        /// Create or replace v2 facet configurations (batch operation).
        /// Replacing will overwrite all other configurations, resetting them to defaults, except facet options.
        /// </summary>
        /// <param name="facets">List of v2 facets to create or replace.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>List of created or replaced v2 facet configurations.</returns>
        public async Task<List<FacetV2>> CreateOrReplaceFacetConfigsV2(List<FacetV2> facets, string section = "Products", CancellationToken cancellationToken = default)
        {
            if (facets == null || facets.Count == 0)
            {
                throw new ArgumentException("facets cannot be null or empty", nameof(facets));
            }

            string url = CreateFacetUrlV2(section);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            Hashtable requestBody = new Hashtable
            {
                { "facets", facets }
            };

            List<FacetV2> result;
            try
            {
                result = await MakeHttpRequest<List<FacetV2>>(Options, HttpMethod.Put, url, requestHeaders, requestBody, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("CreateOrReplaceFacetConfigsV2 response data is malformed");
        }

        /// <summary>
        /// Partially updates a list of v2 facet configurations (batch operation).
        /// </summary>
        /// <param name="facetFieldsList">List of v2 facets fields to be updated.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>List of updated v2 facet configurations.</returns>
        public async Task<List<FacetV2>> BatchPartiallyUpdateFacetConfigsV2(List<FacetV2> facetFieldsList, string section = "Products", CancellationToken cancellationToken = default)
        {
            if (facetFieldsList == null || facetFieldsList.Count == 0)
            {
                throw new ArgumentException("facetFieldsList cannot be null or empty", nameof(facetFieldsList));
            }

            string url = CreateFacetUrlV2(section);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            Hashtable requestBody = new Hashtable
            {
                { "facets", facetFieldsList }
            };

            List<FacetV2> result;
            try
            {
                result = await MakeHttpRequest<List<FacetV2>>(Options, HttpMethodPatch, url, requestHeaders, requestBody, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("BatchPartiallyUpdateFacetConfigsV2 response data is malformed");
        }

        /// <summary>
        /// Replace an existing v2 facet configuration by facet name.
        /// This will overwrite all other configurations, resetting them to defaults, except facet options.
        /// </summary>
        /// <param name="facet">New v2 Facet Configuration to replace the existing one.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Replaced v2 facet configuration.</returns>
        public async Task<FacetV2> ReplaceFacetConfigV2(FacetV2 facet, string section = "Products", CancellationToken cancellationToken = default)
        {
            if (facet == null)
            {
                throw new ArgumentNullException(nameof(facet));
            }

            if (string.IsNullOrEmpty(facet.Name))
            {
                throw new ArgumentException("facet.Name is required", nameof(facet));
            }

            string url = CreateFacetUrlV2(section, facet.Name);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            FacetV2 result;
            try
            {
                result = await MakeHttpRequest<FacetV2>(Options, HttpMethod.Put, url, requestHeaders, facet, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("ReplaceFacetConfigV2 response data is malformed");
        }

        /// <summary>
        /// Partially updates a specific v2 facet configuration by facet name.
        /// </summary>
        /// <param name="facetFields">v2 Facet fields to be updated.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Updated v2 facet configuration.</returns>
        public async Task<FacetV2> PartiallyUpdateFacetConfigV2(FacetV2 facetFields, string section = "Products", CancellationToken cancellationToken = default)
        {
            if (facetFields == null)
            {
                throw new ArgumentNullException(nameof(facetFields));
            }

            if (string.IsNullOrEmpty(facetFields.Name))
            {
                throw new ArgumentException("facetFields.Name is required", nameof(facetFields));
            }

            string url = CreateFacetUrlV2(section, facetFields.Name);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            FacetV2 result;
            try
            {
                result = await MakeHttpRequest<FacetV2>(Options, HttpMethodPatch, url, requestHeaders, facetFields, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("PartiallyUpdateFacetConfigV2 response data is malformed");
        }

        /// <summary>
        /// Deletes a v2 Facet Configuration.
        /// </summary>
        /// <param name="facetName">Name of the facet configuration/facet group.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>FacetV2 object representing the facet configuration deleted.</returns>
        public async Task<FacetV2> DeleteFacetConfigV2(string facetName, string section = "Products", CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(facetName))
            {
                throw new ArgumentException("facetName is required", nameof(facetName));
            }

            string url = CreateFacetUrlV2(section, facetName);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            FacetV2 result;
            try
            {
                result = await MakeHttpRequest<FacetV2>(Options, HttpMethod.Delete, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("DeleteFacetConfigV2 response data is malformed");
        }

        #endregion

        #region V2 Searchabilities

        internal string CreateSearchabilitiesUrlV2(string section = "Products", string name = null, Hashtable queryParams = null)
        {
            List<string> paths = new List<string> { "v2", "searchabilities" };
            if (name != null)
            {
                paths.Add(name);
            }

            if (queryParams == null)
            {
                queryParams = new Hashtable();
            }

            if (!string.IsNullOrEmpty(section))
            {
                queryParams.Add(Constants.SECTION, section);
            }

            string url = MakeUrl(this.Options, paths, queryParams, OmitDtAndCQueryParams);

            return url;
        }

        /// <summary>
        /// Retrieves v2 searchabilities with options for filtering and paginating.
        /// </summary>
        /// <param name="retrieveSearchabilitiesV2Request">Constructorio's v2 retrieve searchabilities request object.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's v2 Searchabilities response object.</returns>
        public async Task<SearchabilitiesV2Response> RetrieveSearchabilitiesV2(RetrieveSearchabilitiesV2Request retrieveSearchabilitiesV2Request, CancellationToken cancellationToken = default)
        {
            if (retrieveSearchabilitiesV2Request == null)
            {
                throw new ArgumentNullException(nameof(retrieveSearchabilitiesV2Request));
            }

            SearchabilitiesV2Response result;

            try
            {
                var url = CreateSearchabilitiesUrlV2(retrieveSearchabilitiesV2Request.Section ?? "Products", queryParams: retrieveSearchabilitiesV2Request.GetRequestParameters());
                Dictionary<string, string> requestHeaders = retrieveSearchabilitiesV2Request.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest<SearchabilitiesV2Response>(Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("RetrieveSearchabilitiesV2 response data is malformed");
        }

        /// <summary>
        /// Gets a specific v2 searchability by name.
        /// </summary>
        /// <param name="name">Name of the searchability field.</param>
        /// <param name="section">Section name. Defaults to "Products".</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>SearchabilityV2 object representing the searchability.</returns>
        public async Task<SearchabilityV2> GetSearchabilityV2(string name, string section = "Products", CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name is required", nameof(name));
            }

            string url = CreateSearchabilitiesUrlV2(section, name);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            SearchabilityV2 result;
            try
            {
                result = await MakeHttpRequest<SearchabilityV2>(Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("GetSearchabilityV2 response data is malformed");
        }

        /// <summary>
        /// Create or update v2 searchabilities (batch operation).
        /// </summary>
        /// <param name="patchSearchabilitiesV2Request">Constructorio's v2 patch searchabilities request object.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's v2 Searchabilities response object.</returns>
        public async Task<SearchabilitiesV2Response> PatchSearchabilitiesV2(PatchSearchabilitiesV2Request patchSearchabilitiesV2Request, CancellationToken cancellationToken = default)
        {
            if (patchSearchabilitiesV2Request == null)
            {
                throw new ArgumentNullException(nameof(patchSearchabilitiesV2Request));
            }

            SearchabilitiesV2Response result;
            Hashtable postbody = new Hashtable
            {
                { "searchabilities", patchSearchabilitiesV2Request.Searchabilities }
            };

            try
            {
                var url = CreateSearchabilitiesUrlV2(patchSearchabilitiesV2Request.Section ?? "Products", queryParams: patchSearchabilitiesV2Request.GetRequestParameters());
                Dictionary<string, string> requestHeaders = patchSearchabilitiesV2Request.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest<SearchabilitiesV2Response>(Options, HttpMethodPatch, url, requestHeaders, postbody, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("PatchSearchabilitiesV2 response data is malformed");
        }

        /// <summary>
        /// Partially updates a specific v2 searchability by name.
        /// </summary>
        /// <param name="searchability">SearchabilityV2 fields to be updated.</param>
        /// <param name="section">Section name. Defaults to "Products".</param>
        /// <param name="skipRebuild">Whether to skip index rebuild.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Updated SearchabilityV2 object.</returns>
        public async Task<SearchabilityV2> PatchSearchabilityV2(SearchabilityV2 searchability, string section = "Products", bool? skipRebuild = null, CancellationToken cancellationToken = default)
        {
            if (searchability == null)
            {
                throw new ArgumentNullException(nameof(searchability));
            }

            if (string.IsNullOrEmpty(searchability.Name))
            {
                throw new ArgumentException("searchability.Name is required", nameof(searchability));
            }

            Hashtable queryParams = new Hashtable();
            if (skipRebuild.HasValue)
            {
                queryParams.Add("skip_rebuild", skipRebuild.Value.ToString().ToLower());
            }

            string url = CreateSearchabilitiesUrlV2(section, searchability.Name, queryParams);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            SearchabilityV2 result;
            try
            {
                result = await MakeHttpRequest<SearchabilityV2>(Options, HttpMethodPatch, url, requestHeaders, searchability, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("PatchSearchabilityV2 response data is malformed");
        }

        /// <summary>
        /// Delete v2 searchabilities (batch operation).
        /// </summary>
        /// <param name="deleteSearchabilitiesV2Request">Constructorio's v2 delete searchabilities request object.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Constructorio's v2 Searchabilities response object.</returns>
        public async Task<SearchabilitiesV2Response> DeleteSearchabilitiesV2(DeleteSearchabilitiesV2Request deleteSearchabilitiesV2Request, CancellationToken cancellationToken = default)
        {
            if (deleteSearchabilitiesV2Request == null)
            {
                throw new ArgumentNullException(nameof(deleteSearchabilitiesV2Request));
            }

            SearchabilitiesV2Response result;

            try
            {
                var url = CreateSearchabilitiesUrlV2(deleteSearchabilitiesV2Request.Section ?? "Products", queryParams: deleteSearchabilitiesV2Request.GetRequestParameters());
                Dictionary<string, string> requestHeaders = deleteSearchabilitiesV2Request.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest<SearchabilitiesV2Response>(Options, HttpMethod.Delete, url, requestHeaders, deleteSearchabilitiesV2Request.GetRequestBody(), cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("DeleteSearchabilitiesV2 response data is malformed");
        }

        /// <summary>
        /// Delete a specific v2 searchability by name.
        /// </summary>
        /// <param name="name">Name of the searchability field to delete.</param>
        /// <param name="section">Section name. Defaults to "Products".</param>
        /// <param name="skipRebuild">Whether to skip index rebuild.</param>
        /// <param name="cancellationToken">The cancellation token to abandon the request.</param>
        /// <returns>Deleted SearchabilityV2 object.</returns>
        public async Task<SearchabilityV2> DeleteSearchabilityV2(string name, string section = "Products", bool? skipRebuild = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name is required", nameof(name));
            }

            Hashtable queryParams = new Hashtable();
            if (skipRebuild.HasValue)
            {
                queryParams.Add("skip_rebuild", skipRebuild.Value.ToString().ToLower());
            }

            string url = CreateSearchabilitiesUrlV2(section, name, queryParams);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            SearchabilityV2 result;
            try
            {
                result = await MakeHttpRequest<SearchabilityV2>(Options, HttpMethod.Delete, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return result ?? throw new ConstructorException("DeleteSearchabilityV2 response data is malformed");
        }

        #endregion
    }
}