using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io v2 Delete Searchabilities Request Class.
    /// </summary>
    public class DeleteSearchabilitiesV2Request
    {
        /// <summary>
        /// Gets or sets the list of searchability names to delete.
        /// </summary>
        public List<string> SearchabilityNames { get; set; }

        /// <summary>
        /// Gets or sets the name of the section. Defaults to "Products".
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to skip index rebuild.
        /// </summary>
        public bool? SkipRebuild { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSearchabilitiesV2Request"/> class.
        /// </summary>
        /// <param name="searchabilityNames">List of searchability names to delete.</param>
        public DeleteSearchabilitiesV2Request(List<string> searchabilityNames)
        {
            if (searchabilityNames == null || searchabilityNames.Count == 0)
            {
                throw new ArgumentException("searchabilityNames cannot be null or empty", nameof(searchabilityNames));
            }

            this.SearchabilityNames = searchabilityNames;
        }

        /// <summary>
        /// Get request parameters.
        /// </summary>
        /// <returns>Hashtable of request parameters.</returns>
        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();

            if (this.SkipRebuild.HasValue)
            {
                parameters.Add("skip_rebuild", this.SkipRebuild.Value.ToString().ToLower());
            }

            return parameters;
        }

        /// <summary>
        /// Get request headers.
        /// </summary>
        /// <returns>Dictionary of request headers.</returns>
        public Dictionary<string, string> GetRequestHeaders()
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            return requestHeaders;
        }

        /// <summary>
        /// Get the request body for the delete operation.
        /// </summary>
        /// <returns>Hashtable containing the searchabilities to delete.</returns>
        public Hashtable GetRequestBody()
        {
            var searchabilities = new List<Hashtable>();
            foreach (var name in this.SearchabilityNames)
            {
                searchabilities.Add(new Hashtable { { "name", name } });
            }

            return new Hashtable
            {
                { "searchabilities", searchabilities }
            };
        }
    }
}
