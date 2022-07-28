using System;
using System.Collections;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class UserInfoTest : Helpers
    {
        private readonly string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private readonly string ServiceUrl = "https://ac.cnstrc.com";
        private readonly string Version = "cionet-5.6.0";
        private UserInfo UserInfo;

        [SetUp]
        public void Setup()
        {
            this.UserInfo = new UserInfo(ClientId, SessionId);
        }

        [Test]
        public void SetClientIdAndSessionIdOnInstantiation()
        {
            Assert.AreEqual(ClientId, this.UserInfo.GetClientId(), "ClientId should be equal");
            Assert.AreEqual(SessionId, this.UserInfo.GetSessionId(), "SessionId should be equal");
        }

        [Test]
        public void SetClientIdAndSessionIdAfterInstantiation()
        {
            string diffClientId = "diffClientId";
            int diffSessionId = 1;
            this.UserInfo.SetClientId(diffClientId);
            this.UserInfo.SetSessionId(diffSessionId);

            Assert.AreEqual(diffClientId, this.UserInfo.GetClientId(), "ClientId should be equal");
            Assert.AreEqual(diffSessionId, this.UserInfo.GetSessionId(), "SessionId should be equal");
        }

        [Test]
        public void SetClientIdWithInvalidClientId()
        {
            Assert.Throws<ArgumentException>(() => this.UserInfo.SetClientId(null));
        }

        [Test]
        public void SetSessionIdWithInvalidSessionId()
        {
            Assert.Throws<ArgumentException>(() => this.UserInfo.SetSessionId(0));
        }

        [Test]
        public void SetUserIdWithInvalidUserId()
        {
            Assert.Throws<ArgumentException>(() => this.UserInfo.SetUserId(null));
        }

        [Test]
        public void SetUserSegmentsWithInvalidUserSegments()
        {
            Assert.Throws<ArgumentException>(() => this.UserInfo.SetUserSegments(null));
        }

        [Test]
        public void SetForwardedForWithInvalidForwardedFor()
        {
            Assert.Throws<ArgumentException>(() => this.UserInfo.SetForwardedFor(null));
        }

        [Test]
        public void SetUserAgentWithInvalidUserAgent()
        {
            Assert.Throws<ArgumentException>(() => this.UserInfo.SetUserAgent(null));
        }
    }
}
