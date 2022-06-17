using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        /// The name of the section 
        /// </value>
        public string Section { get; set; }
        /// <value>
        /// </value>
        public string Items { get; set; }
        /// <value>
        /// </value>
        public string Variations { get; set; }
        /// <value>
        /// </value>
        public string ItemGroups { get; set; }

        /// <summary>
        /// Creates a catalog request
        /// </summary>
        /// <param name="query"></param>
        public CatalogRequest(Dictionary<string, StreamContent> files)
        // public CatalogRequest(Dictionary<string, FileStream> files)
        {
            // foreach (var file in files)
            // {
            //     if (!File.Exists($@"{file.Value}"))
            //     {
            //         throw new ArgumentException($"File for {file.Key} was not found");
            //     }
            //     else
            //     {
            //         // this[filePath.Key] = filePath.Value;
            //         this.GetType().GetProperty($"{file.Key}").SetValue(file.Key, file.Value, null);
            //     }
            // }

            this.Files = files;
        }

        public Hashtable GetUrlParameters()
        {
            Hashtable parameters = new Hashtable();

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
