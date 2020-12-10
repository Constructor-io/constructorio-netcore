using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Item Group
 */
namespace Constructorio_NET
{
    public class ResultGroup {

        [JsonPropertyName("display_name")]
        public String DisplayName { get; set; }

        [JsonPropertyName("group_id")]
        public String GroupId { get; set; }

        [JsonPropertyName("path")]
        public String Path { get; set; }

    }
}