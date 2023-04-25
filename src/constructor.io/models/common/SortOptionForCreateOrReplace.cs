using Newtonsoft.Json;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Sort Option For Modification Requests.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SortOptionForModifyRequest
    {
        /// <summary>
        /// Display name for the sort option.
        /// Defaults to null/
        /// </summary>
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Order the sort option will be presented in/
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
    }
}