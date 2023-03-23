using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io QuestionImages Response
 */
namespace Constructorio_NET.Models
{
    public class QuestionImages
    {
        [JsonProperty("primary_url")]
        public string PrimaryUrl { get; set; }

        [JsonProperty("primary_alt")]
        public string PrimaryAlt { get; set; }

        [JsonProperty("secondary_url")]
        public string SecondaryUrl { get; set; }

        [JsonProperty("secondary_alt")]
        public string SecondaryAlt { get; set; }
    }
}
