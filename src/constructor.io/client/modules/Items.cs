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
    public class Items : Helpers
    {
        private static readonly JsonSerializer ObjectCreationHandlingReplaceJsonSerializer = JsonSerializer.Create(
            new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
            });
        private readonly Hashtable Options;

        /// <summary>
        /// Internal method to delete items from section using itemIds, multiple overloaded methods use this method.
        /// </summary>
        /// <param name="items">List of ConstructorItems with only Item Id.</param>
        /// <param name="section">Section to upload items to.</param>
        /// <param name="force">Boolean to indicate whether or not to use force sync.</param>
        /// <param name="notificationEmail">Email to send failure notifications to.</param>
        /// <param name="cancellationToken">The cancellation token for the HTTP request.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        internal async Task<bool> InternalDeleteItems(List<ConstructorItem> items, string section, bool force = false, string notificationEmail = null, CancellationToken cancellationToken = default)
        {
            try
            {
                List<ConstructorItem> cleanedItems = items.ConvertAll(item => new ConstructorItem(item.Id));
                var url = CreateItemsUrl(section, force, notificationEmail);
                Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
                Hashtable requestBody = new Hashtable();
                requestBody.Add("items", cleanedItems);
                AddAuthHeaders(this.Options, requestHeaders);
                await MakeHttpRequest(this.Options, HttpMethod.Delete, url, requestHeaders, requestBody, cancellationToken: cancellationToken).ConfigureAwait(false);
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Items"/> class.
        /// Interface for item/variation related API calls.
        /// </summary>
        /// <param name="options">Hashtable of options from Constructorio instantiation.</param>
        public Items(Hashtable options)
        {
            this.Options = options;
        }

        internal string CreateItemsUrl(string section, bool force = false, string notificationEmail = null, CatalogRequest.OnMissingStrategy onMissing = CatalogRequest.OnMissingStrategy.FAIL)
        {
            List<string> paths = new List<string>(capacity: 2) { "v2", "items" };
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

            if (onMissing != CatalogRequest.OnMissingStrategy.FAIL)
            {
                queryParams.Add(Constants.ON_MISSING, onMissing.ToString());
            }

            string url = MakeUrl(this.Options, paths, queryParams, OmitDtQueryParam);

            return url;
        }

        internal string CreateVariationsUrl(string section, bool force = false, string notificationEmail = null, CatalogRequest.OnMissingStrategy onMissing = CatalogRequest.OnMissingStrategy.FAIL)
        {
            List<string> paths = new List<string>(capacity: 2) { "v2", "variations" };
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

            if (onMissing != CatalogRequest.OnMissingStrategy.FAIL)
            {
                queryParams.Add(Constants.ON_MISSING, onMissing.ToString());
            }

            string url = MakeUrl(this.Options, paths, queryParams, OmitDtQueryParam);

            return url;
        }

        internal string CreateRetrieveItemsUrl(ItemsRequest req)
        {
            List<string> paths = new List<string>(capacity: 2) { "v2", "items" };
            Hashtable queryParams = req.GetRequestParameters();
            string url = MakeUrl(this.Options, paths, queryParams, OmitDtQueryParam);

            return url;
        }

        internal string CreateRetrieveVariationsUrl(VariationsRequest req)
        {
            List<string> paths = new List<string>(capacity: 2) { "v2", "variations" };
            Hashtable queryParams = req.GetRequestParameters();
            string url = MakeUrl(this.Options, paths, queryParams, OmitDtQueryParam);
            return url;
        }

        /// <summary>
        /// Creates or replaces items in a section.
        /// </summary>
        /// <param name="items">List of ConstructorItems to upload.</param>
        /// <param name="section">Section to upload items to.</param>
        /// <param name="force">Boolean to indicate whether or not to use force sync.</param>
        /// <param name="notificationEmail">Email to send failure notifications to.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<bool> CreateOrReplaceItems(List<ConstructorItem> items, string section, bool force = false, string notificationEmail = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = CreateItemsUrl(section, force, notificationEmail);
                Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
                Hashtable requestBody = new Hashtable();
                requestBody.Add("items", items);
                AddAuthHeaders(this.Options, requestHeaders);
                await MakeHttpRequest(this.Options, HttpMethod.Put, url, requestHeaders, requestBody, cancellationToken: cancellationToken).ConfigureAwait(false);
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

        /// <summary>
        /// Creates or Replaces variations in a section.
        /// </summary>
        /// <param name="variations">List of ConstructorItems to upload.</param>
        /// <param name="section">Section to upload items to.</param>
        /// <param name="force">Boolean to indicate whether or not to use force sync.</param>
        /// <param name="notificationEmail">Email to send failure notifications to.</param>
        /// <param name="cancellationToken">The cancellation token for the HTTP request.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<bool> CreateOrReplaceVariations(List<ConstructorVariation> variations, string section, bool force = false, string notificationEmail = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = CreateVariationsUrl(section, force, notificationEmail);
                Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
                Hashtable requestBody = new Hashtable();
                requestBody.Add("variations", variations);
                AddAuthHeaders(this.Options, requestHeaders);
                await MakeHttpRequest(this.Options, HttpMethod.Put, url, requestHeaders, requestBody, cancellationToken: cancellationToken).ConfigureAwait(false);
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

        /// <summary>
        /// Updates items in a section.
        /// </summary>
        /// <param name="items">List of ConstructorItems to upload.</param>
        /// <param name="section">Section to upload items to.</param>
        /// <param name="force">Boolean to indicate whether or not to use force sync.</param>
        /// <param name="notificationEmail">Email to send failure notifications to.</param>
        /// <param name="onMissing">Either "FAIL", "CREATE", or "IGNORE". Indicating what to do when missing items are present in an update. "FAIL" fails the ingestion. "CREATE" creates the missing items. "IGNORE" ignores the missing items. Defaults to "FAIL".</param>
        /// <param name="cancellationToken">The cancellation token for the HTTP request.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<bool> UpdateItems(List<ConstructorItem> items, string section, bool force = false, string notificationEmail = null, CatalogRequest.OnMissingStrategy onMissing = CatalogRequest.OnMissingStrategy.FAIL, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = CreateItemsUrl(section, force, notificationEmail, onMissing);
                Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
                Hashtable requestBody = new Hashtable();
                requestBody.Add("items", items);
                AddAuthHeaders(this.Options, requestHeaders);
                await MakeHttpRequest(this.Options, HttpMethodPatch, url, requestHeaders, requestBody, cancellationToken: cancellationToken).ConfigureAwait(false);
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

        /// <summary>
        /// Updates variations in a section.
        /// </summary>
        /// <param name="Variations">List of ConstructorVariations to update.</param>
        /// <param name="section">Section to upload variations to.</param>
        /// <param name="force">Boolean to indicate whether or not to use force sync.</param>
        /// <param name="notificationEmail">Email to send failure notifications to.</param>
        /// <param name="onMissing">Either "FAIL", "CREATE", or "IGNORE". Indicating what to do when missing items are present in an update. "FAIL" fails the ingestion. "CREATE" creates the missing items. "IGNORE" ignores the missing items. Defaults to "FAIL".</param>
        /// <param name="cancellationToken">The cancellation token for the HTTP request.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<bool> UpdateVariations(List<ConstructorVariation> Variations, string section, bool force = false, string notificationEmail = null, CatalogRequest.OnMissingStrategy onMissing = CatalogRequest.OnMissingStrategy.FAIL, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = CreateVariationsUrl(section, force, notificationEmail, onMissing);
                Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
                Hashtable requestBody = new Hashtable();
                requestBody.Add("variations", Variations);
                AddAuthHeaders(this.Options, requestHeaders);
                await MakeHttpRequest(this.Options, HttpMethodPatch, url, requestHeaders, requestBody, cancellationToken: cancellationToken).ConfigureAwait(false);
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

        /// <summary>
        /// Deletes items from section using itemIds.
        /// </summary>
        /// <param name="items">List of ConstructorItems with only Item Id.</param>
        /// <param name="section">Section to upload items to.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<bool> DeleteItems(List<ConstructorItem> items, string section)
        {
            return await this.InternalDeleteItems(items, section, false, null);
        }
        
        /// <summary>
        /// Deletes items from section using itemIds.
        /// </summary>
        /// <param name="items">List of ConstructorItems with only Item Id.</param>
        /// <param name="section">Section to upload items to.</param>
        /// <param name="cancellationToken">The cancellation token for the HTTP request.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public Task<bool> DeleteItems(List<ConstructorItem> items, string section, CancellationToken cancellationToken = default)
        {
            return InternalDeleteItems(items, section, false, null, cancellationToken);
        }

        /// <summary>
        /// Deletes items from section using itemIds.
        /// </summary>
        /// <param name="items">List of ConstructorItems with only Item Id.</param>
        /// <param name="section">Section to upload items to.</param>
        /// <param name="force">Boolean to indicate whether or not to use force sync.</param>
        /// <param name="notificationEmail">Email to send failure notifications to.</param>
        /// <param name="cancellationToken">The cancellation token for the HTTP request.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public Task<bool> DeleteItems(List<ConstructorItem> items, string section, bool force = false, string notificationEmail = null, CancellationToken cancellationToken = default)
        {
            return InternalDeleteItems(items, section, force, notificationEmail, cancellationToken);
        }


        /// <summary>
        /// Deletes variations from a section.
        /// </summary>
        /// <param name="variations">List of ConstructorItems with only Item Id.</param>
        /// <param name="section">Section to upload items to.</param>
        /// <param name="cancellationToken">The cancellation token for the HTTP request.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<bool> DeleteVariations(List<ConstructorVariation> variations, string section, CancellationToken cancellationToken = default)
        {
            try
            {
                List<ConstructorVariation> cleanedVariations = variations.ConvertAll((item) => new ConstructorVariation(item.Id));
                var url = CreateVariationsUrl(section);
                Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
                Hashtable requestBody = new Hashtable();
                requestBody.Add("variations", cleanedVariations);
                AddAuthHeaders(this.Options, requestHeaders);
                await MakeHttpRequest(this.Options, HttpMethod.Delete, url, requestHeaders, requestBody, cancellationToken: cancellationToken).ConfigureAwait(false);
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

        /// <summary>
        /// Retrieves items or specific items.
        /// </summary>
        /// <param name="req">The Items request object.</param>
        /// <param name="cancellationToken">The cancellation token for the HTTP request.</param>
        /// <returns>Constructorio's Items response object.</returns>
        public async Task<ItemsResponse> RetrieveItems(ItemsRequest req, CancellationToken cancellationToken = default)
        {
            var url = CreateRetrieveItemsUrl(req);
            Dictionary<string, string> requestHeaders = req.GetRequestHeaders();
            AddAuthHeaders(this.Options, requestHeaders);
            var result = await MakeHttpRequest<ItemsResponse>(Options, HttpMethod.Get, url, requestHeaders, jsonSerializer: ObjectCreationHandlingReplaceJsonSerializer, cancellationToken: cancellationToken).ConfigureAwait(false);

            return result ?? throw new ConstructorException("RetrieveItems response data is malformed");
        }

        /// <summary>
        /// Retrieves variations or specific variations.
        /// </summary>
        /// <param name="req">Variations request object</param>
        /// <param name="cancellationToken">The cancellation token for the HTTP request.</param>
        /// <returns>Constructorio's catalog response object.</returns>
        public async Task<VariationsResponse> RetrieveVariations(VariationsRequest req, CancellationToken cancellationToken = default)
        {
            var url = CreateRetrieveVariationsUrl(req);
            Dictionary<string, string> requestHeaders = req.GetRequestHeaders();
            AddAuthHeaders(this.Options, requestHeaders);
            var result = await MakeHttpRequest<VariationsResponse>(Options, HttpMethod.Get, url, requestHeaders, jsonSerializer: ObjectCreationHandlingReplaceJsonSerializer, cancellationToken: cancellationToken).ConfigureAwait(false);

            return result ?? throw new ConstructorException("RetrieveVariations response data is malformed");
        }
    }
}