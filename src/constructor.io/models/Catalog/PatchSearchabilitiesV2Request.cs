using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io v2 Patch Searchabilities Request Class.
    /// </summary>
    public class PatchSearchabilitiesV2Request
    {
        /// <summary>
        /// Gets or sets searchabilities to be created or updated.
        /// </summary>
        public List<SearchabilityV2> Searchabilities { get; set; }

        /// <summary>
        /// Gets or sets the name of the section. Defaults to "Products".
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to skip index rebuild.
        /// </summary>
        public bool? SkipRebuild { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatchSearchabilitiesV2Request"/> class.
        /// </summary>
        /// <param name="searchabilities">List of searchabilities to create or update.</param>
        public PatchSearchabilitiesV2Request(List<SearchabilityV2> searchabilities)
        {
            if (searchabilities == null || searchabilities.Count == 0)
            {
                throw new ArgumentException("searchabilities cannot be null or empty", nameof(searchabilities));
            }

            this.Searchabilities = searchabilities;
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
    }
}
