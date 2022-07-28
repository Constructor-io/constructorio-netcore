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
    }
}