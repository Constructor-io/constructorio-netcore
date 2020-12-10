using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Filter Facet Option
 **/
namespace Constructorio_NET
{
    public class FilterFacetOption {

  [JsonPropertyName("count")]
  private Integer count;

  [JsonPropertyName("data")]
  private Map<String, Object> data;

  [JsonPropertyName("display_name")]
  private String displayName;

  [JsonPropertyName("status")]
  private String status;

  [JsonPropertyName("value")]
  private String value;

  /**
   * @return the counts
   */
  public Integer getCount() {
    return count;
  }

  /**
   * @return the data
   */
  public Map<String, Object> getData() {
    return data;
  }

  /**
   * @return the displayName
   */
  public String getDisplayName() {
    return displayName;
  }

  /**
   * @return the status
   */
  public String getStatus() {
    return status;
  }

  /**
   * @return the value
   */
  public String getValue() {
    return value;
  }
}