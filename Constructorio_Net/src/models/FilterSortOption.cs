using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Sort Option
 */
namespace Constructorio_NET
{
    public class FilterSortOption {
  
        [JsonPropertyName("display_name")]
        public String DisplayName { get; set; } 

        [JsonPropertyName("sort_by")]
        public String SortBy { get; set; }

        [JsonPropertyName("sort_order")]
        public String SortOrder { get; set; }

        [JsonPropertyName("status")]
        public String Status { get; set; }

    }  
}