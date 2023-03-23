using System;
using Constructorio_NET.Models;
using Newtonsoft.Json;

/**
 * Constructor.io OptionAttribute Response
 */
namespace Constructorio_NET.Models
{
    public class OptionAttribute
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}