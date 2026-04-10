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
        public void GetRequestParametersWithShowHiddenFacets()
        {
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest(FilterName)
            {
                FmtOptions = new FmtOptions { ShowHiddenFacets = true },
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual("true", requestParameters[$"{Constants.FMT_OPTIONS}[{Constants.SHOW_HIDDEN_FACETS}]"]);
        }

        [Test]
        public void GetRequestParametersWithShowProtectedFacets()
        {
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest(FilterName)
            {
                FmtOptions = new FmtOptions { ShowProtectedFacets = true },
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual("true", requestParameters[$"{Constants.FMT_OPTIONS}[{Constants.SHOW_PROTECTED_FACETS}]"]);
        }

        [Test]
        public void GetRequestParametersWithFmtOptions()
        {
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest(FilterName)
            {
                FmtOptions = new FmtOptions()
                {
                    GroupsMaxDepth = 3,
                    ShowHiddenFacets = true,
                    ShowProtectedFacets = true,
                },
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual("3", requestParameters[$"{Constants.FMT_OPTIONS}[{Constants.GROUPS_MAX_DEPTH}]"]);
            Assert.AreEqual("true", requestParameters[$"{Constants.FMT_OPTIONS}[{Constants.SHOW_HIDDEN_FACETS}]"]);
            Assert.AreEqual("true", requestParameters[$"{Constants.FMT_OPTIONS}[{Constants.SHOW_PROTECTED_FACETS}]"]);
        }

        [Test]
        public void GetRequestParametersWithFmtOptionsAndShowHiddenFacets()
        {
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest(FilterName)
            {
                FmtOptions = new FmtOptions()
                {
                    GroupsMaxDepth = 3,
                    ShowHiddenFacets = true,
                },
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual("3", requestParameters[$"{Constants.FMT_OPTIONS}[{Constants.GROUPS_MAX_DEPTH}]"]);
            Assert.AreEqual("true", requestParameters[$"{Constants.FMT_OPTIONS}[{Constants.SHOW_HIDDEN_FACETS}]"]);
        }

        [Test]
        public void GetRequestHeaders()
        {
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest(FilterName)
            {
                UserInfo = this.UserInfo,
            };

            Dictionary<string, string> requestHeaders = req.GetRequestHeaders();
            Assert.AreEqual(this.OS, requestHeaders[Constants.USER_AGENT]);
            Assert.AreEqual(this.IP, requestHeaders[Constants.USER_IP]);
        }
    }
}
