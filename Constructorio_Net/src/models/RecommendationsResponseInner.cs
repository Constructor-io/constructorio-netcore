using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Recommendations Response Inner
 */
namespace Constructorio_NET
{
    public class RecommendationsResponseInner {

        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }

        [JsonPropertyName("total_num_results")]
        public Int16 TotalNumberOfResults { get; set; }

        [JsonPropertyName("pod")]
        public ResultPod Pod { get; set; }

    }
}