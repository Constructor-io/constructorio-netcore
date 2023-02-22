using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructorio Facet Option.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class FacetOption
    {
        /// <summary>
        /// A value for this facet option. Must be unique for particular facet.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Display name for end users.
        /// Defaults to `null`.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Used to order facet options to fixed positions.
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
        /// Specifies whether the facet option is hidden from users.
        /// Defaults to `false`.
        /// </summary>
        public bool? Hidden { get; set; }

        /// <summary>
        /// Dictionary with any extra facet option data.
        /// Defaults to `null`.
        /// </summary>
        public Hashtable Data { get; set; }

        public FacetOption(string value)
        {
            this.Value = value;
            this.Data = null;
        }
    }
}