using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using Newtonsoft.Json;

namespace Constructorio_NET
{
    public class Autocomplete : Helpers
    {
        private Hashtable Options;

        /// <summary>
        /// Interface for autocomplete related API calls
        /// </summary>
        internal Autocomplete(Hashtable options)
        {
            this.Options = options;
        }
        internal string CreateAutocompleteUrl(AutocompleteRequest req)
        {
            Hashtable queryParams = req.GetUrlParameters();
            List<string> paths = new List<string> { "autocomplete", req.Query };

            return Helpers.MakeUrl(this.Options, paths, queryParams);
        }

        /// <summary>
        /// Retrieve Autocomplete results from API
        /// </summary>
        /// <param name="AutocompleteRequest">Constructorio's request object</param>
        /// <returns>Constructorio's response object</returns>
        public AutocompleteResponse GetAutocompleteResults(AutocompleteRequest recommendationsRequest)
        {
            string url;
            Task<string> task;
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            try
            {
                url = CreateAutocompleteUrl(recommendationsRequest);
                requestHeaders = recommendationsRequest.GetRequestHeaders();
                task = Helpers.MakeHttpRequest(HttpMethod.Get, url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (task.Result != null)
            {
                return JsonConvert.DeserializeObject<AutocompleteResponse>(task.Result);
            }

            throw new ConstructorException("GetAutocompleteResults response data is malformed");
        }
    }
}
