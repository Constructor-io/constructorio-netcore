using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Filter Group
 **/
namespace Constructorio_NET
{
    public class FilterGroup {

    [JsonPropertyName("children")]
    private List<FilterGroup> children;

    [JsonPropertyName("parents")]
    private List<FilterGroup> parents;

    [JsonPropertyName("count")]
    private Integer count;

    [JsonPropertyName("display_name")]
    private String displayName;

    [JsonPropertyName("group_id")]
    private String groupId;

    /**
     * @return the children
     */
    public List<FilterGroup> getChildren() {
      return children;
    }

    /**
     * @return the parents
     */
    public List<FilterGroup> getParents() {
      return parents;
    }

    /**
     * @return the count
     */
    public Integer getCount() {
      return count;
    }

    /**
     * @return the displayName
     */
    public String getDisplayName() {
      return displayName;
    }

    /**
     * @return the groupId
     */
    public String getGroupId() {
      return groupId;
    }
}