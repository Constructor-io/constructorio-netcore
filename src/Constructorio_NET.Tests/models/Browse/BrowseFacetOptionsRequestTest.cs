using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class BrowseFacetOptionsRequestTest
    {
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private readonly string FilterName = "Color";
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
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest(FilterName)
            {
                UserInfo = this.UserInfo,
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.FilterName, requestParameters[Constants.FACET_NAME]);
        }

        [Test]
        public void GetRequestHeaders()
        {
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest(FilterName)
            {
                UserInfo = this.UserInfo,
            };

            Dictionary<string, string> requestParameters = req.GetRequestHeaders();
            Assert.AreEqual(this.OS, requestParameters[Constants.USER_AGENT]);
            Assert.AreEqual(this.IP, requestParameters[Constants.USER_IP]);
        }
    }
}
