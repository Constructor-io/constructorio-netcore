using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Constructorio_NET.Models.Items;
using Constructorio_NET.Utils;
using Newtonsoft.Json;

namespace Constructorio_NET.Modules
{
    public class Items : Helpers
    {
        private readonly Hashtable Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="Items"/> class.
        /// Interface for catalog related API calls.
        /// </summary>
        /// <param name="options">Hashtable of options from Constructorio instantiation.</param>
        public Items(Hashtable options)
        {
            this.Options = options;
        }

        internal string CreateItemsUrl(string section, bool force = false, string notificationEmail = null, Dictionary<string, bool> omittedQueryParams = null)
        {
            List<string> paths = new List<string> { "v2", "items" };
            Hashtable queryParams = new Hashtable();
            if (force)
            {
                queryParams.Add("force", force);
            }

            if (!string.IsNullOrEmpty(section))
            {
                queryParams.Add("section", section);
            }

            if (!string.IsNullOrEmpty(notificationEmail))
            {
                queryParams.Add("notification_email", notificationEmail);
            }

            string url = MakeUrl(this.Options, paths, queryParams, omittedQueryParams);

            return url;
        }

        internal string CreateVariationsUrl(string section, bool force = false, string notificationEmail = null, Dictionary<string, bool> omittedQueryParams = null)
        {
            List<string> paths = new List<string> { "v2", "variations" };
            Hashtable queryParams = new Hashtable();
            if (force)
            {
                queryParams.Add("force", force);
            }

            if (!string.IsNullOrEmpty(section))
            {
                queryParams.Add("section", section);
            }

            if (!string.IsNullOrEmpty(notificationEmail))
            {
                queryParams.Add("notification_email", notificationEmail);
            }

            string url = MakeUrl(this.Options, paths, queryParams, omittedQueryParams);

            return url;
        }

        internal string CreateRetrieveItemsUrl(ItemsRequest req)
        {
            List<string> paths = new List<string> { "v2", "items" };
            Hashtable queryParams = req.GetRequestParameters();
            Dictionary<string, bool> omittedQueryParams = new Dictionary<string, bool>()
            {
                { "_dt", true },
                { "c", true },
            };
            string url = MakeUrl(this.Options, paths, queryParams, omittedQueryParams);

            return url;
        }

