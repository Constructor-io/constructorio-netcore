using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Tasks Response
 */
namespace Constructorio_NET.Models
{
    public class AllTasksResponse
    {

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("tasks")]
        public List<TaskResponse> Tasks { get; set; }

        [JsonProperty("status_counts")]
        public TasksStatusCounts StatusCounts { get; set; }
    }
}