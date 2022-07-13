using System;

/**
 * Constructor.io ConstructorioConfig
 **/
namespace Constructorio_NET.Models
{
    public class ConstructorioConfig
    {
        /// <summary>
        /// Gets or sets key used to direct requests.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets token used to authorize certain requests.
        /// </summary>
        public string ApiToken { get; set; }

        /// <summary>
        /// Gets or sets token used to validate backend side requests.
        /// </summary>
        public string ConstructorToken { get; set; }

        /// <summary>
        /// Gets or sets url used to direct requests to correct DNS zone.
        /// </summary>
        public string ServiceUrl { get; set; } = "https://ac.cnstrc.com";

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorioConfig"/> class.
        /// Collection of values to pass to modules.
        /// </summary>
        /// <param name="apiKey">Api key used to direct requests.</param>
        public ConstructorioConfig(string apiKey)
        {
            if (apiKey == null)
            {
                throw new ArgumentException("apiKey is required");
            }

            this.ApiKey = apiKey;
        }

        public bool Contains(string field)
        {
            return typeof(ConstructorioConfig).GetProperty(field) != null;
        }
    }
}
