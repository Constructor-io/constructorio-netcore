using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Autocomplete Response
 */
namespace Constructorio_NET.Models
{
    public class AutocompleteResponse {

        [JsonProperty("sections")]
        public Dictionary<string, List<Result>> Sections { get; set; }

        [JsonProperty("result_id")]
        public string ResultId { get; set; }

        [JsonProperty("request")]
        public Dictionary<string, Object> Request { get; set; }
    }
}
