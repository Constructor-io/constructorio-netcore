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
            Hashtable queryParams = req.GetUrlParameters();
            List<string> paths = new List<string> { "autocomplete", req.Query };

            return MakeUrl(this.Options, paths, queryParams);
        }

        /// <summary>
        /// Retrieve Autocomplete results from API.
        /// </summary>
        /// <param name="autocompleteReq">Constructorio's Autocomplete request object.</param>
        /// <returns>Constructorio's Autocomplete response object.</returns>
        public AutocompleteResponse GetAutocompleteResults(AutocompleteRequest autocompleteReq)
        {
            string url;
            Task<string> task;

            try
            {
                url = CreateAutocompleteUrl(autocompleteReq);
                Dictionary<string, string> requestHeaders = autocompleteReq.GetRequestHeaders();
                task = MakeHttpRequest(HttpMethod.Get, url, requestHeaders);
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
