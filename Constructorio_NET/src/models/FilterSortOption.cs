using System;
using Newtonsoft.Json;

/**
 * Constructor.io Sort Option
 */
namespace Constructorio_NET
{
    public class FilterSortOption {
  
        [JsonProperty("display_name")]
        public String DisplayName { get; set; } 

        [JsonProperty("sort_by")]
        public String SortBy { get; set; }

        [JsonProperty("sort_order")]
        public String SortOrder { get; set; }

        [JsonProperty("status")]
        public String Status { get; set; }

    }  
}