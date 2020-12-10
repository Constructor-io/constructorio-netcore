using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Recommendations Response Inner
 **/
namespace Constructorio_NET
{
    public class RecommendationsResponseInner {

        [JsonPropertyName("results")]
        public List<Result> results;

        [JsonPropertyName("total_num_results")]
        public Integer totalNumberOfResults;

        [JsonPropertyName("pod")]
        public ResultPod pod;

    }
}