using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Redirect Data
 */
namespace Constructorio_NET.Models
{
    public class RedirectData
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("rule_id")]
        public int RuleId { get; set; }

        [JsonProperty("match_id")]
        public int MatchId { get; set; }

        [JsonExtensionData]
        private Dictionary<string, object> _additionalData;

        /// <summary>
        /// Indexer for accessing arbitrary metadata keys
        /// </summary>
        public object this[string key]
        {
            get => _additionalData?.TryGetValue(key, out var value) == true ? value : null;
            set
            {
                _additionalData ??= new Dictionary<string, object>();
                _additionalData[key] = value;
            }
        }
    }
}