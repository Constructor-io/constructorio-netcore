using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class RecommendationPageRequestTest
    {
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private readonly string UserId = "user1";
        private readonly string IP = "1,2,3";
        private readonly string OS = "Mac";
        private UserInfo UserInfo;

        [OneTimeSetUp]
        public void Setup()
        {
            this.UserInfo = new UserInfo(ClientId, SessionId);
            this.UserInfo.SetUserId(this.UserId);
            this.UserInfo.SetUserAgent(this.OS);
            this.UserInfo.SetForwardedFor(this.IP);
        }

        [Test]
        public void GetRequestParametersWithSharedValues()
        {
            var filters = new Dictionary<string, List<string>> { { "in_stock", new List<string> { "true" } } };
            RecommendationPageRequest req = new RecommendationPageRequest("pdp_b2c")
            {
                UserInfo = this.UserInfo,
                ItemIds = new List<string> { "product-123" },
                NumResults = 10,
                Section = "Products",
                Filters = filters,
                FilterMatchTypes = new Dictionary<string, string> { { "color", "any" } },
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.ClientId, requestParameters[Constants.CLIENT_ID]);
            Assert.AreEqual(this.SessionId, requestParameters[Constants.SESSION_ID]);
            Assert.AreEqual(this.UserId, requestParameters[Constants.USER_ID]);
            Assert.AreEqual("Products", requestParameters[Constants.SECTION]);
            Assert.AreEqual(new List<string> { "product-123" }, requestParameters["item_id"]);
            Assert.AreEqual(10, requestParameters["num_results"]);
            Assert.AreEqual(new List<string> { "true" }, requestParameters["filters[in_stock]"]);
            Assert.AreEqual("any", requestParameters["filter_match_types[color]"]);
        }

        [Test]
        public void GetRequestParametersWithPodOverrides()
        {
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
            Assert.AreEqual("2", requestParameters[$"{prefix}[fmt_options][groups_max_depth]"]);
            Assert.AreEqual(new List<string> { "inventory", "margin" }, requestParameters[$"{prefix}[fmt_options][hidden_fields]"]);
        }

        [Test]
        public void GetRequestHeaders()
        {
            RecommendationPageRequest req = new RecommendationPageRequest("pdp_b2c") { UserInfo = this.UserInfo };

            Dictionary<string, string> requestHeaders = req.GetRequestHeaders();
            Assert.AreEqual(this.IP, requestHeaders[Constants.USER_IP]);
            Assert.AreEqual(this.OS, requestHeaders[Constants.USER_AGENT]);
        }

        [Test]
        public void RecommendationPageRequestWithoutPageIdShouldError()
        {
            Assert.Throws<ArgumentException>(() => new RecommendationPageRequest(null));
        }
    }
}
