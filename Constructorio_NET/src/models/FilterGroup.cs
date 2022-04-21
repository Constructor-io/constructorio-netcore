using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Filter Group
 **/
namespace Constructorio_NET
{
    public class FilterGroup {

        [JsonPropertyName("children")]
        public List<FilterGroup> Children { get; set; }

        [JsonPropertyName("parents")]
        public List<FilterGroup> Parents { get; set; }

        [JsonPropertyName("count")]
        public Int32 Count { get; set; }

        [JsonPropertyName("display_name")]
        public String DisplayName { get; set; }

        [JsonPropertyName("group_id")]
        public String GroupId { get; set; }

    }
}