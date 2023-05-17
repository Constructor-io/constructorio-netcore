using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Sort Option.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SortOption
    {
        /// <summary>
        /// Display name for the sort option.
        /// Defaults to null.
        /// </summary>
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Unique identifier for the sort option.
        /// Only one combination of SortOrder & SortBy can exist.
        /// </summary>
        [JsonProperty("sort_by")]
        public string SortBy { get; set; }

        /// <summary>
        /// Order the results will be sorted in.
        /// </summary>
        [JsonProperty("sort_order")]
        public SortOrder? SortOrder { get; set; }

        /// <summary>
        /// Order the sort option will be presented in.
        /// Defaults to null.
        /// </summary>
        [JsonProperty("position", NullValueHandling = NullValueHandling.Include)]
        public int? Position { get; set; }

        /// <summary>
        /// Informs Newtonsoft whether to serialize `Position`.
        /// </summary>
        public bool ShouldSerializePosition()
        {
            if (Position == -1) return false;
            return true;
        }

        /// <summary>
        /// Path to the metadata field to sort on. Required when creating new sort options.
        /// </summary>
        [JsonProperty("path_in_metadata")]
        public string PathInMetadata { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortOption"/> class.
        /// Object model for adding/updating sort options.
        /// </summary>
        /// <param name="sortBy"><see cref="SortOption.SortBy"/></param>
        /// <param name="sortOrder"><see cref="SortOption.SortOrder"/></param>
        [JsonConstructor]
        public SortOption(string sortBy, SortOrder? sortOrder)
        {
            this.SortBy = sortBy;
            this.SortOrder = sortOrder;
            this.Position = -1;
        }

        /// <summary>
        /// Initializes an empty instance of the <see cref="SortOption"/> class, typically for delta requests.
        /// </summary>
        public SortOption() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortOption"/> class.
        /// </summary>
        /// <param name="sortBy"><see cref="SortOption.SortBy"/></param>
        /// <param name="sortOrder"><see cref="SortOption.SortOrder"/></param>
        /// <param name="pathInMetadata"><see cref="SortOption.PathInMetadata"/></param>
        public SortOption(string sortBy, SortOrder sortOrder, string pathInMetadata)
        {
            this.SortBy = sortBy;
            this.SortOrder = sortOrder;
            this.PathInMetadata = pathInMetadata;
            this.Position = -1;
        }
    }

    /// <summary>
    /// Constructorio Supported Sort Order Types.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortOrder
    {
        /// <summary>
        /// Specifies that results should be sorted in ascending order.
        /// </summary>
        [EnumMember(Value = "ascending")]
        Ascending,

        /// <summary>
        /// Specifies that results should be sorted in descending order.
        /// </summary>
        [EnumMember(Value = "descending")]
        Descending
    }
}