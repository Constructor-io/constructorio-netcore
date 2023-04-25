﻿using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Sort Options Base Request Class.
    /// </summary>
    public class SortOptionsRequest
    {
        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortOptionsRequest"/> class.
        /// </summary>
        /// <param name="section">Section to address the request to.</param>
        public SortOptionsRequest(string section = "Products")
        {
            this.Section = section;
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