﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;

namespace Constructorio_NET
{
    /**
     * Constructor.io Catalog Request
     */
    public class CatalogRequest
    {
        /// <value>
        /// Collection of files to upload
        /// </value>
        public Dictionary<string, StreamContent> Files { get; set; }
        /// <value>
        /// Process the catalog even if it will invalidate a large number of existing items
        /// </value>
        public string Force { get; set; }
        /// <value>
        /// An email address to receive an email notification if the task fails
        /// </value>
        public string NotificationEmail { get; set; }
        /// <value>
        /// The name of the section 
        /// </value>
        public string Section { get; set; }

        /// <summary>
        /// Creates a catalog request
        /// </summary>
        /// <param name="files"></param>
        public CatalogRequest(Dictionary<string, StreamContent> files)
        {
            if (!files.ContainsKey("items") && !files.ContainsKey("variations") && !files.ContainsKey("item_groups"))
            {
                throw new ArgumentException("Files should contain at least one of 'items', 'variations', 'item_groups' file(s)");
            }

            this.Files = files;
        }

        public Hashtable GetUrlParameters()
        {
            Hashtable parameters = new Hashtable();

            if (this.Force != null)
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

        public Dictionary<string, string> GetRequestHeaders()
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            return requestHeaders;
        }
    }
}
