using System;
using Newtonsoft.Json;

/**
 * Constructor.io Recommendation Strategy
 */
namespace Constructorio_NET.Models
{
    public class RecommendationStrategy
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}