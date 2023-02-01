using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructorio Supported Facet Types
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public enum FacetType
    {
        /// <summary>
        /// Used for facet groups with categorical values.
        /// </summary>
        Multiple,

        /// <summary>
        /// Used for facet groups with numerical values. Required for displaying a slider or a list of range buckets.
        /// </summary>
        Range
    }

    /// <summary>
    /// Default criterion to sort facet options in a facet group. Overriden by `position` attribute in facet options.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public enum FacetSortOrder
    {
        /// <summary>
        /// Default sort order.
        /// </summary>
        Relevance,

        /// <summary>
        /// Sorts facet options alpha-numerically.
        /// </summary>
        Value,

        /// <summary>
        /// Sorts facet options by number of matches.
        /// </summary>
        NumMatches
    }

    /// <summary>
    /// Required for FacetType = `range`. Specifies the origin of range buckets.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public enum FacetRangeType
    {
        /// <summary>
        /// Range buckets are defined statically.
        /// </summary>
        Static,

        /// <summary>
        /// Range buckets are defined dynamically based on the values of matching facet options.
        /// </summary>
        Dynamic,
    }

    /// <summary>
    /// Required for FaceType = `range`. Format of range facets.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public enum FacetRangeFormat
    {
        /// <summary>
        /// Facet to be displayed as a slider (Search endpoint returns only min & max values).
        /// </summary>
        Boundaries,

        /// <summary>
        /// Facet to be displayed as a list of buckets.
        /// </summary>
        Options
    }

    /// <summary>
    /// Used to create inclusive buckets.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public enum FacetRangeInclusive
    {
        /// <summary>
        /// Range Buckets are non-inclusive. Default option.
        /// </summary>
        Null,

        /// <summary>
        /// Range Buckets have no upper bound.
        /// </summary>
        Above,

        /// <summary>
        /// Range buckets have no lower bound.
        /// </summary>
        Below
    }

    /// <summary>
    /// Specifies filter behavior given multiple filters on the same facet (e.g: color: yellow & blue) are selected.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public enum FacetMatchType
    {
        /// <summary>
        /// Results matching any of the filters are returned.
        /// </summary>
        Any,

        /// <summary>
        /// Only results matching all of the filters are returned.
        /// </summary>
        All,

        /// <summary>
        /// Only results matching none of the filters are returned.
        /// </summary>
        None
    }

    /// <summary>
    /// Constructorio Facet.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Facet
    {
        /// <summary>
        /// The facet name used in the catalog. Must be unique inside the section and key.
        /// </summary>
        public string Name { get; set; }

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
        /// Type of facet group.
        /// </summary>
        public FacetType Type { get; set; }

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
        /// Specifies the size of generated range buckets. Defaults to null.
        /// Required if FacetType = `range` & RangeLimits = null.
        /// </summary>
        public float? BucketSize { get; set; }

        /// <summary>
        /// Defines the cut-off points for generating static range buckets. Expects list of sorted numbers (like [10, 25, 40]).
        /// Required if FacetType = `range` & BucketSize = null.
        /// </summary>
        public List<int> RangeLimits { get; set; }

        /// <summary>
        /// Specifies filter behavior given multiple filters on the same facet (e.g: color: yellow & blue) are selected.
        /// </summary>
        public FacetMatchType? MatchType { get; set; }

        /// <summary>
        /// Used to slot facet groups to fixed positions.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public int? Position { get; set; }
        public bool ShouldSerializePosition()
        {
            if (Position == null) return false;
            if (Position == -1) Position = null;
            return true;
        }

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
        /// List of facet option configurations to create and associate with this facet group.
        /// Defaults to `[]` (empty list).
        /// </summary>
        public List<FilterFacetOption> Options { get; set; }

        /// <summary>
        /// Dictionary with any extra facet data.
        /// Defaults to `{}` (empty dictionary)
        /// </summary>
        public Hashtable Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Facet"/> class.
        /// Object model for adding/updating facet configurations.
        /// </summary>
        /// <param name="name"><see cref="Facet.Name"/></param>
        /// <param name="type"><see cref="Facet.FacetType"/></param>
        public Facet(string name, FacetType type)
        {
            this.Name = name;
            this.Type = type;
        }
    }
}