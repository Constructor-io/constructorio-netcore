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
    public class Autocomplete : Helpers
    {
        private readonly Hashtable Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="Autocomplete"/> class.
        /// Interface for autocomplete related API calls.
        /// </summary>
        /// <param name="options">Hashtable of options from Constructorio instantiation.</param>
        internal Autocomplete(Hashtable options)
        {
            this.Options = options;
        }

        internal string CreateAutocompleteUrl(AutocompleteRequest req)
        {
            Hashtable requestParams = req.GetRequestParameters();
            List<string> paths = new List<string> { "autocomplete", req.Query };

            return MakeUrl(this.Options, paths, requestParams);
        }

        /// <summary>
        /// Retrieve Autocomplete results from API.
        /// </summary>
        /// <param name="autocompleteRequest">Constructorio's autocomplete request object.</param>
        /// <returns>Constructorio's autocomplete response object.</returns>
        public async Task<AutocompleteResponse> GetAutocompleteResults(AutocompleteRequest autocompleteRequest)
        {
            string url;
            string result;

            url = CreateAutocompleteUrl(autocompleteRequest);
            Dictionary<string, string> requestHeaders = autocompleteRequest.GetRequestHeaders();
            result = await MakeHttpRequest(HttpMethod.Get, url, requestHeaders);

            if (result != null)
            {
                return JsonConvert.DeserializeObject<AutocompleteResponse>(result);
            }

            throw new ConstructorException("GetAutocompleteResults response data is malformed");
        }
    }
}
