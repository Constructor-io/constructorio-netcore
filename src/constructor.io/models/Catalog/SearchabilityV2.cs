using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io v2 Searchability model.
    /// Note: v2 removes percentage_presence, example_items, and type fields from v1.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SearchabilityV2
    {
        /// <summary>
        /// Gets or sets name of the searchability field.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether searchability is fuzzy searchable.
        /// Either exact_searchable or fuzzy_searchable can be true, but not both.
        /// </summary>
        [JsonProperty("fuzzy_searchable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? FuzzySearchable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether searchability is exact searchable.
        /// Either exact_searchable or fuzzy_searchable can be true, but not both.
        /// </summary>
        [JsonProperty("exact_searchable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ExactSearchable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the field is displayable in the response within the results array.
        /// </summary>
        [JsonProperty("displayable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Displayable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the field is hidden by default in the response
        /// but makes it available to retrieve via fmt_options[hidden_fields] parameter.
        /// </summary>
        [JsonProperty("hidden", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Hidden { get; set; }

        /// <summary>
        /// Gets the creation date and time in ISO 8601 format.
        /// Read-only field returned by the API.
        /// </summary>
        [JsonProperty("created_at")]
        public string CreatedAt { get; private set; }

        /// <summary>
        /// Gets the last updated date and time in ISO 8601 format.
        /// Read-only field returned by the API. Null if never updated.
        /// </summary>
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchabilityV2"/> class.
        /// </summary>
        /// <param name="name">Name of the searchability field.</param>
        public SearchabilityV2(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchabilityV2"/> class.
        /// Default constructor for deserialization.
        /// </summary>
        [JsonConstructor]
        public SearchabilityV2()
        {
        }
    }
}
