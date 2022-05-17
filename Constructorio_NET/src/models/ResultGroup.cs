using System;
using Newtonsoft.Json;

/**
 * Constructor.io Item Group
 */
namespace Constructorio_NET
{
    public class ResultGroup {

        [JsonProperty("display_name")]
        public String DisplayName { get; set; }

        [JsonProperty("group_id")]
        public String GroupId { get; set; }

        [JsonProperty("path")]
        public String Path { get; set; }

    }
}