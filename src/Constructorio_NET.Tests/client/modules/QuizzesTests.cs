using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class QuizzesTest
    {
        private readonly string ApiKey = "key_vM4GkLckwiuxwyRA";
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private readonly string Id = "test-quiz";
        private readonly List<List<string>> Answers = new List<List<string>>()
        {
            new List<string>() { "1" },
            new List<string>() { "1", "2" },
            new List<string>() { "seen" },
        };
        private readonly string VersionId = "e03210db-0cc6-459c-8f17-bf014c4f554d";
        private ConstructorioConfig Config;
        private UserInfo UserInfo;

        [OneTimeSetUp]
        public void Setup()
        {
            JObject json = JObject.Parse(File.ReadAllText("./../../../../../.config/local.json"));
            string testApiToken = json.SelectToken("TEST_API_TOKEN").Value<string>();

            this.Config = new ConstructorioConfig(this.ApiKey)
            {
                ApiToken = testApiToken
            };
            this.UserInfo = new UserInfo(ClientId, SessionId);
        }

        [Test]
        public void GetQuizNextQuestionWithInvalidApiKeyShouldError()
        {
            QuizRequest req = new QuizRequest(this.Id);
            ConstructorIO constructorio = new ConstructorIO(new ConstructorioConfig("invalidKey"));
            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Quizzes.GetNextQuestion(req));
            Assert.IsTrue(ex.Message == "Http[404]: The quiz you requested, \"test-quiz\" was not found, please specify a valid quiz id before trying again.", "Correct Error is Returned");
        }

        [Test]
        public void GetQuizResultsWithInvalidApiKeyShouldError()
        {
            QuizRequest req = new QuizRequest(this.Id);
            ConstructorIO constructorio = new ConstructorIO(new ConstructorioConfig("invalidKey"));
            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Quizzes.GetResults(req));
            Assert.IsTrue(ex.Message == "Http[404]: The quiz you requested, \"test-quiz\" was not found, please specify a valid quiz id before trying again.", "Correct Error is Returned");
        }

        [Test]
        public async Task GetQuizNextQuestion()
        {
            QuizRequest req = new QuizRequest(this.Id)
            {
                Answers = this.Answers,
                VersionId = this.VersionId
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            NextQuestionResponse res = await constructorio.Quizzes.GetNextQuestion(req);
            Assert.IsNotNull(res.NextQuestion, "NextQuestion should exist");
            Assert.IsNotNull(res.IsLastQuestion, "IsLastQuestion should exist");
            Assert.IsNotNull(res.VersionId, "VersionId should exist");
        }

        [Test]
        public async Task GetQuizResults()
        {
            QuizRequest req = new QuizRequest(this.Id)
            {
                Answers = this.Answers,
                VersionId = this.VersionId
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            QuizResultsResponse res = await constructorio.Quizzes.GetResults(req);
            Assert.IsNotNull(res.Result, "Result should exist");
            Assert.IsNotNull(res.ResultsUrl, "ResultsUrl should exist");
            Assert.IsNotNull(res.VersionId, "VersionId should exist");
        }
    }
}