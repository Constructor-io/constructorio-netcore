using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class SearchRequestTest
    {
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private readonly string Query = "item";
        private readonly Dictionary<string, List<string>> Filters = new Dictionary<string, List<string>>()
        {
            { "Color", new List<string>() { "green", "blue" } }
        };
        private readonly int Page = 2;
        private readonly string Section = "Search Suggestions";
        private readonly string SortBy = "Price";
        private readonly string SortOrder = "Ascending";
        private readonly string UserId = "user1";
        private readonly List<string> UserSegments = new List<string>() { "us", "desktop" };
        private UserInfo UserInfo;

        [SetUp]
        public void Setup()
        {
            this.UserInfo = new UserInfo(ClientId, SessionId);
            this.UserInfo.SetUserId(this.UserId);
            this.UserInfo.SetUserSegments(this.UserSegments);
        }

        [Test]
        public void GetRequestParameters()
        {
            SearchRequest req = new SearchRequest(this.Query)
            {
                UserInfo = this.UserInfo,
                Page = this.Page,
                Section = this.Section,
                SortBy = this.SortBy,
                SortOrder = SortOrder,
                Filters = this.Filters,
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.ClientId, requestParameters[Constants.CLIENT_ID]);
            Assert.AreEqual(this.SessionId, requestParameters[Constants.SESSION_ID]);
            Assert.AreEqual(this.UserId, requestParameters[Constants.USER_ID]);
            Assert.AreEqual(this.UserSegments, requestParameters[Constants.USER_SEGMENTS]);
            Assert.AreEqual(this.Page, requestParameters[Constants.PAGE]);
            Assert.AreEqual(this.Section, requestParameters[Constants.SECTION]);
            Assert.AreEqual(this.SortBy, requestParameters[Constants.SORT_BY]);
            Assert.AreEqual(this.SortOrder, requestParameters[Constants.SORT_ORDER]);
            Assert.AreEqual(this.Filters, requestParameters[Constants.FILTERS]);
        }

        [Test]
        public void GetBrowseItemsResultsWithInvalidQuery()
        {
            Assert.Throws<ArgumentException>(() => new SearchRequest(null));
        }
    }
}
