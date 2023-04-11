using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class BrowseFacetsRequestTest
    {
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private readonly int Page = 2;
        private readonly int Offset = 10;
        private readonly int ResultsPerPage = 20;
        private readonly string IP = "1,2,3";
        private readonly string OS = "Mac";
        private UserInfo UserInfo;

        [OneTimeSetUp]
        public void Setup()
        {
            this.UserInfo = new UserInfo(ClientId, SessionId);
            this.UserInfo.SetUserAgent(this.OS);
            this.UserInfo.SetForwardedFor(this.IP);
        }

        [Test]
        public void GetRequestParameters()
        {
            BrowseFacetsRequest req = new BrowseFacetsRequest()
            {
                UserInfo = this.UserInfo,
                Page = this.Page,
                ResultsPerPage = this.ResultsPerPage,
                Offset = this.Offset,
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.ResultsPerPage, requestParameters[Constants.RESULTS_PER_PAGE]);
            Assert.AreEqual(this.Page, requestParameters[Constants.PAGE]);
            Assert.AreEqual(this.Offset, requestParameters[Constants.OFFSET]);
        }

        [Test]
        public void GetRequestHeaders()
        {
            BrowseFacetsRequest req = new BrowseFacetsRequest()
            {
                UserInfo = this.UserInfo,
            };

            Dictionary<string, string> requestHeaders = req.GetRequestHeaders();
            Assert.AreEqual(this.OS, requestHeaders[Constants.USER_AGENT]);
            Assert.AreEqual(this.IP, requestHeaders[Constants.USER_IP]);
        }
    }
}
