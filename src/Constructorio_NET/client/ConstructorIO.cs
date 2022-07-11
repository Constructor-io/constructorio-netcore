using System;
using System.Collections;
using System.Net.Http;
using Constructorio_NET.Utils;

namespace Constructorio_NET
{
    public class ConstructorIO
    {
        public int Port { get; set; }
        public string Protocol { get; set; }
        public Autocomplete Autocomplete { get; }
        public Browse Browse { get; }
        public Catalog Catalog { get; }
        public Recommendations Recommendations { get; }
        public Search Search { get; }
        public Tasks Tasks { get; }
        private readonly string Version = "cionet-1.0.0";

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorIO"/> class.
        /// Creates a constructor.io instance.
        /// </summary>
        /// <param name="options">Hashtable of options to pass to modules.</param>
        public ConstructorIO(Hashtable options)
        {
            Hashtable fullOptions = new Hashtable
            {
                { Constants.VERSION, this.Version }
            };

            if (options.ContainsKey(Constants.API_KEY))
            {
                fullOptions.Add(Constants.API_KEY, options[Constants.API_KEY]);
            }
            else
            {
                throw new ConstructorException("apiKey is required");
            }

            if (options.ContainsKey("constructorToken"))
            {
                fullOptions.Add("constructorToken", options["constructorToken"]);
            }

            if (options.ContainsKey(Constants.API_TOKEN))
            {
                fullOptions.Add(Constants.API_TOKEN, options[Constants.API_TOKEN]);
            }

            string serviceUrl = options.ContainsKey(Constants.SERVICE_URL) ? (string)options[Constants.SERVICE_URL] : "https://ac.cnstrc.com";
            fullOptions.Add(Constants.SERVICE_URL, serviceUrl);
            this.Autocomplete = new Autocomplete(fullOptions);
            this.Browse = new Browse(fullOptions);
            this.Catalog = new Catalog(fullOptions);
            this.Recommendations = new Recommendations(fullOptions);
            this.Search = new Search(fullOptions);
            this.Tasks = new Tasks(fullOptions);
        }
    }
}