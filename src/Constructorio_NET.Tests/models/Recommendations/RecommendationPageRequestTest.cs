using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class RecommendationPageRequestTest
    {
        [Test]
        public void GetRequestParametersWithPodOverrides()
        {
            VariationsMap variationsMap = new VariationsMap();
            variationsMap.AddValueRule("variation_id", AggregationTypes.First, "data.variation_id");
            RecommendationPageRequest req = new RecommendationPageRequest("pdp_b2c")
            {
                NumResults = 10,
                PodOverrides = new Dictionary<string, RecommendationPagePodOverride>
                {
                    { "similar_items", new RecommendationPagePodOverride { NumResults = 0 } },
                    {
                        "complete_the_look", new RecommendationPagePodOverride
                        {
                            NumResults = 8,
                            Filters = new Dictionary<string, List<string>> { { "color", new List<string> { "red", "blue" } } },
                            FilterMatchTypes = new Dictionary<string, string> { { "color", "all" } },
                            PreFilterExpression = new JsonPrefilterExpression("{\"name\":\"brand\",\"value\":\"acme\"}"),
                            VariationsMap = variationsMap,
                            FmtOptions = new FmtOptions { GroupsMaxDepth = 2, HiddenFields = new List<string> { "inventory", "margin" } },
                        }
                    },
                },
            };

            Hashtable requestParameters = req.GetRequestParameters();
            string prefix = "pod_overrides[complete_the_look]";
            Assert.AreEqual(10, requestParameters["num_results"]);
            Assert.AreEqual(0, requestParameters["pod_overrides[similar_items][num_results]"]);
            Assert.AreEqual(8, requestParameters[$"{prefix}[num_results]"]);
            Assert.AreEqual(new List<string> { "red", "blue" }, requestParameters[$"{prefix}[filters][color]"]);
            Assert.AreEqual("all", requestParameters[$"{prefix}[filter_match_types][color]"]);
            Assert.AreEqual("{\"name\":\"brand\",\"value\":\"acme\"}", requestParameters[$"{prefix}[pre_filter_expression]"]);
            Assert.AreEqual(JsonConvert.SerializeObject(variationsMap), requestParameters[$"{prefix}[variations_map]"]);
            Assert.AreEqual("2", requestParameters[$"{prefix}[fmt_options][groups_max_depth]"]);
            Assert.AreEqual(new List<string> { "inventory", "margin" }, requestParameters[$"{prefix}[fmt_options][hidden_fields]"]);
        }

        [Test]
        public void GetRequestParametersWithPageRequestAsPodOverrideShouldError()
        {
            RecommendationPageRequest req = new RecommendationPageRequest("pdp_b2c")
            {
                PodOverrides = new Dictionary<string, RecommendationPagePodOverride>
                {
                    { "similar_items", new RecommendationPageRequest("pdp_b2c") { ItemIds = new List<string> { "product-123" } } },
                },
            };

            var ex = Assert.Throws<ArgumentException>(() => req.GetRequestParameters());
            StringAssert.Contains("PodOverrides.similar_items", ex.Message);
        }

        [Test]
        public void RecommendationPageRequestWithoutPageIdShouldError()
        {
            Assert.Throws<ArgumentException>(() => new RecommendationPageRequest(null));
        }
    }
}
