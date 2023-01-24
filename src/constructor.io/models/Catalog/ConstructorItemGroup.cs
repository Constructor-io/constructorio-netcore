using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Constructorio_NET.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ConstructorItemGroup
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("children")]
        public List<ConstructorItemGroup> Children { get; set; }

        [JsonProperty("data")]
        public JObject Data { get; set; }

        public ConstructorItemGroup()
        {
        }

        public ConstructorItemGroup(string Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public ConstructorItemGroup(string Id, string Name, JObject Data)
        {
            this.Id = Id;
            this.Name = Name;
            this.Data = Data;
        }

        public ConstructorItemGroup(string ParentId, string Id, string Name)
        {
            this.ParentId = ParentId;
            this.Id = Id;
            this.Name = Name;
        }

        public ConstructorItemGroup(string ParentId, string Id, string Name, JObject Data)
        {
            this.ParentId = ParentId;
            this.Id = Id;
            this.Name = Name;
            this.Data = Data;
        }
    }
}
