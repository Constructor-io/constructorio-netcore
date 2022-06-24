using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Task Response
 */
namespace Constructorio_NET
{
  public class TaskResponse
  {

    [JsonProperty("task_id")]
    public Int32 TaskId { get; set; }

    [JsonProperty("task_status_path")]
    public string TaskStatusPath { get; set; }
  }
}