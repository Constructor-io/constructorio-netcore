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
        public Dictionary<String, List<Result>> Sections { get; set; }

        [JsonProperty("result_id")]
        public String ResultId { get; set; }

        [JsonProperty("request")]
        public Dictionary<String, Object> Request { get; set; }
    }
}
