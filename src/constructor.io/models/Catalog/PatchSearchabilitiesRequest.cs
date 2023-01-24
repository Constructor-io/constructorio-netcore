using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Patch Searchabilities Request Class.
    /// </summary>
    public class PatchSearchabilitiesRequest
    {
        /// <summary>
        /// Gets or sets searchabilities to be uploaded.
        /// </summary>
        public List<Searchability> Searchabilities { get; set; }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatchSearchabilitiesRequest"/> class.
        /// </summary>
        /// <param name="searchabilities">List of searchabilities to update.</param>
        public PatchSearchabilitiesRequest(List<Searchability> searchabilities)
        {
            if (searchabilities == null || searchabilities.Count == 0) throw new ArgumentException("searchabilities");

            this.Searchabilities = searchabilities;
        }

        /// <summary>
        /// Get request parameters.
        /// </summary>
        /// <returns>Hashtable of request parameters.</returns>
        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();

            if (this.Section == null)
            {
                this.Section = "Products";
            }

            parameters.Add(Constants.SECTION, this.Section);

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
