using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class BrowseRequestTest
    {
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private readonly string UserId = "user1";
        private readonly int Page = 2;
        private readonly int Offset = 4;
        private readonly string FilterName = "Color";
        private readonly string FilterValue = "Blue";
        private readonly string Section = "Search Suggestions";
        private readonly string SortBy = "Price";
        private readonly string SortOrder = "Ascending";
        private readonly List<string> UserSegments = new List<string>() { "us", "desktop" };
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
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue)
            {
                UserInfo = this.UserInfo,
                Page = this.Page,
                Offset = this.Offset,
                Section = this.Section,
                SortBy = this.SortBy,
                SortOrder = SortOrder,
                TestCells = this.TestCells,
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.ClientId, requestParameters[Constants.CLIENT_ID]);
            Assert.AreEqual(this.SessionId, requestParameters[Constants.SESSION_ID]);
            Assert.AreEqual(this.UserId, requestParameters[Constants.USER_ID]);
            Assert.AreEqual(this.UserSegments, requestParameters[Constants.USER_SEGMENTS]);
            Assert.AreEqual(this.Page, requestParameters[Constants.PAGE]);
            Assert.AreEqual(this.Offset, requestParameters[Constants.OFFSET]);
            Assert.AreEqual(this.Section, requestParameters[Constants.SECTION]);
            Assert.AreEqual(this.SortBy, requestParameters[Constants.SORT_BY]);
            Assert.AreEqual(this.SortOrder, requestParameters[Constants.SORT_ORDER]);
            Assert.AreEqual(this.TestCells, requestParameters[Constants.TEST_CELLS]);
        }

        [Test]
        public void GetRequestHeaders()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue)
            {
                UserInfo = this.UserInfo,
            };

            Dictionary<string, string> requestHeaders = req.GetRequestHeaders();
            Assert.AreEqual(this.OS, requestHeaders[Constants.USER_AGENT]);
            Assert.AreEqual(this.IP, requestHeaders[Constants.USER_IP]);
        }

        [Test]
        public void BrowseRequestWithInvalidFilters()
        {
            Assert.Throws<ArgumentException>(() => new BrowseRequest(null, null));
        }
    }
}
