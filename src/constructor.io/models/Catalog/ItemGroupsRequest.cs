using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Items Groups Request Class.
    /// </summary>
    public class ItemGroupsRequest
    {
        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Gets or sets a item group id to request.
        /// </summary>
        public string ItemGroupId { get; set; }

        /// <summary>
        /// Gets or sets the list of item groups to request.
        /// </summary>
        public List<ConstructorItemGroup> ItemGroups { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemGroupsRequest"/> class.
        /// </summary>
        public ItemGroupsRequest()
        {
        }

        public ItemGroupsRequest(List<ConstructorItemGroup> itemGroups)
        {
            this.ItemGroups = itemGroups;
        }

        public ItemGroupsRequest(string itemGroupId)
        {
            this.ItemGroupId = itemGroupId;
        }

        /// <summary>
        /// Get request parameters.
        /// </summary>
        /// <returns>Hashtable of request parameters.</returns>
        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();

            if (string.IsNullOrEmpty(this.Section))
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
