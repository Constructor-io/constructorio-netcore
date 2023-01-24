using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Searchability
 **/
namespace Constructorio_NET.Models
{
    public class Searchability
    {
        /// <summary>
        /// Gets or sets name of the searchability.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether searchability is fuzzy searchable.
        /// </summary>
        [JsonProperty("fuzzy_searchable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? FuzzySearchable { get; set; }

        /// <summary>
        /// Gets a value indicating the percentage of items with this field.
        /// </summary>
        [JsonProperty("percentage_presence", NullValueHandling = NullValueHandling.Ignore)]
        public float? PercentagePresence { get; private set; }

        /// <summary>
        /// Gets a list of items that contain this field if exists.
        /// </summary>
        [JsonProperty("example_items", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ExampleItems { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether searchability is exact searchable.
        /// </summary>
        [JsonProperty("exact_searchable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ExactSearchable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether searchability is hidden.
        /// </summary>
        [JsonProperty("hidden", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Hidden { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether searchability is displayable.
        /// </summary>
        [JsonProperty("displayable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Displayable { get; set; }

        /// <summary>
        /// Gets a value indicating the type of searchability ex: String, Array, ...etc.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; private set; }

        /// <summary>
        /// Gets a value indicating when this searchability was first created.
        /// </summary>
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedAt { get; private set; }

        /// <summary>
        /// Gets a value indicating when this searchability was last updated. Null if never updated.
        /// </summary>
        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedAt { get; private set; }

        public Searchability(string name)
        {
            Name = name;
        }

        public Searchability()
        {
        }
    }
}