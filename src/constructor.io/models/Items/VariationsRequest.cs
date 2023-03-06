using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Variations Request Class.
    /// </summary>
    public class VariationsRequest
    {
        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Gets or sets the page of results to request.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the number of results per page to request.
        /// </summary>
        public int ResultsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the list of variation ids to request.
        /// </summary>
        public List<string> Ids { get; set; }

        /// <summary>
        /// Gets or sets the item id to filter for.
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VariationsRequest"/> class.
        /// </summary>
        /// <param name="files">Dictionary of stream content to for the request.</param>
        public VariationsRequest()
        {
            this.Section = "Products";
            this.Page = 1;
            this.ResultsPerPage = 30;
            this.Ids = new List<string>();
        }

        /// <summary>
        /// Get request parameters.
        /// </summary>
        /// <returns>Hashtable of request parameters.</returns>
        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();

            if (this.Page > 0)
            {
                parameters.Add(Constants.PAGE, this.Page);
            }

            if (this.ResultsPerPage > 0)
            {
                parameters.Add(Constants.RESULTS_PER_PAGE, this.ResultsPerPage);
            }

            if (string.IsNullOrEmpty(this.Section))
            {
                this.Section = "Products";
            }

            if (this.Ids != null && this.Ids.Count > 0)
            {
                parameters.Add("id", this.Ids);
            }

            if (!string.IsNullOrEmpty(this.ItemId))
            {
                parameters.Add("item_id", this.ItemId);
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
