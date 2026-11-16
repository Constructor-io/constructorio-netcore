using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Constructorio_NET.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class RecommendationPageTest
    {
        private const string PageResultId = "a1b2c3d4-0000-0000-0000-0000000000ff";
        private const string PageResponseJson = @"{
            ""request"": { ""page_id"": ""pdp_b2c"", ""item_id"": ""product-123"", ""num_results"": 10 },
            ""response"": {
                ""page_id"": ""pdp_b2c"",
                ""display_name"": ""PDP - B2C"",
                ""page_type"": ""pdp"",
                ""pods"": [
                    {
                        ""pod_id"": ""similar_items"",
                        ""request"": { ""item_id"": ""product-123"", ""num_results"": 12 },
                        ""response"": {
                            ""results"": [ { ""data"": { ""id"": ""product-987"" }, ""value"": ""Red Running Shoe"", ""is_slotted"": false, ""labels"": {}, ""strategy"": { ""id"": ""alternative_items"" } } ],
                            ""total_num_results"": 1,
                            ""pod"": { ""id"": ""similar_items"", ""display_name"": ""Similar Items"" }
                        },
                        ""result_id"": ""a1b2c3d4-0000-0000-0000-000000000001""
                    },
                    {
                        ""pod_id"": ""complete_the_look"",
                        ""request"": { ""item_id"": ""product-123"", ""num_results"": 8 },
                        ""response"": {
                            ""results"": [],
                            ""total_num_results"": 0,
                            ""pod"": { ""id"": ""complete_the_look"", ""display_name"": ""Complete the Look"" }
                        },
                        ""result_id"": ""a1b2c3d4-0000-0000-0000-000000000002""
                    }
                ]
            },
            ""result_id"": ""a1b2c3d4-0000-0000-0000-0000000000ff""
        }";

        [Test]
        public void CreateRecommendationPageUrlShouldEncodePodOverridesInBracketNotation()
        {
            RecommendationPageRequest req = new RecommendationPageRequest("pdp_b2c")
            {
                Filters = new Dictionary<string, List<string>> { { "in_stock", new List<string> { "true" } } },
                PodOverrides = new Dictionary<string, RecommendationPagePodOverride>
                {
                    { "similar_items", new RecommendationPagePodOverride { NumResults = 0 } },
                    { "trending", null },
                    {
                        "complete_the_look", new RecommendationPagePodOverride
                        {
                            NumResults = 8,
                            Filters = new Dictionary<string, List<string>> { { "color", new List<string> { "red", "blue" } } },
                        }
                    },
                },
            };
            ConstructorIO constructorio = new ConstructorIO(new ConstructorioConfig("key_mock_page_api"));

            string url = constructorio.Recommendations.CreateRecommendationPageUrl(req);
            Uri uri = new Uri(url);
            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.AreEqual("/recommendations/v1/pages/pdp_b2c", uri.AbsolutePath);
            Assert.AreEqual("true", query["filters[in_stock]"]);
            Assert.AreEqual(new[] { "red", "blue" }, query.GetValues("pod_overrides[complete_the_look][filters][color]"));
            StringAssert.Contains("pod_overrides%5Bsimilar_items%5D%5Bnum_results%5D=0", url);
            StringAssert.DoesNotContain("trending", url);
        }

        [Test]
        public void RecommendationPageResponseShouldDeserializePerPodResultIds()
        {
            RecommendationPageResponse res = JsonConvert.DeserializeObject<RecommendationPageResponse>(PageResponseJson);
            List<RecommendationPagePod> pods = res.Response.Pods;

            Assert.AreEqual(PageResultId, res.ResultId);
            Assert.AreEqual(new[] { "similar_items", "complete_the_look" }, pods.Select(pod => pod.PodId).ToArray());
            Assert.AreEqual("a1b2c3d4-0000-0000-0000-000000000001", pods[0].ResultId);
            Assert.AreEqual("Red Running Shoe", pods[0].Response.Results[0].Value);
            Assert.AreEqual("a1b2c3d4-0000-0000-0000-000000000002", pods[1].ResultId);
        }

        [Test]
        [Ignore("requires a recommendation page configured on the test index")]
        public async Task GetRecommendationPageResultsLive()
        {
            RecommendationPageRequest req = new RecommendationPageRequest("pdp_b2c")
            {
                UserInfo = new UserInfo("r4nd-cl1ent-1d", 4),
                ItemIds = new List<string> { "power_drill" },
            };
            ConstructorIO constructorio = new ConstructorIO(new ConstructorioConfig("key_vM4GkLckwiuxwyRA"));

            RecommendationPageResponse res = await constructorio.Recommendations.GetRecommendationPageResults(req);

            Assert.Greater(res.Response.Pods.Count, 0);
            Assert.IsTrue(res.Response.Pods.All(pod => pod.ResultId != res.ResultId));
        }
    }
}
