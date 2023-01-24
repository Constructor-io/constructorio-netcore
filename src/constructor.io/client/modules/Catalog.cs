using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
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
            url = CreateCatalogUrl(catalogRequest);
            url += "&patch_delta=true";
            Dictionary<string, string> requestHeaders = catalogRequest.GetRequestHeaders();
            AddAuthHeaders(this.Options, requestHeaders);
            result = await MakeHttpRequest(this.Options, new HttpMethod("PATCH"), url, requestHeaders, null, catalogRequest.Files);

            if (result != null)
            {
                return JsonConvert.DeserializeObject<CatalogResponse>(result);
            }

            throw new ConstructorException("PatchCatalog response data is malformed");
        }

        // Facets
        internal string CreateFacetUrl([Optional] string section, [Optional] string facetGroup, [Optional] Hashtable queryParams)
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
        public async Task<Facet> CreateFacetConfig(Facet facet, [Optional] string section)
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
        /// <param name="pageRequest">PageRequest object for pagination.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// <returns>List of Facets in a given section.</returns>
        public async Task<FacetGetAllResponse> GetAllFacetConfigs([Optional] PageRequest pageRequest, [Optional] string section)
        {
            string url;
            if (pageRequest != null)
            {
                url = CreateFacetUrl(section, queryParams: pageRequest.GetQueryParameters());
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
        public async Task<Facet> GetFacetConfig(string facetName, [Optional] string section)
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
        public async Task<List<Facet>> BatchPartiallyUpdateFacetConfigs(List<Facet> facetFieldsList, [Optional] string section)
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
        public async Task<Facet> PartiallyUpdateFacetConfig(Facet facetFields, [Optional] string section)
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
        public async Task<Facet> UpdateFacetConfig(Facet facet, [Optional] string section)
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
        public async Task<Facet> DeleteFacetConfig(string facetName, [Optional] string section)
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
        internal string CreateFacetOptionsUrl([Optional] string section, string facetGroupName, [Optional] string facetOptionValue, [Optional] Hashtable queryParams)
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
        public async Task<FacetOption> CreateFacetOption(FacetOption facetOption, string facetGroupName, [Optional] string section)
        {
            string url = CreateFacetOptionsUrl(section, facetGroupName);
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
        /// <param name="pageRequest">PageRequest object for pagination.</param>
        /// <param name="section">Section in which the facet is defined.</param>
        /// </summary>
        public async Task<FacetOptionsGetAllResponse> GetAllFacetOptions(string facetGroupName, [Optional] PageRequest pageRequest, [Optional] string section)
        {
            string url;
            if (pageRequest != null)
            {
                url = CreateFacetOptionsUrl(section, facetGroupName, queryParams: pageRequest.GetQueryParameters());
            }
            else
            {
                url = CreateFacetOptionsUrl(section, facetGroupName);
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
        public async Task<FacetOption> GetFacetOption(string facetOptionValue, string facetGroupName, [Optional] string section)
        {
            string url;
            url = CreateFacetOptionsUrl(section, facetGroupName, facetOptionValue);
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
        public async Task<List<FacetOption>> BatchCreateOrUpdateFacetOptions(List<FacetOption> facetOptions, string facetGroupName, [Optional] string section)
        {
            string url;
            url = CreateFacetOptionsUrl(section, facetGroupName);
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
        public async Task<FacetOption> ReplaceFacetOption(FacetOption facetOption, string facetGroupName, [Optional] string section)
        {
            string url;
            url = CreateFacetOptionsUrl(section, facetGroupName, facetOption.Value);
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
        public async Task<FacetOption> PartiallyUpdateFacetOption(FacetOption facetOption, string facetGroupName, [Optional] string section)
        {
            string url;
            url = CreateFacetOptionsUrl(section, facetGroupName, facetOption.Value);
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
        public async Task<FacetOption> DeleteFacetOption(string facetOptionValue, string facetGroupName, [Optional] string section)
        {
            string url = CreateFacetOptionsUrl(section, facetGroupName, facetOptionValue);
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
    }
}