using System.Collections;
using System.Net.Http;
using Constructorio_NET.Models;
using Constructorio_NET.Modules;
using Constructorio_NET.Utils;

namespace Constructorio_NET
{
    /// <summary>
    /// Constructor.io .NET Client.
    /// </summary>
    public class ConstructorIO
    {
        private readonly string Version = "cionet-3.14.0";
        public Autocomplete Autocomplete { get; }
        public Browse Browse { get; }
        public Catalog Catalog { get; }
        public Recommendations Recommendations { get; }
        public Search Search { get; }
        public Tasks Tasks { get; }
        public Items Items { get; }
        public Quizzes Quizzes { get; }
        public static readonly HttpClient HttpClient = new HttpClient();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorIO"/> class.
        /// </summary>
        /// <param name="config">Collection of values to pass to modules.</param>
        public ConstructorIO(ConstructorioConfig config)
        {
            Hashtable options = new Hashtable { { Constants.VERSION, this.Version } };

            if (config.Contains("ApiKey"))
            {
                options.Add(Constants.API_KEY, config.ApiKey);
            }

            if (config.Contains("ConstructorToken"))
            {
                options.Add(Constants.CONSTRUCTOR_TOKEN, config.ConstructorToken);
            }

            if (config.Contains("ApiToken"))
            {
                options.Add(Constants.API_TOKEN, config.ApiToken);
            }

            if (config.Contains("ServiceUrl"))
            {
                options.Add(Constants.SERVICE_URL, config.ServiceUrl);
            }

            this.Autocomplete = new Autocomplete(options);
            this.Browse = new Browse(options);
            this.Catalog = new Catalog(options);
            this.Recommendations = new Recommendations(options);
            this.Search = new Search(options);
            this.Tasks = new Tasks(options);
            this.Items = new Items(options);
            this.Quizzes = new Quizzes(options);
        }
    }
}
