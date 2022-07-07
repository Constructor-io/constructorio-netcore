using System;
using System.Collections;
using System.Net.Http;
using Constructorio_NET.Utils;

namespace Constructorio_NET
{
    public class ConstructorIO
    {
        public string protocol;
        public int port;
        private Hashtable Options;
        public Autocomplete Autocomplete;
        public Browse Browse;
        public Catalog Catalog;
        public Recommendations Recommendations;
        public Search Search;
        public Tasks Tasks;
        private readonly string Version = "cionet-1.0.0";

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorIO"/> class.
        /// Creates a constructor.io instance.
        /// </summary>
        /// <param name="options">Hashtable of options to pass to modules.</param>
        public ConstructorIO(Hashtable options)
        {
            this.Options = new Hashtable
            {
                { Constants.VERSION, this.Version }
            };

            if (options.ContainsKey(Constants.API_KEY))
            {
                this.Options.Add(Constants.API_KEY, options[Constants.API_KEY]);
            }
            else
            {
                throw new ConstructorException("apiKey is required");
            }

            if (options.ContainsKey("constructorToken"))
            {
                this.Options.Add("constructorToken", options["constructorToken"]);
            }

            if (options.ContainsKey(Constants.API_TOKEN))
            {
                this.Options.Add(Constants.API_TOKEN, options[Constants.API_TOKEN]);
            }

            string serviceUrl = options.ContainsKey(Constants.SERVICE_URL) ? (string)options[Constants.SERVICE_URL] : "https://ac.cnstrc.com";
            this.Options.Add(Constants.SERVICE_URL, serviceUrl);
            this.Autocomplete = new Autocomplete(this.Options);
            this.Browse = new Browse(this.Options);
            this.Catalog = new Catalog(this.Options);
            this.Recommendations = new Recommendations(this.Options);
            this.Search = new Search(this.Options);
            this.Tasks = new Tasks(this.Options);
        }
    }
}