using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Autocomplete Response
 */
namespace Constructorio_NET
{
    public class AutocompleteResponse
    {

        [JsonPropertyName("sections")]
        public Dictionary<String, List<Result>> Sections { get; set; }

        [JsonPropertyName("result_id")]
        public String ResultId { get; set; }

        [JsonPropertyName("request")]
        public Dictionary<String, Object> Request { get; set; }
    }
}
