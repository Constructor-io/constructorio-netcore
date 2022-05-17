using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Catalog Response
 */
namespace Constructorio_NET
{
    public class CatalogResponse {

        [JsonPropertyName("result_id")]
        public String ResultId { get; set; }

        [JsonPropertyName("request")]
        public Dictionary<String, Object> Request { get; set; }
    }
}