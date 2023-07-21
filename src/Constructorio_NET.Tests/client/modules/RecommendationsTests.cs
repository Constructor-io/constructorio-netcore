using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Constructorio_NET.Models;
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