        internal string CreateRetrieveVariationsUrl(VariationsRequest req)
        {
            List<string> paths = new List<string> { "v2", "variations" };
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
        /// <param name="items">List of ConstructorItems to upload.</param>
        /// <param name="section">Section to upload items to.</param>
        /// <param name="force">Boolean to indicate whether or not to use force sync.</param>
        /// <param name="notificationEmail">Email to send failure notifications to.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<bool> CreateOrReplaceItems(List<ConstructorItem> items, string section, bool force = false, string notificationEmail = null)
        {
            string url;
            string result;

            try
            {
                url = CreateItemsUrl(section, force, notificationEmail);
                Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
                Hashtable requestBody = new Hashtable();
                requestBody.Add("items", items);
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest(this.Options, new HttpMethod("PUT"), url, requestHeaders, requestBody, null);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return true;
        }

        /// <summary>
        /// Send full catalog files to replace the current catalog.
        /// </summary>
        /// <param name="variations">List of ConstructorItems to upload.</param>
        /// <param name="section">Section to upload items to.</param>
        /// <param name="force">Boolean to indicate whether or not to use force sync.</param>
        /// <param name="notificationEmail">Email to send failure notifications to.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<bool> CreateOrReplaceVariations(List<ConstructorVariation> variations, string section, bool force = false, string notificationEmail = null)
        {
            string url;
            string result;

            try
            {
                url = CreateVariationsUrl(section, force, notificationEmail);
                Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
                Hashtable requestBody = new Hashtable();
                requestBody.Add("variations", variations);
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest(this.Options, new HttpMethod("PUT"), url, requestHeaders, requestBody, null);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return true;
        }

        /// <summary>
        /// Send full catalog files to replace the current catalog.
        /// </summary>
        /// <param name="items">List of ConstructorItems to upload.</param>
        /// <param name="section">Section to upload items to.</param>
        /// <param name="force">Boolean to indicate whether or not to use force sync.</param>
        /// <param name="notificationEmail">Email to send failure notifications to.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<bool> UpdateItems(List<ConstructorItem> items, string section, bool force = false, string notificationEmail = null)
        {
            string url;
            string result;

            try
            {
                url = CreateItemsUrl(section, force, notificationEmail);
                Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
                Hashtable requestBody = new Hashtable();
                requestBody.Add("items", items);
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest(this.Options, new HttpMethod("PATCH"), url, requestHeaders, requestBody, null);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return true;
        }

        /// <summary>
        /// Send full catalog files to replace the current catalog.
        /// </summary>
        /// <param name="Variations">List of ConstructorVariations to update.</param>
        /// <param name="section">Section to upload items to.</param>
        /// <param name="force">Boolean to indicate whether or not to use force sync.</param>
        /// <param name="notificationEmail">Email to send failure notifications to.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<bool> UpdateVariations(List<ConstructorVariation> Variations, string section, bool force = false, string notificationEmail = null)
        {
            string url;
            string result;

            try
            {
                url = CreateVariationsUrl(section, force, notificationEmail);
                Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
                Hashtable requestBody = new Hashtable();
                requestBody.Add("variations", Variations);
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest(this.Options, new HttpMethod("PATCH"), url, requestHeaders, requestBody, null);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return true;
        }

        /// <summary>
        /// Send full catalog files to replace the current catalog.
        /// </summary>
        /// <param name="items">List of ConstructorItems with only Item Id.</param>
        /// <param name="section">Section to upload items to.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<bool> DeleteItems(List<ConstructorItem> items, string section)
        {
            string url;
            string result;

            try
            {
                List<ConstructorItem> cleanedItems = items.Select((item) => new ConstructorItem(item.Id)).ToList();
                url = CreateItemsUrl(section);
                Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
                Hashtable requestBody = new Hashtable();
                requestBody.Add("items", cleanedItems);
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest(this.Options, new HttpMethod("DELETE"), url, requestHeaders, requestBody, null);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return true;
        }

        /// <summary>
        /// Send full catalog files to replace the current catalog.
        /// </summary>
        /// <param name="variations">List of ConstructorItems with only Item Id.</param>
        /// <param name="section">Section to upload items to.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<bool> DeleteVariations(List<ConstructorVariation> variations, string section)
        {
            string url;
            string result;

            try
            {
                List<ConstructorVariation> cleanedVariations = variations.Select((item) => new ConstructorVariation(item.Id)).ToList();
                url = CreateVariationsUrl(section);
                Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
                Hashtable requestBody = new Hashtable();
                requestBody.Add("variations", cleanedVariations);
                AddAuthHeaders(this.Options, requestHeaders);
                result = await MakeHttpRequest(this.Options, new HttpMethod("DELETE"), url, requestHeaders, requestBody, null);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            return true;
        }

        /// <summary>
        /// Send full catalog files to replace the current catalog.
        /// </summary>
        /// <param name="req">The Items request object.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<ItemsResponse> RetrieveItems(ItemsRequest req)
        {
            string url;
            string result;
            url = CreateRetrieveItemsUrl(req);
            Dictionary<string, string> requestHeaders = req.GetRequestHeaders();
            AddAuthHeaders(this.Options, requestHeaders);
            result = await MakeHttpRequest(this.Options, new HttpMethod("GET"), url, requestHeaders, null);

            if (result != null)
            {
                Console.WriteLine(result);
                JsonSerializerSettings settings = new JsonSerializerSettings() { ObjectCreationHandling = ObjectCreationHandling.Replace };
                return JsonConvert.DeserializeObject<ItemsResponse>(result, settings);
            }

            throw new ConstructorException("RetrieveItems response data is malformed");
        }

        /// <summary>
        /// Send full catalog files to replace the current catalog.
        /// </summary>
        /// <param name="rq">Variations request object</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<VariationsResponse> RetrieveVariations(VariationsRequest req)
        {
            string url;
            string result;
            url = CreateRetrieveVariationsUrl(req);
            Dictionary<string, string> requestHeaders = req.GetRequestHeaders();
            AddAuthHeaders(this.Options, requestHeaders);
            result = await MakeHttpRequest(this.Options, new HttpMethod("GET"), url, requestHeaders, null);

            if (result != null)
            {
                Console.WriteLine(result);
                JsonSerializerSettings settings = new JsonSerializerSettings() { ObjectCreationHandling = ObjectCreationHandling.Replace };
                return JsonConvert.DeserializeObject<VariationsResponse>(result, settings);
            }

            throw new ConstructorException("RetrieveVariations response data is malformed");
        }
    }
}