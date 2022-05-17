using System;
using Newtonsoft.Json;

/**
 * Constructor.io Pod
 */
namespace Constructorio_NET
{
    public class ResultPod {

        [JsonProperty("display_name")]
        public String DisplayName { get; set; }

        [JsonProperty("id")]
        public String Id { get; set; }

    }
}