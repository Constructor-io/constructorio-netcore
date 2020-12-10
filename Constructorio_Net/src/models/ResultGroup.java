using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Item Group
 */
*/
namespace Constructorio_NET
{
    public class ResultGroup {

        [JsonPropertyName("display_name")]
        public String displayName;

        [JsonPropertyName("group_id")]
        public String groupId;

        [JsonPropertyName("path")]
        public String path;

    }
}