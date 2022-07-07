using Constructorio_NET.Models;
using Constructorio_NET.Utils;
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
        private int SessionId = 4;

        [SetUp]
        public void Setup()
        {
            this.Options = new Hashtable()
            {
               { Constants.API_KEY, this.ApiKey }
            };
        }

        [Test]
        public void GetRecommendationsResults()
        {
            RecommendationsRequest req = new RecommendationsRequest("item_page_1");
            req.ItemId = new List<string> { "power_drill"};
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            RecommendationsResponse res = constructorio.Recommendations.GetRecommendationsResults(req);

            Assert.GreaterOrEqual(res.Response.Results.Count, 0, "Results exist");
            Assert.NotNull(res.ResultId, "Result id exists");
        }

        [Test]
        public void GetRecommendationsResultsShouldReturnAResultWithMultipleItemIds()
        {
            RecommendationsRequest req = new RecommendationsRequest("item_page_1");
            req.ItemId = new List<string> { "power_drill", "drill" };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            RecommendationsResponse res = constructorio.Recommendations.GetRecommendationsResults(req);

            Assert.GreaterOrEqual(res.Response.Results.Count, 0, "Results exist");
            Assert.NotNull(res.ResultId, "Result id exists");
        }

        [Test]
        public void GetRecommendationsResultsShouldReturnAResultWithNumResults()
        {
            RecommendationsRequest req = new RecommendationsRequest("item_page_1");
            req.ItemId = new List<string> { "power_drill", "drill" };
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
            req.ItemId = new List<string> { "power_drill", "drill" };
            req.UserInfo = new UserInfo(ClientId, SessionId);
            req.UserInfo.SetUserId("123");
            req.UserInfo.SetUserSegments(new List<string>());
            req.UserInfo.GetUserSegments().Add("vs");
            req.UserInfo.GetUserSegments().Add("pink");
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            RecommendationsResponse res = constructorio.Recommendations.GetRecommendationsResults(req);

            Assert.GreaterOrEqual(res.Response.Results.Count, 0, "Results exist");
            Assert.NotNull(res.ResultId, "Result id exists");
        }
    }
}