using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class RecommendationsTest
    {
        private string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private string ClientId = "r4nd-cl1ent-1d";
        private Hashtable Options = new Hashtable();
        private string Query = "item";
        private int SessionId = 4;
        private Hashtable UserParameters = new Hashtable();

        [SetUp]
        public void Setup()
        {
            this.Options = new Hashtable()
            {
               { Constants.API_KEY, this.ApiKey }
            };
            this.UserParameters = new Hashtable()
            {
                { "clientId", ClientId },
                { "sessionId", SessionId }
            };
        }

        [Test]
        public void GetRecommendationsResults()
        {
            RecommendationsRequest req = new RecommendationsRequest("item_page_1");
            req.ItemId = new List<String> { "power_drill"};
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            RecommendationsResponse res = constructorio.Recommendations.GetRecommendationsResults(req);

            Assert.GreaterOrEqual(res.Response.Results.Count, 0, "Results exist");
            Assert.NotNull(res.ResultId, "Result id exists");
        }

        [Test]
        public void GetRecommendationsResultsShouldReturnAResultWithMultipleItemIds()
        {
            RecommendationsRequest req = new RecommendationsRequest("item_page_1");
            req.ItemId = new List<String> { "power_drill", "drill" };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            RecommendationsResponse res = constructorio.Recommendations.GetRecommendationsResults(req);

            Assert.GreaterOrEqual(res.Response.Results.Count, 0, "Results exist");
            Assert.NotNull(res.ResultId, "Result id exists");
        }

        [Test]
        public void GetRecommendationsResultsShouldReturnAResultWithNumResults()
        {
            RecommendationsRequest req = new RecommendationsRequest("item_page_1");
            req.ItemId = new List<String> { "power_drill", "drill" };
            req.NumResults = 5;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            RecommendationsResponse res = constructorio.Recommendations.GetRecommendationsResults(req);

            Assert.GreaterOrEqual(res.Response.Results.Count, 0, "Results exist");
            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.AreEqual(res.Request["num_results"], 5, "Num Results is set");
        }

        [Test]
        public void GetRecommendationsResultsShouldReturnAResultProvidedUserInfo()
        {
            RecommendationsRequest req = new RecommendationsRequest("item_page_1");
            req.ItemId = new List<String> { "power_drill", "drill" };
            req.UserInfo = new UserInfo(4, ClientId);
            req.UserInfo.setUserId("123");
            req.UserInfo.setUserSegments(new List<String>());
            req.UserInfo.getUserSegments().Add("vs");
            req.UserInfo.getUserSegments().Add("pink");
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            RecommendationsResponse res = constructorio.Recommendations.GetRecommendationsResults(req);

            Assert.GreaterOrEqual(res.Response.Results.Count, 0, "Results exist");
            Assert.NotNull(res.ResultId, "Result id exists");
        }
    }
}