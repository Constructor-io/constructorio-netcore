﻿using System;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Client Config Class.
    /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorioConfig"/> class.
        /// </summary>
        /// <param name="apiKey">Api key used to direct requests.</param>
        /// <param name="apiToken">Api token used to authorize requests.</param>
        public ConstructorioConfig(string apiKey, string apiToken)
        {
            if (apiKey == null)
            {
                throw new ArgumentException("apiKey is required");
            }

            this.ApiToken = apiToken;
            this.ApiKey = apiKey;
        }

        public bool Contains(string field)
        {
            return typeof(ConstructorioConfig).GetProperty(field) != null;
        }
    }
}
