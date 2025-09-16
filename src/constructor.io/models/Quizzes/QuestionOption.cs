using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io QuestionOption Response
 */
namespace Constructorio_NET.Models
{
    public class QuestionOption
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("attribute")]
        public OptionAttribute Attribute { get; set; }

        [JsonProperty("images")]
        public QuestionImages Images { get; set; }
    }
}