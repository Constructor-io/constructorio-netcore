using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Filter Group
 **/
namespace Constructorio_NET.Models
{
    public class FilterGroup {

        [JsonProperty("children")]
        public List<FilterGroup> Children { get; set; }

        [JsonProperty("parents")]
        public List<FilterGroup> Parents { get; set; }

        [JsonProperty("count")]
        public Int32 Count { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("group_id")]
        public string GroupId { get; set; }

    }
}