using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructorio Supported v2 Facet Types
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public enum FacetTypeV2
    {
        /// <summary>
        /// Used for facet groups with categorical values.
        /// </summary>
        Multiple,

        /// <summary>
        /// Used for facet groups with hierarchical values.
        /// </summary>
        Hierarchical,

        /// <summary>
        /// Used for facet groups with numerical values. Required for displaying a slider or a list of range buckets.
        /// </summary>
        Range
    }

    /// <summary>
    /// Constructorio v2 Facet.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class FacetV2
    {
        /// <summary>
        /// The facet name used in the catalog. Must be unique inside the section and key.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The path in metadata of each item where this facet is present. Must be unique inside the section and key.
        /// Required for creating a new facet configuration.
        /// </summary>
        public string PathInMetadata { get; set; }

        /// <summary>
        /// Type of facet group. Can be 'multiple', 'hierarchical', or 'range'.
        /// </summary>
        public FacetTypeV2 Type { get; set; }

        /// <summary>
        /// Display name for end users.
        /// Default value is `null`, in which case `Name` will be used.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Default criterion to sort this facet group. Overridden by `Position` attribute on facet options.
        /// Defaults to `relevance`.
        /// </summary>
        public FacetSortOrder? SortOrder { get; set; }

        /// <summary>
        /// Sort direction for `SortOrderBy`.
        /// Default value is `false` for SortOrderBy = `value` and `true` otherwise.
        /// </summary>
        public bool? SortDescending { get; set; }

        /// <summary>
        /// Specifies whether the range buckets are determined dynamically (based on the values of matching facet options) or statically.
        /// Required if FacetType = `range` & RangeFormat = `options`.
        /// </summary>
        public FacetRangeType? RangeType { get; set; }

        /// <summary>
        /// Format of range facets. Determines whether the facet is configured to display as a slider (in which case the search endpoint will return only min & max values) or as a list of buckets.
        /// Required if FaceType = `range`.
        /// </summary>
        public FacetRangeFormat? RangeFormat { get; set; }

        /// <summary>
        /// Used to create inclusive buckets.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public FacetRangeInclusive? RangeInclusive { get; set; }

        // Used by Json.Net to determine whether or not to serialize RangeInclusive.
        public bool ShouldSerializeRangeInclusive()
        {
            if (RangeInclusive == null) return false;
            if (RangeInclusive == FacetRangeInclusive.Null) RangeInclusive = null;
            return true;
        }

        /// <summary>
        /// Defines the cut-off points for generating static range buckets. Expects list of sorted numbers (like [10, 25, 40]).
        /// Required if FacetType = `range` & BucketSize = null.
        /// </summary>
        public List<double> RangeLimits { get; set; }

        /// <summary>
        /// Specifies filter behavior given multiple filters on the same facet (e.g: color: yellow & blue) are selected.
        /// </summary>
        public FacetMatchType? MatchType { get; set; }

        /// <summary>
        /// Used to slot facet groups to fixed positions. Must be >= 1.
        /// When set to null, position will not be serialized and server defaults apply.
        /// </summary>
        public int? Position { get; set; }

        /// <summary>
        /// Specifies whether the facet is hidden from users. Use this for facet data that you don't want shown to end users, but that isn't sensitive.
        /// Defaults to `false`.
        /// </summary>
        public bool? Hidden { get; set; }

        /// <summary>
        /// Specifies whether the facet is protected from users. Setting this to true will require authentication to view the facet.
        /// Defaults to `false`.
        /// </summary>
        public bool? Protected { get; set; }

        /// <summary>
        /// Specifies whether counts for each facet option should be calculated and shown in the response.
        /// Setting this to false will skip counting these options, improving performance for facets with high cardinality.
        /// Defaults to `true`.
        /// </summary>
        public bool? Countable { get; set; }

        /// <summary>
        /// Maximum number of options of facet type `multiple` to return in search responses.
        /// If absent, the default limit is applied. Minimum: 0, Maximum: 4000, Default: 500.
        /// </summary>
        public int? OptionsLimit { get; set; }

        /// <summary>
        /// Dictionary with any extra facet data.
        /// Defaults to `{}` (empty dictionary)
        /// </summary>
        public Hashtable Data { get; set; }

        /// <summary>
        /// Facet creation date and time in ISO 8601 format.
        /// Read-only field returned by the API.
        /// </summary>
        [JsonProperty("created_at")]
        public string CreatedAt { get; private set; }

        /// <summary>
        /// Last facet update date and time in ISO 8601 format.
        /// Read-only field returned by the API.
        /// </summary>
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FacetV2"/> class.
        /// Object model for adding/updating v2 facet configurations.
        /// </summary>
        /// <param name="name"><see cref="FacetV2.Name"/></param>
        /// <param name="pathInMetadata"><see cref="FacetV2.PathInMetadata"/></param>
        /// <param name="type"><see cref="FacetV2.Type"/></param>
        public FacetV2(string name, string pathInMetadata, FacetTypeV2 type)
        {
            this.Name = name;
            this.PathInMetadata = pathInMetadata;
            this.Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FacetV2"/> class.
        /// Default constructor for deserialization.
        /// </summary>
        [JsonConstructor]
        public FacetV2()
        {
        }
    }
}
