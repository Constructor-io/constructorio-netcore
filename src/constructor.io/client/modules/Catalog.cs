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
            Dictionary<string, bool> omittedQueryParams = new Dictionary<string, bool>()
            {
                { "_dt", true },
                { "c", true },
            };
            string url = MakeUrl(this.Options, paths, queryParams, omittedQueryParams);

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
            Dictionary<string, bool> omittedQueryParams = new Dictionary<string, bool>()
            {
                { "_dt", true },
            };
            string url = MakeUrl(this.Options, paths, queryParams, omittedQueryParams);

            return url;
        }

        internal string CreateRetrieveSearchabilitiesUrl(RetrieveSearchabilitiesRequest req)
        {
            List<string> paths = new List<string> { "v1", "searchabilities" };
            Hashtable queryParams = req.GetRequestParameters();
            Dictionary<string, bool> omittedQueryParams = new Dictionary<string, bool>()
            {
                { "_dt", true },
                { "c", true },
            };
            string url = MakeUrl(this.Options, paths, queryParams, omittedQueryParams);

            return url;
        }

        internal string CreatePatchSearchabilitiesUrl(PatchSearchabilitiesRequest req)
        {
            List<string> paths = new List<string> { "v1", "searchabilities" };
            Hashtable queryParams = req.GetRequestParameters();
            Dictionary<string, bool> omittedQueryParams = new Dictionary<string, bool>()
            {
                { "_dt", true },
                { "c", true },
            };
            string url = MakeUrl(this.Options, paths, queryParams, omittedQueryParams);

            return url;
        }

        /// <summary>
        /// Send full catalog files to replace the current catalog.
        /// </summary>
        /// <param name="catalogRequest">Constructorio's catalog request object.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<CatalogResponse> ReplaceCatalog(CatalogRequest catalogRequest)
        {
            string url;
            string result;

            try
            {
                url = CreateCatalogUrl(catalogRequest);
                Dictionary<string, string> requestHeaders = catalogRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest(this.Options, new HttpMethod("PUT"), url, requestHeaders, null, catalogRequest.Files);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<CatalogResponse>(result);
            }

            throw new ConstructorException("ReplaceCatalog response data is malformed");
        }

        /// <summary>
        /// Send full catalog files to update the current catalog.
        /// </summary>
        /// <param name="catalogRequest">Constructorio's catalog request object.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<CatalogResponse> UpdateCatalog(CatalogRequest catalogRequest)
        {
            string url;
            string result;
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            try
            {
                url = CreateCatalogUrl(catalogRequest);
                requestHeaders = catalogRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest(this.Options, new HttpMethod("PATCH"), url, requestHeaders, null, catalogRequest.Files);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<CatalogResponse>(result);
            }

            throw new ConstructorException("UpdateCatalog response data is malformed");
        }

        /// <summary>
        /// Send full catalog files to patch update the current catalog.
        /// </summary>
        /// <param name="catalogRequest">Constructorio's catalog request object.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<CatalogResponse> PatchCatalog(CatalogRequest catalogRequest)
        {
            string url;
            string result;

            try
            {
                url = CreateCatalogUrl(catalogRequest);
                url += "&patch_delta=true";
                Dictionary<string, string> requestHeaders = catalogRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest(this.Options, new HttpMethod("PATCH"), url, requestHeaders, null, catalogRequest.Files);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<CatalogResponse>(result);
            }

            throw new ConstructorException("PatchCatalog response data is malformed");
        }

        /// <summary>
        /// Add item group to a catalog.
        /// </summary>
        /// <param name="itemGroupsRequest">Constructorio's item groups request object.</param>
        /// <returns>Constructorio's item group.</returns>
        public async Task<ConstructorItemGroup> AddItemGroup(ItemGroupsRequest itemGroupsRequest)
        {
            string url;
            string result;

            try
            {
                ConstructorItemGroup itemGroup = itemGroupsRequest.ItemGroups[0];
                url = CreateItemGroupsUrl(itemGroupsRequest, new List<string> { itemGroup.Id });
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
                result = await MakeHttpRequest(this.Options, new HttpMethod("PUT"), url, requestHeaders, requestBody);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<ConstructorItemGroup>(result);
            }

            throw new ConstructorException("AddItemGroup response data is malformed");
        }

        /// <summary>
        /// Update item group in a catalog.
        /// </summary>
        /// <param name="itemGroupsRequest">Constructorio's item groups request object.</param>
        /// <returns>Constructorio's item group.</returns>
        public async Task<ConstructorItemGroup> UpdateItemGroup(ItemGroupsRequest itemGroupsRequest)
        {
            string url;
            string result;

            try
            {
                ConstructorItemGroup itemGroup = itemGroupsRequest.ItemGroups[0];
                url = CreateItemGroupsUrl(itemGroupsRequest, new List<string> { itemGroup.Id });
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
                result = await MakeHttpRequest(this.Options, new HttpMethod("PUT"), url, requestHeaders, requestBody);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<ConstructorItemGroup>(result);
            }

            throw new ConstructorException("UpdateItemGroup response data is malformed");
        }

        /// <summary>
        /// Add item group(s) to a catalog (limit of 1,000).
        /// </summary>
        /// <param name="itemGroupsRequest">Constructorio's item groups request object.</param>
        /// <returns>Constructorio's item group response object.</returns>
        public async Task<ItemGroupsResponse> AddItemGroups(ItemGroupsRequest itemGroupsRequest)
        {
            string url;
            string result;

            try
            {
                url = CreateItemGroupsUrl(itemGroupsRequest);
                Hashtable requestBody = new Hashtable
                {
                    { "item_groups", itemGroupsRequest.ItemGroups }
                };
                Dictionary<string, string> requestHeaders = itemGroupsRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest(this.Options, new HttpMethod("POST"), url, requestHeaders, requestBody);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<ItemGroupsResponse>(result);
            }

            throw new ConstructorException("AddItemGroups response data is malformed");
        }

        /// <summary>
        /// Update item group(s) in a catalog (limit of 1,000).
        /// </summary>
        /// <param name="itemGroupsRequest">Constructorio's item groups request object.</param>
        /// <returns>Constructorio's item group response object.</returns>
        public async Task<ItemGroupsResponse> UpdateItemGroups(ItemGroupsRequest itemGroupsRequest)
        {
            string url;
            string result;

            try
            {
                url = CreateItemGroupsUrl(itemGroupsRequest);
                Hashtable requestBody = new Hashtable
                {
                    { "item_groups", itemGroupsRequest.ItemGroups }
                };
                Dictionary<string, string> requestHeaders = itemGroupsRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest(this.Options, new HttpMethod("PATCH"), url, requestHeaders, requestBody);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<ItemGroupsResponse>(result);
            }

            throw new ConstructorException("UpdateItemGroups response data is malformed");
        }

        /// <summary>
        /// Retrieves item group(s) in a tree structure.
        /// </summary>
        /// <param name="itemGroupsRequest">Constructorio's item groups request object.</param>
        /// <returns>Constructorio's item group response object.</returns>
        public async Task<ItemGroupsGetResponse> GetItemGroup(ItemGroupsRequest itemGroupsRequest)
        {
            string url;
            string result;

            try
            {
                if (itemGroupsRequest.ItemGroupId != null)
                {
                    url = CreateItemGroupsUrl(itemGroupsRequest, new List<string> { itemGroupsRequest.ItemGroupId });
                }
                else
                {
                    url = CreateItemGroupsUrl(itemGroupsRequest);
                }

                Hashtable requestBody = new Hashtable();
                Dictionary<string, string> requestHeaders = itemGroupsRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest(this.Options, new HttpMethod("GET"), url, requestHeaders, requestBody);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<ItemGroupsGetResponse>(result);
            }

            throw new ConstructorException("GetItemGroup response data is malformed");
        }

        /// <summary>
        /// Delete all item groups.
        /// </summary>
        /// <param name="itemGroupsRequest">Constructorio's item groups request object.</param>
        /// <returns>Constructorio's confirmation message.</returns>
        public async Task<string> DeleteItemGroups(ItemGroupsRequest itemGroupsRequest)
        {
            string url;
            string result;

            try
            {
                url = CreateItemGroupsUrl(itemGroupsRequest);
                Dictionary<string, string> requestHeaders = itemGroupsRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest(this.Options, new HttpMethod("DELETE"), url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<ItemGroupsResponse>(result).Message;
            }

            throw new ConstructorException("DeleteItemGroups response data is malformed");
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

            Dictionary<string, bool> omittedQueryParams = new Dictionary<string, bool>()
            {
                { "_dt", true },
                { "c", true },
            };

            string url = MakeUrl(this.Options, paths, queryParams, omittedQueryParams);

            return url;
        }

        /// <summary>
        /// Creates a Facet Configuration.
        /// </summary>
        /// <param name="facet">New Facet Configuration to be created.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <returns>Facet object representing the facet created.</returns>
        public async Task<Facet> CreateFacetConfig(Facet facet, string section = "Products")
        {
            string url = CreateFacetUrl(section);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, HttpMethod.Post, url, requestHeaders, facet);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<Facet>(result);
            }

            throw new ConstructorException("CreateFacetConfig response data is malformed");
        }

        /// <summary>
        /// Gets all facets in a particular section.
        /// </summary>
        /// <param name="paginationOptions">PaginationOptions object for pagination.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <returns>List of Facets in a given section.</returns>
        public async Task<FacetGetAllResponse> GetAllFacetConfigs(PaginationOptions paginationOptions = null, string section = "Products")
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

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, HttpMethod.Get, url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<FacetGetAllResponse>(result);
            }

            throw new ConstructorException("GetAllFacetConfigs response data is malformed");
        }

        /// <summary>
        /// Gets a facet given the facet name and section.
        /// <param name="facetName">Name of the facet.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// </summary>
        public async Task<Facet> GetFacetConfig(string facetName, string section = "Products")
        {
            string url = CreateFacetUrl(section, facetName);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, HttpMethod.Get, url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<Facet>(result);
            }

            throw new ConstructorException("GetFacetConfig response data is malformed");
        }

        /// <summary>
        /// Partially updates a list of facet configurations.
        /// <param name="facetFieldsList">List of facets fields to be updated.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// </summary>
        public async Task<List<Facet>> BatchPartiallyUpdateFacetConfigs(List<Facet> facetFieldsList, string section = "Products")
        {
            string url = CreateFacetUrl(section);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, new HttpMethod("PATCH"), url, requestHeaders, facetFieldsList);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<List<Facet>>(result);
            }

            throw new ConstructorException("BatchPartiallyUpdateFacetConfigs response data is malformed");
        }

        /// <summary>
        /// Partially updates a specifc facet configuration by facet name.
        /// <param name="facetFields">Facets fields to be updated.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// </summary>
        public async Task<Facet> PartiallyUpdateFacetConfig(Facet facetFields, string section = "Products")
        {
            string url = CreateFacetUrl(section, facetFields.Name);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, new HttpMethod("PATCH"), url, requestHeaders, facetFields);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<Facet>(result);
            }

            throw new ConstructorException("PartiallyUpdateFacetConfig response data is malformed");
        }

        /// <summary>
        /// Replace an existing facet configuration by facet name.
        /// <param name="facet">New Facet Configuration to be created.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// </summary>
        public async Task<Facet> UpdateFacetConfig(Facet facet, string section = "Products")
        {
            string url = CreateFacetUrl(section, facet.Name);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, HttpMethod.Put, url, requestHeaders, facet);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<Facet>(result);
            }

            throw new ConstructorException("UpdateFacetConfig response data is malformed");
        }

        /// <summary>
        /// Deletes a Facet Configuration.
        /// </summary>
        /// <param name="facetName">Name of the facet configuration/facet group.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <returns>Facet object representing the facet configuration deleted</returns>
        public async Task<Facet> DeleteFacetConfig(string facetName, string section = "Products")
        {
            string url = CreateFacetUrl(section, facetName);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, HttpMethod.Delete, url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<Facet>(result);
            }

            throw new ConstructorException("DeleteFacetConfig response data is malformed");
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

            Dictionary<string, bool> omittedQueryParams = new Dictionary<string, bool>()
            {
                { "_dt", true },
                { "c", true },
            };

            string url = MakeUrl(this.Options, paths, queryParams, omittedQueryParams);

            return url;
        }

        /// <summary>
        /// Creates a Facet Option given a facet group name.
        /// <param name="facetOption">New Facet Option to be created.</param>
        /// <param name="facetGroupName">Facet Group where the Facet Option should be created.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// </summary>
        public async Task<FacetOption> CreateFacetOption(FacetOption facetOption, string facetGroupName, string section = "Products")
        {
            string url = CreateFacetOptionsUrl(facetGroupName, section);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, HttpMethod.Post, url, requestHeaders, facetOption);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<FacetOption>(result);
            }

            throw new ConstructorException("CreateFacetOption response data is malformed");
        }

        /// <summary>
        /// Gets all Facet Options in a facet group.
        /// <param name="facetGroupName">Facet Group to retrieve the Facet Options from.</param>
        /// <param name="paginationOptions">PaginationOptions object for pagination.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// </summary>
        public async Task<FacetOptionsGetAllResponse> GetAllFacetOptions(string facetGroupName, PaginationOptions paginationOptions = null, string section = "Products")
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

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, HttpMethod.Get, url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<FacetOptionsGetAllResponse>(result);
            }

            throw new ConstructorException("GetAllFacetOptions response data is malformed");
        }

        /// <summary>
        /// Gets a specific Facet Option given the facet option value and facet group.
        /// <param name="facetOptionValue">Facet Option value to identify the Facet Option to be retrieved.</param>
        /// <param name="facetGroupName">Facet Group where the Facet Option resides.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// </summary>
        public async Task<FacetOption> GetFacetOption(string facetOptionValue, string facetGroupName, string section = "Products")
        {
            string url;
            url = CreateFacetOptionsUrl(facetGroupName, section, facetOptionValue);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, HttpMethod.Get, url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<FacetOption>(result);
            }

            throw new ConstructorException("GetFacetOption response data is malformed");
        }

        /// <summary>
        /// Creates the Facet Options if they don't exists, or partially updates them if they do.
        /// <param name="facetOptions">List of Facet Options to be created or partially updated.</param>
        /// <param name="facetGroupName">Facet Group where the Facet Options reside.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// </summary>
        public async Task<List<FacetOption>> BatchCreateOrUpdateFacetOptions(List<FacetOption> facetOptions, string facetGroupName, string section = "Products")
        {
            string url;
            url = CreateFacetOptionsUrl(facetGroupName, section);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, new HttpMethod("PATCH"), url, requestHeaders, facetOptions);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<List<FacetOption>>(result);
            }

            throw new ConstructorException("BatchCreateOrUpdateFacetOptions response data is malformed");
        }

        /// <summary>
        /// Replaces an existing Facet Option with a new one.
        /// <param name="facetOption">New Facet Option.</param>
        /// <param name="facetGroupName">Facet Group where the Facet Option resides.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// </summary>
        public async Task<FacetOption> ReplaceFacetOption(FacetOption facetOption, string facetGroupName, string section = "Products")
        {
            string url;
            url = CreateFacetOptionsUrl(facetGroupName, section, facetOption.Value);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, HttpMethod.Put, url, requestHeaders, facetOption);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<FacetOption>(result);
            }

            throw new ConstructorException("ReplaceFacetOption response data is malformed");
        }

        /// <summary>
        /// Partially updates an existing Facet Option.
        /// <param name="facetOption">Facet Option values for partial updating.</param>
        /// <param name="facetGroupName">Facet Group where the Facet Option resides.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// </summary>
        public async Task<FacetOption> PartiallyUpdateFacetOption(FacetOption facetOption, string facetGroupName, string section = "Products")
        {
            string url;
            url = CreateFacetOptionsUrl(facetGroupName, section, facetOption.Value);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, new HttpMethod("PATCH"), url, requestHeaders, facetOption);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<FacetOption>(result);
            }

            throw new ConstructorException("PartiallyUpdateFacetOption response data is malformed");
        }

        /// <summary>
        /// Deletes a particular Facet Option given the Facet Option Value and Facet Group.
        /// <param name="facetOptionValue">Facet Option value to identify the Facet Option to be deleted.</param>
        /// <param name="facetGroupName">Facet Group where the Facet Option resides.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// </summary>
        public async Task<FacetOption> DeleteFacetOption(string facetOptionValue, string facetGroupName, string section = "Products")
        {
            string url = CreateFacetOptionsUrl(facetGroupName, section, facetOptionValue);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, HttpMethod.Delete, url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<FacetOption>(result);
            }

            throw new ConstructorException("DeleteFacetOption response data is malformed");
        }

        /// <summary>
        /// Retrieves searchabilities with options for filtering, paginating.
        /// </summary>
        /// <param name="retrieveSearchabilitiesRequest">Constructorio's retrieve searchabilities request object.</param>
        /// <returns>Constructorio's Searchability response object.</returns>
        public async Task<SearchabilitiesResponse> RetrieveSearchabilities(RetrieveSearchabilitiesRequest retrieveSearchabilitiesRequest)
        {
            string url;
            string result;

            try
            {
                url = CreateRetrieveSearchabilitiesUrl(retrieveSearchabilitiesRequest);
                Dictionary<string, string> requestHeaders = retrieveSearchabilitiesRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest(this.Options, new HttpMethod("GET"), url, requestHeaders, null, null);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<SearchabilitiesResponse>(result);
            }

            throw new ConstructorException("RetrieveSearchabilities response data is malformed");
        }

        /// <summary>
        /// Send single or multiple searchabilities to create or modify.
        /// </summary>
        /// <param name="patchSearchabilitiesRequest">Constructorio's patch searchabilities request object.</param>
        /// <returns>Constructorio's Searchability response object.</returns>
        public async Task<SearchabilitiesResponse> PatchSearchabilities(PatchSearchabilitiesRequest patchSearchabilitiesRequest)
        {
            string url;
            string result;
            Hashtable postbody = new Hashtable
            {
                { "searchabilities", patchSearchabilitiesRequest.Searchabilities }
            };

            try
            {
                url = CreatePatchSearchabilitiesUrl(patchSearchabilitiesRequest);
                Dictionary<string, string> requestHeaders = patchSearchabilitiesRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest(this.Options, new HttpMethod("PATCH"), url, requestHeaders, postbody, null);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<SearchabilitiesResponse>(result);
            }

            throw new ConstructorException("PatchSearchabilities response data is malformed");
        }

        // Sort Options
        internal string CreateSortOptionsUrl(SortOptionsRequest req)
        {
            List<string> paths = new List<string> { "v1", "sort_options" };
            Hashtable queryParams = req.GetRequestParameters();

            bool toFilterBySortBy = req.SortBy != null;

            if (toFilterBySortBy)
            {
                queryParams.Add("sort_by", req.SortBy);
            }

            Dictionary<string, bool> omittedQueryParams = new Dictionary<string, bool>()
            {
                { "_dt", true },
                { "c", true },
            };
            string url = MakeUrl(this.Options, paths, queryParams, omittedQueryParams);

            return url;
        }

        /// <summary>
        /// Retrieves a list of all Sort Options.
        /// Specify an optional SortBy property to retrieve a specific sort option.
        /// </summary>
        /// <param name="sortOptionsRequest">Constructorio's <see cref="SortOptionsRequest"/> object model.</param>
        /// <param name="sortOptionsRequest.SortBy">A sort_by property to retrieve a specific Sort Option.</param>
        /// <returns>Constructorio's <see cref="SortOptionList"/> object.</returns>
        public async Task<SortOptionList> RetrieveSortOptions(SortOptionsRequest sortOptionsRequest)
        {
            string url = CreateSortOptionsUrl(sortOptionsRequest);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, HttpMethod.Get, url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<SortOptionList>(result);
            }

            throw new ConstructorException("RetrieveSortOptions response data is malformed");
        }

        /// <summary>
        /// Replaces all existing Sort Options with a new list of Sort Options.
        /// At most 50 Sort Options can be provided.
        /// </summary>
        /// <param name="sortOptionsListRequest">Constructorio's <see cref="SortOptionsListRequest"/> object model.</param>
        /// <returns>Constructorio's <see cref="SortOptionList"/> object.</returns>
        public async Task<SortOptionList> SetSortOptions(SortOptionsListRequest sortOptionsListRequest)
        {
            string url = CreateSortOptionsUrl(sortOptionsListRequest);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, HttpMethod.Put, url, requestHeaders, sortOptionsListRequest.GetSortOptionsList());
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<SortOptionList>(result);
            }

            throw new ConstructorException("SetSortOptions response data is malformed");
        }

        /// <summary>
        /// Deletes a list of Sort Options, identified by sort_by and sort_order.
        /// </summary>
        /// <param name="sortOptionsListRequest">Constructorio's <see cref="SortOptionsListRequest"/> object model.</param>
        /// <returns>True if delete request succeeds.</returns>
        public async Task<bool> DeleteSortOptions(SortOptionsListRequest sortOptionsListRequest)
        {
            string url = CreateSortOptionsUrl(sortOptionsListRequest);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, HttpMethod.Delete, url, requestHeaders, sortOptionsListRequest.GetSortOptionListForDeletion());
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

            Dictionary<string, bool> omittedQueryParams = new Dictionary<string, bool>()
            {
                { "_dt", true },
                { "c", true },
            };
            string url = MakeUrl(this.Options, paths, queryParams, omittedQueryParams);

            return url;
        }

        /// <summary>
        /// Creates a Sort Option.
        /// </summary>
        /// <param name="sortOptionsSingleRequest">Constructorio's <see cref="SortOptionsSingleRequest"/> object model.</param>
        /// <returns>Constructorio's <see cref="SortOption"/> object.</returns>
        public async Task<SortOption> CreateSortOption(SortOptionsSingleRequest sortOptionsSingleRequest)
        {
            string url = CreateSortOptionUrl(sortOptionsSingleRequest);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, HttpMethod.Post, url, requestHeaders, sortOptionsSingleRequest.SortOption);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<SortOption>(result);
            }

            throw new ConstructorException("CreateSortOption response data is malformed");
        }

        /// <summary>
        /// Creates or Replace a Sort Option.
        /// </summary>
        /// <param name="sortOptionsSingleRequest">Constructorio's <see cref="SortOptionsSingleRequest"/> object model.</param>
        /// <returns>Constructorio's <see cref="SortOption"/> object.</returns>
        public async Task<SortOption> CreateOrReplaceSortOption(SortOptionsSingleRequest sortOptionsSingleRequest)
        {
            if (sortOptionsSingleRequest.SortOption.SortBy == null)
            {
                throw new ConstructorException("SortBy is a required property for SortOptionsSingleRequest.SortOption.");
            }

            string url = CreateSortOptionUrl(sortOptionsSingleRequest, sortOptionsSingleRequest.SortOption.SortBy, sortOptionsSingleRequest.SortOption.SortOrder);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, HttpMethod.Put, url, requestHeaders, sortOptionsSingleRequest.GetSortOptionDelta());
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<SortOption>(result);
            }

            throw new ConstructorException("CreateOrReplaceSortOption response data is malformed");
        }

        /// <summary>
        /// Updates a Sort Option.
        /// </summary>
        /// <param name="sortOptionsSingleRequest">Constructorio's <see cref="SortOptionsSingleRequest"/> object model.</param>
        /// <returns>Constructorio's <see cref="SortOption"/> object.</returns>
        public async Task<SortOption> UpdateSortOption(SortOptionsSingleRequest sortOptionsSingleRequest)
        {
            if (sortOptionsSingleRequest.SortOption.SortBy == null)
            {
                throw new ConstructorException("SortBy is a required property for SortOptionsSingleRequest.SortOption.");
            }

            string url = CreateSortOptionUrl(sortOptionsSingleRequest, sortOptionsSingleRequest.SortOption.SortBy, sortOptionsSingleRequest.SortOption.SortOrder);
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            AddAuthHeaders(this.Options, requestHeaders);

            string result;
            try
            {
                result = await MakeHttpRequest(this.Options, new HttpMethod("PATCH"), url, requestHeaders, sortOptionsSingleRequest.GetSortOptionDelta());
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (result != null)
            {
                return JsonConvert.DeserializeObject<SortOption>(result);
            }

            throw new ConstructorException("UpdateSortOption response data is malformed");
        }
    }
}