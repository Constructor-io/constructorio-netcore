using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Search Response
 **/
namespace Constructorio_NET
{
    public class SearchResponse {

        [JsonPropertyName("result_id")]
        public String resultId;

        [JsonPropertyName("response")]
        public SearchResponseInner response;

        [JsonPropertyName("request")]
        public Map<String, Object> request;

    }
}