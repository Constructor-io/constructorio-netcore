using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Pod
 */
namespace Constructorio_NET
{
    public class ResultPod {

        [JsonPropertyName("display_name")]
        public String DisplayName { get; set; }

        [JsonPropertyName("id")]
        public String Id { get; set; }

    }
}