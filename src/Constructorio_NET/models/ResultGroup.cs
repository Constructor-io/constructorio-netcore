using System;
using Newtonsoft.Json;

/**
 * Constructor.io Item Group
 */
namespace Constructorio_NET.Models
{
    public class ResultGroup {

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("group_id")]
        public string GroupId { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

    }
}