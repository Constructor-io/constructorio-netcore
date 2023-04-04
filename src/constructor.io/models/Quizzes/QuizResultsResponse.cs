using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io QuizResultsResponse Response
 */
namespace Constructorio_NET.Models
{
    public class ResultResponse
    {
        [JsonProperty("filter_expression")]
        public Dictionary<string, object> FilterExpression { get; set; }

        [JsonProperty("results_url")]
        public string ResultsUrl { get; set; }
    }

    public class QuizResultsResponse
    {
        [JsonProperty("result")]
        public ResultResponse Result { get; set; }

        [JsonProperty("version_id")]
        public string VersionId { get; set; }
    }
}