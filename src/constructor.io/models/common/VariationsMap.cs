﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Constructorio_NET.Models
{
    public class Value
    {
        [JsonProperty("aggregation")]
        public string Aggregation { get; set; }
        [JsonProperty("field")]
        public string Field { get; set; }
    }

    public class Group
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("field")]
        public string Field { get; set; }
    }

    public static class AggregationTypes
    {
        public const string First = "first";
        public const string Min = "min";
        public const string Max = "max";
        public const string All = "all";
    }

    public static class DTypes
    {
        public const string Array = "array";
        public const string Object = "object";
    }

    public class VariationsMap
    {
        [JsonProperty("group_by")]
        public List<Group> GroupBy { get; set; }

        [JsonProperty("values")]
        public Dictionary<string, Value> Values { get; set; }

        [JsonProperty("dtype")]
        public string DType { get; set; }

        [JsonProperty("filter_by", NullValueHandling = NullValueHandling.Ignore)]
        public JObject FilterBy { get; set; }

        public VariationsMap()
        {
            Values = new Dictionary<string, Value>();
            GroupBy = new List<Group>();
            DType = DTypes.Object;
        }

        public void AddGroupByRule(string name, string field)
        {
            if (this.GroupBy != null)
            {
                this.GroupBy.Add(new Group { Name = name, Field = field });
            }
            else
            {
                this.GroupBy = new List<Group>() { new Group { Name = name, Field = field } };
            }
        }

        public void AddValueRule(string name, string aggregation, string field)
        {
            if (this.Values != null)
            {
                this.Values.Add(name, new Value { Aggregation = aggregation, Field = field });
            }
            else
            {
                this.Values = new Dictionary<string, Value> { { name, new Value { Aggregation = aggregation, Field = field } } };
            }
        }

        public void AddFilterByRule(string filterByString)
        {
            this.FilterBy = JObject.Parse(filterByString);
        }

        public void AddFilterByRule(JObject filterBy)
        {
            this.FilterBy = filterBy;
        }
    }
}
