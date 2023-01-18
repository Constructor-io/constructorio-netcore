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
        private string _Data;

        public string Data
        {
            get
            {
                return _Data;
            }

            set
            {
                try
                {
                    JObject.Parse(value);
                    _Data = value;
                }
                catch
                {
                    throw new ConstructorException("Data is not valid JSON");
                }
            }
        }

        public ConstructorItemGroup()
        {
        }

        public ConstructorItemGroup(string Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public ConstructorItemGroup(string ParentId, string Id, string Name)
        {
            this.ParentId = ParentId;
            this.Id = Id;
            this.Name = Name;
        }

        public ConstructorItemGroup(string ParentId, string Id, string Name, string Data)
        {
            this.ParentId = ParentId;
            this.Id = Id;
            this.Name = Name;
            this.Data = Data;
        }
    }
}
