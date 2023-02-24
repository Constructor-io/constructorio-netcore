using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class QuizRequestTest
    {
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private readonly string UserId = "user1";
        private readonly string Section = "Products";
        private readonly string Id = "1";
        private readonly List<List<string>> Answers = new List<List<string>>()
        {
            new List<string>() { "1", "2" }
        };
        private readonly string VersionId = "1234";
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
            QuizRequest req = new QuizRequest(this.Id)
            {
                UserInfo = this.UserInfo,
                Section = this.Section,
                Answers = this.Answers,
                VersionId = this.VersionId,
                TestCells = this.TestCells,
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.ClientId, requestParameters[Constants.CLIENT_ID]);
            Assert.AreEqual(this.SessionId, requestParameters[Constants.SESSION_ID]);
            Assert.AreEqual(this.UserId, requestParameters[Constants.USER_ID]);
            Assert.AreEqual(this.UserSegments, requestParameters[Constants.USER_SEGMENTS]);
            Assert.AreEqual(this.Section, requestParameters[Constants.SECTION]);
            Assert.AreEqual(this.Answers, requestParameters[Constants.ANSWERS]);
            Assert.AreEqual(this.VersionId, requestParameters[Constants.VERSION_ID]);
            Assert.AreEqual(this.TestCells, requestParameters[Constants.TEST_CELLS]);
        }

        [Test]
        public void GetRequestHeaders()
        {
            QuizRequest req = new QuizRequest(this.Id)
            {
                UserInfo = this.UserInfo,
            };

            Dictionary<string, string> requestHeaders = req.GetRequestHeaders();
            Assert.AreEqual(this.OS, requestHeaders[Constants.USER_AGENT]);
            Assert.AreEqual(this.IP, requestHeaders[Constants.USER_IP]);
        }

        [Test]
        public void QuizRequestWithInvalidId()
        {
            Assert.Throws<ArgumentException>(() => new QuizRequest(null));
        }
    }
}
