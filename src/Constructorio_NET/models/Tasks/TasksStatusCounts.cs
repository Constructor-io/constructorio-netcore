using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Status Counts
 */
namespace Constructorio_NET.Models
{
    public class TasksStatusCounts
    {
        [JsonProperty("QUEUED")]
        public int Queued { get; set; }

        [JsonProperty("DONE")]
        public int Done { get; set; }

        [JsonProperty("IN_PROGRESS")]
        public int InProgress { get; set; }

        [JsonProperty("FAILED")]
        public int Failed { get; set; }
    }
}