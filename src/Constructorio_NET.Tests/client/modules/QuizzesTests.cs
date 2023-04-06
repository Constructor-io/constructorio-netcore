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
        private readonly string QuizId = "test-quiz";
        private readonly List<List<string>> Answers = new List<List<string>>()
        {
            new List<string>() { "1" },
            new List<string>() { "1", "2" },
            new List<string>() { "seen" },
        };
        private readonly string QuizVersionId = "e03210db-0cc6-459c-8f17-bf014c4f554d";
        private readonly string QuizSessionId = "ca380401-3805-4ded-8f28-638e5a4baa92";
        private ConstructorioConfig Config;
        private UserInfo UserInfo;

        [OneTimeSetUp]
        public void Setup()
        {
            this.Config = new ConstructorioConfig(this.ApiKey);
            this.UserInfo = new UserInfo(ClientId, SessionId);
        }

        [Test]
        public void GetQuizNextQuestionWithInvalidApiKeyShouldError()
        {
            QuizRequest req = new QuizRequest(this.QuizId);
            ConstructorIO constructorio = new ConstructorIO(new ConstructorioConfig("invalidKey"));
            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Quizzes.GetNextQuestion(req));
            Assert.IsTrue(ex.Message == "Http[404]: The quiz you requested, \"test-quiz\" was not found, please specify a valid quiz id before trying again.", "Correct Error is Returned");
        }

        [Test]
        public void GetQuizResultsWithInvalidApiKeyShouldError()
        {
            QuizRequest req = new QuizRequest(this.QuizId);
            ConstructorIO constructorio = new ConstructorIO(new ConstructorioConfig("invalidKey"));
            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Quizzes.GetResults(req));
            Assert.IsTrue(ex.Message == "Http[404]: The quiz you requested, \"test-quiz\" was not found, please specify a valid quiz id before trying again.", "Correct Error is Returned");
        }

        [Test]
        public async Task GetQuizNextQuestion()
        {
            QuizRequest req = new QuizRequest(this.QuizId)
            {
                Answers = this.Answers,
                QuizVersionId = this.QuizVersionId
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            NextQuestionResponse res = await constructorio.Quizzes.GetNextQuestion(req);
            Assert.IsNotNull(res.NextQuestion, "NextQuestion should exist");
            Assert.IsNotNull(res.NextQuestion.Id, "NextQuestion Id should exist");
            Assert.IsNotNull(res.NextQuestion.Title, "NextQuestion Title should exist");
            Assert.IsNotNull(res.NextQuestion.Type, "NextQuestion Type should exist");
            Assert.IsNotNull(res.NextQuestion.Description, "NextQuestion Description should exist");
            Assert.IsNotNull(res.NextQuestion.Images, "NextQuestion Images should exist");
            Assert.IsNotNull(res.IsLastQuestion, "IsLastQuestion should exist");
            Assert.IsNotNull(res.QuizVersionId, "QuizVersionId should exist");
            Assert.IsNotNull(res.QuizSessionId, "QuizSessionId should exist");
            Assert.IsNotNull(res.QuizId, "QuizId should exist");
        }

        [Test]
        public async Task GetQuizNextQuestionShouldMatchPassedIds()
        {
            QuizRequest req = new QuizRequest(this.QuizId)
            {
                Answers = this.Answers,
                QuizVersionId = this.QuizVersionId,
                QuizSessionId = this.QuizSessionId
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            NextQuestionResponse res = await constructorio.Quizzes.GetNextQuestion(req);
            Assert.AreEqual(this.QuizVersionId, res.QuizVersionId, "QuizVersionId should match the one passed in the request");
            Assert.AreEqual(this.QuizSessionId, res.QuizSessionId, "QuizSessionId should match the one passed in the request");
            Assert.AreEqual(this.QuizId, res.QuizId, "QuizId should match the one passed in the request");
        }

        [Test]
        public async Task GetQuizResults()
        {
            QuizRequest req = new QuizRequest(this.QuizId)
            {
                Answers = this.Answers,
                QuizVersionId = this.QuizVersionId
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);   
            QuizResultsResponse res = await constructorio.Quizzes.GetResults(req);
            Assert.IsNotNull(res.Response, "Response should exist");
            Assert.IsNotNull(res.Response.RefinedContent, "RefinedContent should exist");
            Assert.IsNotNull(res.Response.Results, "Results should exist");
            Assert.IsNotNull(res.Response.Facets, "Facets should exist");
            Assert.IsNotNull(res.Response.Groups, "Groups should exist");
            Assert.IsNotNull(res.Response.TotalNumResults, "TotalNumResults should exist");
            Assert.IsNotNull(res.Response.SortOptions, "SortOptions should exist");
            Assert.IsNotNull(res.QuizSessionId, "QuizSessionId should exist");
            Assert.IsNotNull(res.QuizVersionId, "QuizVersionId should exist");
            Assert.IsNotNull(res.QuizId, "QuizId should exist");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetQuizResultsShouldMatchPassedIds()
        {
            QuizRequest req = new QuizRequest(this.QuizId)
            {
                Answers = this.Answers,
                QuizVersionId = this.QuizVersionId,
                QuizSessionId = this.QuizSessionId
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            QuizResultsResponse res = await constructorio.Quizzes.GetResults(req);
            Assert.AreEqual(this.QuizVersionId, res.QuizVersionId, "QuizVersionId should match the one passed in the request");
            Assert.AreEqual(this.QuizSessionId, res.QuizSessionId, "QuizSessionId should match the one passed in the request");
            Assert.AreEqual(this.QuizId, res.QuizId, "QuizId should match the one passed in the request");
        }
    }
}