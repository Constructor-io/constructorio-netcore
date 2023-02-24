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
        public QuestionResponse NextQuestion { get; set; }

        [JsonProperty("is_last_question")]
        public bool IsLastQuestion { get; set; }

        [JsonProperty("version_id")]
        public string VersionId { get; set; }
    }
}