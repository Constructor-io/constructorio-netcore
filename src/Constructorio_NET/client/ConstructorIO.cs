using System;
using System.Collections;
using Constructorio_NET.Models;
using Constructorio_NET.Modules;
using Constructorio_NET.Utils;

namespace Constructorio_NET
{
    public class ConstructorIO
    {
        private readonly string Version = "cionet-1.0.0";
        public Autocomplete Autocomplete { get; }
        public Browse Browse { get; }
        public Catalog Catalog { get; }
        public Recommendations Recommendations { get; }
        public Search Search { get; }
        public Tasks Tasks { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorIO"/> class.
        /// Creates a constructor.io instance.
        /// </summary>
        /// <param name="config">Collection of values to pass to modules.</param>
        public ConstructorIO(ConstructorioConfig config)
        {
            Hashtable fullOptions = new Hashtable
            {
                { Constants.VERSION, this.Version }
            };

            if (config.Contains("ApiKey"))
            {
                fullOptions.Add(Constants.API_KEY, config.ApiKey);
            }

            if (config.Contains("ConstructorToken"))
            {
                fullOptions.Add(Constants.CONSTRUCTOR_TOKEN, config.ConstructorToken);
            }

            if (config.Contains("ApiToken"))
            {
                fullOptions.Add(Constants.API_TOKEN, config.ApiToken);
            }

            if (config.Contains("ServiceUrl"))
            {
                fullOptions.Add(Constants.SERVICE_URL, config.ServiceUrl);
            }

            this.Autocomplete = new Autocomplete(fullOptions);
            this.Browse = new Browse(fullOptions);
            this.Catalog = new Catalog(fullOptions);
            this.Recommendations = new Recommendations(fullOptions);
            this.Search = new Search(fullOptions);
            this.Tasks = new Tasks(fullOptions);
        }
    }
}