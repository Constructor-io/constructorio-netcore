using System;
using Newtonsoft.Json;

/**
 * Constructor.io Task Response
 */
namespace Constructorio_NET.Models.Catalog
{
  public class CatalogResponse
  {
    [JsonProperty("task_id")]
    public int TaskId { get; set; }

    [JsonProperty("task_status_path")]
    public string TaskStatusPath { get; set; }
  }
}