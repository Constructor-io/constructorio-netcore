using System;
using Newtonsoft.Json;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// An item shown to the user, sent with tracking events.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class TrackingItem
    {
        /// <summary>
        /// Gets the item id.
        /// </summary>
        [JsonProperty("item_id")]
        public string ItemId { get; }

        /// <summary>
        /// Gets or sets the variation id.
        /// </summary>
        [JsonProperty("variation_id")]
        public string VariationId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingItem"/> class.
        /// </summary>
        /// <param name="itemId">Id of the item.</param>
        /// <param name="variationId">Id of the variation (optional).</param>
        public TrackingItem(string itemId, string variationId = null)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                throw new ArgumentException("itemId is required");
            }

            this.ItemId = itemId;
            this.VariationId = variationId;
        }
    }
}
