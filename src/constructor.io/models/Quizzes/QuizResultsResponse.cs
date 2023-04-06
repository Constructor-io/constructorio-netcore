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
        [JsonProperty("request")]
        public Dictionary<string, object> Request { get; set; }

        [JsonProperty("response")]
        public QuizResultsResponseInner Response { get; set; }

        [JsonProperty("result_id")]
        public string ResultId { get; set; }

        [JsonProperty("quiz_version_id")]
        public string QuizVersionId { get; set; }

        [JsonProperty("quiz_session_id")]
        public string QuizSessionId { get; set; }

        [JsonProperty("quiz_id")]
        public string QuizId { get; set; }
    }
}
