using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Catalog Response
 */
namespace Constructorio_NET.Models
{
    public class CatalogResponse
    {
        [JsonProperty("task_id")]
        public int TaskId { get; set; }

        [JsonProperty("task_status_path")]
        public string TaskStatusPath { get; set; }
    }
}