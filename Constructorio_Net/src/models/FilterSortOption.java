using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Sort Option
 **/
namespace Constructorio_NET
{
    public class FilterSortOption {
  
        [JsonPropertyName("display_name")]
        public String displayName;

        [JsonPropertyName("sort_by")]
        public String sortBy;

        [JsonPropertyName("sort_order")]
        public String sortOrder;
  
        [JsonPropertyName("status")]
        public String status;

    }  
}