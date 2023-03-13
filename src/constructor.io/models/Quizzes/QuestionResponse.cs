using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

/**
 * Constructor.io NextQuestion Response
 */
namespace Constructorio_NET.Models
{
    public enum QuestionType
    {
        [EnumMember(Value = "single")]
        Single,
        [EnumMember(Value = "multiple")]
        Multiple,
        [EnumMember(Value = "open")]
        Open,
        [EnumMember(Value = "cover")]
        Cover,
    }

    public class NextQuestion
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
        public QuestionType Type { get; set; }

        [JsonProperty("images")]
        public QuestionImages Images { get; set; }

        [JsonProperty("options")]
        public List<QuestionOption> Options { get; set; }
    }
}
