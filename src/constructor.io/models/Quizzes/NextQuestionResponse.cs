using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io NextQuestionResponse Response
 */
namespace Constructorio_NET.Models
{
    public class NextQuestionResponse
    {
        [JsonProperty("next_question")]
        public NextQuestion NextQuestion { get; set; }

        [JsonProperty("quiz_version_id")]
        public string QuizVersionId { get; set; }

        [JsonProperty("quiz_session_id")]
        public string QuizSessionId { get; set; }

        [JsonProperty("quiz_id")]
        public string QuizId { get; set; }
    }
}