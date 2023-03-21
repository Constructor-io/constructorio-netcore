using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Constructorio_NET.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public abstract class PreFilterExpression
    {
        public virtual string GetExpression()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class JsonPrefilterExpression : PreFilterExpression
    {
        private JObject JsonObject { get; set; }
        public override string GetExpression()
        {
            return JsonConvert.SerializeObject(JsonObject);
        }

        public JsonPrefilterExpression(JObject customJson)
        {
            JsonObject = customJson;
        }

        public JsonPrefilterExpression(string customJsonString)
        {
            JsonObject = JObject.Parse(customJsonString);
        }

        public JsonPrefilterExpression() { }
    }

    public class OrPreFilterExpression : PreFilterExpression
    {
        [JsonProperty("or")]
        public List<PreFilterExpression> Or { get; set; }

        public OrPreFilterExpression(List<PreFilterExpression> expressions)
        {
            Or = expressions;
        }

        public OrPreFilterExpression() { }
    }

    public class AndPreFilterExpression : PreFilterExpression
    {
        [JsonProperty("and")]
        public List<PreFilterExpression> And { get; set; }

        public AndPreFilterExpression(List<PreFilterExpression> expressions)
        {
            And = expressions;
        }

        public AndPreFilterExpression() { }
    }

    public class NotPreFilterExpression : PreFilterExpression
    {
        [JsonProperty("not")]
        public PreFilterExpression Not { get; set; }

        public NotPreFilterExpression(PreFilterExpression expression)
        {
            Not = expression;
        }

        public NotPreFilterExpression() { }
    }

    public abstract class PreFilterExpressionBase : PreFilterExpression
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class ValuePreFilterExpression : PreFilterExpressionBase
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        public ValuePreFilterExpression(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    public class RangePreFilterExpression : PreFilterExpressionBase
    {
        [JsonProperty("range")]
        public List<string> Range { get; set; }

        public RangePreFilterExpression(string name, List<string> range)
        {
            Name = name;
            Range = range;
        }
    }
}
