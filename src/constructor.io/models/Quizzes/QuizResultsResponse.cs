using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io QuizResultsResponse Response
 */
namespace Constructorio_NET.Models
{
    public class QuizResultsResponse
    {
        [JsonProperty("result")]
        public Dictionary<string, object> Result { get; set; }

        [JsonProperty("results_url")]
        public string ResultsUrl { get; set; }

        [JsonProperty("version_id")]
        public string VersionId { get; set; }
    }
}
