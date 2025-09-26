using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class RecommendationsTest
    {
        private readonly string ApiKey = "key_vM4GkLckwiuxwyRA";
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private ConstructorioConfig Config;
        private UserInfo UserInfo;

        [SetUp]
        public void Setup()
        {
            this.Config = new ConstructorioConfig(this.ApiKey);
            this.UserInfo = new UserInfo(ClientId, SessionId);
        }

        [Test]
        public void GetRecommendationsWithInvalidApiKeyShouldError()
        {
            RecommendationsRequest req = new RecommendationsRequest("item_page_1")
            {
                UserInfo = this.UserInfo,
                ItemIds = new List<string> { "power_drill" }
            };
            ConstructorIO constructorio = new ConstructorIO(new ConstructorioConfig("invalidKey"));
            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Recommendations.GetRecommendationsResults(req));
            Assert.IsTrue(ex.Message == "Http[400]: You have supplied an invalid `key` or `autocomplete_key`. You can find your key at app.constructor.io/dashboard/accounts/api_integration.", "Correct Error is Returned");
        }

        [Test]
        public async Task GetRecommendationsResults()
        {
            RecommendationsRequest req = new RecommendationsRequest("item_page_1")
            {
                UserInfo = this.UserInfo,
                ItemIds = new List<string> { "power_drill" }
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            RecommendationsResponse res = await constructorio.Recommendations.GetRecommendationsResults(req);

            Assert.GreaterOrEqual(res.Response.Results.Count, 0, "Results exist");
            Assert.NotNull(res.ResultId, "Result id exists");
        }

        [Test]
        public async Task GetRecommendationsResultsShouldReturnResultsWithStrategyId()
        {
            RecommendationsRequest req = new RecommendationsRequest("filtered_items")
            {
                Filters = new Dictionary<string, List<string>>()
                {
                    { "Brand", new List<string>() { "XYZ" } }
                },
                UserInfo = this.UserInfo,
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            RecommendationsResponse res = await constructorio.Recommendations.GetRecommendationsResults(req);

            Assert.AreEqual("filtered_items", res.Response.Results[0].Strategy.Id, "Strategy id exists");
            Assert.NotNull(res.ResultId, "Result id exists");
        }

        [Test]
        public async Task GetRecommendationsResultsShouldReturnAResultWithMultipleItemIds()
        {
            RecommendationsRequest req = new RecommendationsRequest("item_page_1")
            {
                UserInfo = this.UserInfo,
                ItemIds = new List<string> { "power_drill", "drill" }
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            RecommendationsResponse res = await constructorio.Recommendations.GetRecommendationsResults(req);

            Assert.GreaterOrEqual(res.Response.Results.Count, 0, "Results exist");
            Assert.NotNull(res.ResultId, "Result id exists");
        }

        [Test]
        public async Task GetRecommendationsResultsShouldReturnAResultWithNumResults()
        {
            RecommendationsRequest req = new RecommendationsRequest("item_page_1")
            {
                UserInfo = this.UserInfo,
                ItemIds = new List<string> { "power_drill", "drill" },
                NumResults = 5
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            RecommendationsResponse res = await constructorio.Recommendations.GetRecommendationsResults(req);

            Assert.GreaterOrEqual(res.Response.Results.Count, 0, "Results exist");
            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.AreEqual(5, res.Request["num_results"], "Num Results is set");
        }

        [Test]
        public async Task GetRecommendationsResultsShouldReturnAResultWithTerm()
        {
            RecommendationsRequest req = new RecommendationsRequest("query_recommendations")
            {
                UserInfo = this.UserInfo,
                Term = "apple",
                NumResults = 5
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            RecommendationsResponse res = await constructorio.Recommendations.GetRecommendationsResults(req);

            Assert.GreaterOrEqual(res.Response.Results.Count, 0, "Results exist");
            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.AreEqual("apple", res.Request["term"], "Term is set");
        }

        [Test]
        public async Task GetRecommendationsResultsShouldReturnAResultWithVariationsMap()
        {
            RecommendationsRequest req = new RecommendationsRequest("item_page_1")
            {
                UserInfo = this.UserInfo,
                ItemIds = new List<string> { "power_drill" },
                VariationsMap = new VariationsMap()
            };
            req.VariationsMap.AddGroupByRule("url", "data.url");
            req.VariationsMap.AddValueRule(
                "variation_id",
                AggregationTypes.First,
                "data.variation_id"
            );
            req.VariationsMap.AddValueRule(
                "deactivated",
                AggregationTypes.First,
                "data.deactivated"
            );
            req.VariationsMap.AddFilterByRule(
                "{\"and\":[{\"not\":{\"field\":\"data.brand\",\"value\":\"Best Brand\"}}]}"
            );
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            RecommendationsResponse res = await constructorio.Recommendations.GetRecommendationsResults(req);
            res.Request.TryGetValue("variations_map", out object reqVariationsMap);
            JObject variationMapResult = JObject.Parse(
                "{ \"filter_by\": { \"type\": \"and\", \"and\": [{ \"type\": \"not\", \"not\": { \"type\": \"single\", \"field\": \"data.brand\", \"value\": \"Best Brand\" }}]}, \"group_by\": [{ \"name\": \"url\", \"field\": \"data.url\"}], \"values\": { \"variation_id\": { \"aggregation\": \"first\", \"field\": \"data.variation_id\"}, \"deactivated\": { \"aggregation\": \"first\", \"field\": \"data.deactivated\"}}, \"dtype\": \"object\" }"
            );

            Assert.GreaterOrEqual(res.Response.Results.Count, 0, "Results exist");
            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.AreEqual(
                 JObject.Parse(reqVariationsMap.ToString()),
                 variationMapResult,
                 "Variations Map was passed as parameter"
             );
            foreach (var result in res.Response.Results)
            {
                Assert.NotNull(result.VariationsMap, "Variations Map exists in every result");
            }
        }

        [Test]
        public async Task GetRecommendationsResultsShouldReturnAResultWithVariationIds()
        {
            RecommendationsRequest req = new RecommendationsRequest("item_page_1")
            {
                UserInfo = this.UserInfo,
                ItemIds = new List<string> { "power_drill" },
                VariationIds = new List<string> { "variation_1", "variation_2" }
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            RecommendationsResponse res = await constructorio.Recommendations.GetRecommendationsResults(req);

            Assert.GreaterOrEqual(res.Response.Results.Count, 0, "Results exist");
            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.AreEqual(new List<string> { "variation_1", "variation_2" }, res.Request["variation_id"], "Variation IDs are set");
        }

        [Test]
        public async Task GetRecommendationsResultsShouldReturnAResultWithSingleVariationId()
        {
            RecommendationsRequest req = new RecommendationsRequest("item_page_1")
            {
                UserInfo = this.UserInfo,
                ItemIds = new List<string> { "power_drill" },
                VariationIds = new List<string> { "single_variation" }
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            RecommendationsResponse res = await constructorio.Recommendations.GetRecommendationsResults(req);

            Assert.GreaterOrEqual(res.Response.Results.Count, 0, "Results exist");
            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.AreEqual(new List<string> { "single_variation" }, res.Request["variation_id"], "Single Variation ID is set");
        }

        [Test]
        public async Task GetRecommendationsResultsShouldReturnAResultProvidedUserInfo()
        {
            RecommendationsRequest req = new RecommendationsRequest("item_page_1")
            {
                ItemIds = new List<string> { "power_drill", "drill" },
                UserInfo = new UserInfo(ClientId, SessionId)
            };
            req.UserInfo.SetUserId("123");
            req.UserInfo.SetUserSegments(new List<string>());
            req.UserInfo.GetUserSegments().Add("vs");
            req.UserInfo.GetUserSegments().Add("pink");
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            RecommendationsResponse res = await constructorio.Recommendations.GetRecommendationsResults(req);

            Assert.GreaterOrEqual(res.Response.Results.Count, 0, "Results exist");
            Assert.NotNull(res.ResultId, "Result id exists");
        }
    }
}
