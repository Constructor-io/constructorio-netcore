using System;
using Newtonsoft.Json;

/**
 * Constructor.io Sort Option
 */
namespace Constructorio_NET.Models
{
    public class FilterSortOption
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("sort_by")]
        public string SortBy { get; set; }

        [JsonProperty("sort_order")]
        public string SortOrder { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}