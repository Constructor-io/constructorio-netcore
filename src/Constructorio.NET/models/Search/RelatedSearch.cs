using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Related Search
 */
namespace Constructorio_NET.Models
{
    public class RelatedSearch
    {
        [JsonProperty("query")]
        public string Query { get; set; }
    }
}
