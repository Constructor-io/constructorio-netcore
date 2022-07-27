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
        private Hashtable Options;

        [SetUp]
        public void Setup()
        {
            this.Options = new Hashtable()
            {
                { Constants.SERVICE_URL, this.ServiceUrl },
                { Constants.API_KEY, this.ApiKey },
                { Constants.VERSION, this.Version }
            };
        }

        [Test]
        public void SetClientIdAndSessionIdOnInstantiation()
        {
            UserInfo userInfo = new UserInfo(ClientId, SessionId);

            Assert.AreEqual(ClientId, userInfo.GetClientId(), "ClientId should be equal");
            Assert.AreEqual(SessionId, userInfo.GetSessionId(), "SessionId should be equal");
        }

        [Test]
        public void SetClientIdAndSessionIdAfterInstantiation()
        {
            UserInfo userInfo = new UserInfo(ClientId, SessionId);
            string diffClientId = "diffClientId";
            int diffSessionId = 1;
            userInfo.SetClientId(diffClientId);
            userInfo.SetSessionId(diffSessionId);

            Assert.AreEqual(diffClientId, userInfo.GetClientId(), "ClientId should be equal");
            Assert.AreEqual(diffSessionId, userInfo.GetSessionId(), "SessionId should be equal");
        }

        [Test]
        public void SetClientIdWithInvalidClientId()
        {
            UserInfo userInfo = new UserInfo(ClientId, SessionId);
            Assert.Throws<ArgumentException>(() => userInfo.SetClientId(null));
        }

        [Test]
        public void SetClientIdWithInvalidSessionId()
        {
            UserInfo userInfo = new UserInfo(ClientId, SessionId);
            Assert.Throws<ArgumentException>(() => userInfo.SetSessionId(0));
        }
    }
}
