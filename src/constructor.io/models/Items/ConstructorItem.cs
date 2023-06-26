using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Constructorio_NET.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ConstructorItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("suggested_score")]
        public float? SuggestedScore { get; set; }

        [JsonIgnore]
        public List<string> Keywords { get; set; }

        [JsonIgnore]
        public string Url { get; set; }

        [JsonIgnore]
        public string ImageUrl { get; set; }

        [JsonIgnore]
        public string Description { get; set; }

        [JsonIgnore]
        public Dictionary<string, List<object>> Facets { get; set; }

        [JsonIgnore]
        public Dictionary<string, object> Metadata { get; set; }

        [JsonIgnore]
        public List<string> GroupIds { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, object> Data
        {
            get
            {
                Dictionary<string, object> combinedData;

                if (Metadata != null)
                {
                    combinedData = new Dictionary<string, object>(Metadata);
                }
                else
                {
                    combinedData = new Dictionary<string, object>();
                }

                if (Keywords != null)
                {
                    combinedData.Add("keywords", Keywords);
                }

                if (!string.IsNullOrEmpty(Url))
                {
                    combinedData.Add("url", Url);
                }

                if (!string.IsNullOrEmpty(ImageUrl))
                {
                    combinedData.Add("image_url", ImageUrl);
                }

                if (Facets != null)
                {
                    combinedData.Add("facets", Facets);
                }

                if (GroupIds != null)
                {
                    combinedData.Add("group_ids", GroupIds);
                }

                if (!string.IsNullOrEmpty(Description))
                {
                    combinedData.Add("description", Description);
                }

                return combinedData;
            }
            set
            {
                if (value != null)
                {
                    object dictVal;
                    if (value.TryGetValue("url", out dictVal))
                    {
                        this.Url = (string)dictVal;
                        value.Remove("url");
                    }

                    if (value.TryGetValue("image_url", out dictVal))
                    {
                        this.ImageUrl = (string)dictVal;
                        value.Remove("image_url");
                    }

                    if (value.TryGetValue("description", out dictVal))
                    {
                        this.Description = (string)dictVal;
                        value.Remove("description");
                    }

                    if (value.TryGetValue("group_ids", out dictVal))
                    {
                        this.GroupIds = ((JArray)dictVal).ToObject<List<string>>();
                        value.Remove("group_ids");
                    }

                    if (value.TryGetValue("facets", out dictVal))
                    {
                        this.Facets = ((JObject)dictVal).ToObject<Dictionary<string, List<object>>>();
                        value.Remove("facets");
                    }

                    this.Metadata = new Dictionary<string, object>(value);
                }
            }
        }

        public bool ShouldSerializeData()
        {
            return Data != null && Data.Count > 0;
        }

        public ConstructorItem(string Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public ConstructorItem(string Id)
        {
            this.Id = Id;
        }

        public ConstructorItem()
        {
        }
    }
}
