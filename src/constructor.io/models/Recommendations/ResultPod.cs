using System;
using Newtonsoft.Json;

/**
 * Constructor.io Pod
 */
namespace Constructorio_NET.Models.Recommendations
{
    public class ResultPod
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}