using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io QuestionResponse Response
 */
namespace Constructorio_NET.Models
{
    public class QuestionResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("cta_text")]
        public string CTAText { get; set; }

        [JsonProperty("input_placeholder")]
        public string InputPlaceholder { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("images")]
        public QuestionImages Images { get; set; }

        [JsonProperty("options")]
        public List<QuestionOption> Options { get; set; }
    }
}
