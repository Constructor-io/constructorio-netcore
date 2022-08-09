using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Task
 */
namespace Constructorio_NET.Models
{
    public class TaskResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("submission_time")]
        public string SubmissionTime { get; set; }

        [JsonProperty("start_time")]
        public string StartTime { get; set; }

        [JsonProperty("last_update")]
        public string LastUpdate { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonProperty("result")]
        public Dictionary<string, object> Result { get; set; }
    }
}