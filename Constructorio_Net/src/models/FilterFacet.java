using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Filter Facet
 **/
namespace Constructorio_NET
{
    public class 
{
    public class FilterFacet {

  [JsonPropertyName("display_name")]
  private String displayName;

  [JsonPropertyName("name")]
  private String name;

  [JsonPropertyName("status")]
  private Map<String, Object> status;

  [JsonPropertyName("max")]
  private Double max;

  [JsonPropertyName("min")]
  private Double min;

  [JsonPropertyName("options")]
  private List<FilterFacetOption> options;

  [JsonPropertyName("type")]
  private String type;

  /**
   * @return the displayName
   */
  public String getDisplayName() {
    return displayName;
  }

  /**
   * @return the name
   */
  public String getName() {
    return name;
  }

  /**
   * @return the status
   */
  public Map<String, Object> getStatus() {
    return status;
  }

  /**
   * @return the max
   */
  public Double getMax() {
    return max;
  }

  /**
   * @return the min
   */
  public Double getMin() {
    return min;
  }

  /**
   * @return the options
   */
  public List<FilterFacetOption> getOptions() {
    return options;
  }

  /**
   * @return the type
   */
  public String getType() {
    return type;
  }
}