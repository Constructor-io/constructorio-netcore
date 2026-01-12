using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class RecommendationsRequestTest
    {
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private readonly string Pod = "Pod1";
        private readonly string Section = "Search Suggestions";
        private readonly string Term = "Apple";
        private readonly int NumResults = 5;
        private readonly string UserId = "user1";
        private readonly List<string> UserSegments = new List<string>() { "us", "desktop" };
        private readonly List<string> VariationIds = new List<string>() { "var1", "var2" };
        private readonly Dictionary<string, string> TestCells = new Dictionary<string, string>()
        {
            { "test1", "original" },
        };
        private readonly string IP = "1,2,3";
        private readonly string OS = "Mac";
        private UserInfo UserInfo;

        [OneTimeSetUp]
        public void Setup()
        {
            this.UserInfo = new UserInfo(ClientId, SessionId);
            this.UserInfo.SetUserId(this.UserId);
            this.UserInfo.SetUserSegments(this.UserSegments);
            this.UserInfo.SetUserAgent(this.OS);
            this.UserInfo.SetForwardedFor(this.IP);
        }

        [Test]
        public void GetRequestParameters()
        {
            RecommendationsRequest req = new RecommendationsRequest(this.Pod)
            {
                UserInfo = this.UserInfo,
                NumResults = this.NumResults,
                Section = this.Section,
                TestCells = this.TestCells,
                Term = this.Term
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.ClientId, requestParameters[Constants.CLIENT_ID]);
            Assert.AreEqual(this.SessionId, requestParameters[Constants.SESSION_ID]);
            Assert.AreEqual(this.UserId, requestParameters[Constants.USER_ID]);
            Assert.AreEqual(this.UserSegments, requestParameters[Constants.USER_SEGMENTS]);
            Assert.AreEqual(this.Section, requestParameters[Constants.SECTION]);
            Assert.AreEqual(this.NumResults, requestParameters["num_results"]);
            Assert.AreEqual(this.TestCells, requestParameters[Constants.TEST_CELLS]);
            Assert.AreEqual(this.Term, requestParameters["term"]);
        }

        [Test]
        public void GetRequestHeaders()
        {
            RecommendationsRequest req = new RecommendationsRequest(this.Pod)
            {
                UserInfo = this.UserInfo,
            };

            Dictionary<string, string> requestHeaders = req.GetRequestHeaders();
            Assert.AreEqual(this.OS, requestHeaders[Constants.USER_AGENT]);
            Assert.AreEqual(this.IP, requestHeaders[Constants.USER_IP]);
        }

        [Test]
        public void GetRequestParametersWithVariationIds()
        {
            RecommendationsRequest req = new RecommendationsRequest(this.Pod)
            {
                UserInfo = this.UserInfo,
                VariationIds = this.VariationIds
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.VariationIds, requestParameters["variation_id"]);
        }

        [Test]
        public void RecommendationsRequestWithInvalidPod()
        {
            Assert.Throws<ArgumentException>(() => new RecommendationsRequest(null));
        }

        [Test]
        public void GetRequestParametersWithPreFilterExpression()
        {
            ValuePreFilterExpression filterByGroupId = new ValuePreFilterExpression("group_id", "BrandXY");
            ValuePreFilterExpression filterByColor = new ValuePreFilterExpression("Color", "red");
            AndPreFilterExpression preFilterExpression = new AndPreFilterExpression(
                new List<PreFilterExpression> { filterByGroupId, filterByColor }
            );

            RecommendationsRequest req = new RecommendationsRequest(this.Pod)
            {
                UserInfo = this.UserInfo,
                PreFilterExpression = preFilterExpression
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.IsNotNull(requestParameters[Constants.PRE_FILTER_EXPRESSION]);
            Assert.IsTrue(requestParameters[Constants.PRE_FILTER_EXPRESSION].ToString().Contains("group_id"));
            Assert.IsTrue(requestParameters[Constants.PRE_FILTER_EXPRESSION].ToString().Contains("BrandXY"));
        }
    }
}
