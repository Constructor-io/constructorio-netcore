using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Recommendations Response Inner
 */
namespace Constructorio_NET
{
    public class RecommendationsResponseInner {

        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("total_num_results")]
        public Int16 TotalNumberOfResults { get; set; }

        [JsonProperty("pod")]
        public ResultPod Pod { get; set; }

    }
}