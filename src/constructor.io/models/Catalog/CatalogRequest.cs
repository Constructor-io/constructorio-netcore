using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Catalog Request Class.
    /// </summary>
    public class CatalogRequest
    {
        /// <summary>
        /// Gets or sets collection of files to upload.
        /// </summary>
        public Dictionary<string, StreamContent> Files { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether process the catalog even if it will invalidate a large number of existing items.
        /// </summary>
        public bool Force { get; set; }

        /// <summary>
        /// Gets or sets an email address to receive an email notification if the task fails.
        /// </summary>
        public string NotificationEmail { get; set; }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogRequest"/> class.
        /// </summary>
        /// <param name="files">Dictionary of stream content to for the request.</param>
        public CatalogRequest(Dictionary<string, StreamContent> files)
        {
            if (files == null) throw new ArgumentException("files");

            if (!files.ContainsKey("items") && !files.ContainsKey("variations") && !files.ContainsKey("item_groups"))
            {
                throw new ConstructorException("Files should contain at least one of 'items', 'variations', 'item_groups' file(s)");
            }

            this.Files = files;
        }

        /// <summary>
        /// Get request parameters.
        /// </summary>
        /// <returns>Hashtable of request parameters.</returns>
        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();

            if (this.Force)
            {
                parameters.Add(Constants.FORCE, this.Force);
            }

            if (this.NotificationEmail != null)
            {
                parameters.Add(Constants.NOTIFICATION_EMAIL, this.NotificationEmail);
            }

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
