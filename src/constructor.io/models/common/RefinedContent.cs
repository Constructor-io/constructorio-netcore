using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Refined Content
 **/
namespace Constructorio_NET.Models
{
    public class RefinedContent
    {
        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }
    }
}
