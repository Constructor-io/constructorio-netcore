using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Browse Response
 */
namespace Constructorio_NET.Models
{
    public class BrowseResponse
    {
        [JsonProperty("result_id")]
        public string ResultId { get; set; }

        [JsonProperty("response")]
        public BrowseResponseInner Response { get; set; }

        [JsonProperty("request")]
        public Dictionary<string, object> Request { get; set; }
    }
}