using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class AutocompleteRequestTest
    {
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private readonly string Query = "item";
        private readonly Dictionary<string, List<string>> Filters = new Dictionary<string, List<string>>()
        {
            { "Color", new List<string>() { "green", "blue" } }
        };
        private readonly Dictionary<string, Dictionary<string, List<string>>> FiltersPerSection = new Dictionary<string, Dictionary<string, List<string>>>()
        {
            { "Products", new Dictionary<string, List<string>>() { { "Color", new List<string>() { "green", "blue" } } } }
        };
        private readonly string UserId = "user1";
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
            AutocompleteRequest req = new AutocompleteRequest(this.Query)
            {
                UserInfo = this.UserInfo,
                Filters = this.Filters,
                TestCells = this.TestCells,
                FiltersPerSection = this.FiltersPerSection,
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.ClientId, requestParameters[Constants.CLIENT_ID]);
            Assert.AreEqual(this.SessionId, requestParameters[Constants.SESSION_ID]);
            Assert.AreEqual(this.UserId, requestParameters[Constants.USER_ID]);
            Assert.AreEqual(this.UserSegments, requestParameters[Constants.USER_SEGMENTS]);
            Assert.AreEqual(this.Filters, requestParameters[Constants.FILTERS]);
            Assert.AreEqual(this.TestCells, requestParameters[Constants.TEST_CELLS]);
            Assert.AreEqual(this.FiltersPerSection, requestParameters[Constants.FILTERS_PER_SECTION]);
        }

        [Test]
        public void GetRequestHeaders()
        {
            AutocompleteRequest req = new AutocompleteRequest(this.Query)
            {
                UserInfo = this.UserInfo,
            };

            Dictionary<string, string> requestHeaders = req.GetRequestHeaders();
            Assert.AreEqual(this.OS, requestHeaders[Constants.USER_AGENT]);
            Assert.AreEqual(this.IP, requestHeaders[Constants.USER_IP]);
        }

        [Test]
        public void AutocompleteRequestWithInvalidQuery()
        {
            Assert.Throws<ArgumentException>(() => new AutocompleteRequest(null));
        }
    }
}
