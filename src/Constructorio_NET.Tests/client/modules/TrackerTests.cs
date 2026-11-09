using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Constructorio_NET.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class TrackerTests
    {
        private readonly string ApiKey = "key_vM4GkLckwiuxwyRA";
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private readonly string Term = "item";
        private UserInfo UserInfo;

        private sealed class CapturingHandler : HttpMessageHandler
        {
            public HttpRequestMessage Request { get; private set; }
            public string Body { get; private set; }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                this.Request = request;
                this.Body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                return new HttpResponseMessage(HttpStatusCode.NoContent) { Content = new StringContent(string.Empty) };
            }
        }

        private static (ConstructorIO Client, CapturingHandler Handler) CreateClient(string apiKey)
        {
            CapturingHandler handler = new CapturingHandler();
            ConstructorioConfig config = new ConstructorioConfig(apiKey) { HttpClient = new HttpClient(handler) };
            return (new ConstructorIO(config), handler);
        }

        [SetUp]
        public void Setup()
        {
            this.UserInfo = new UserInfo(ClientId, SessionId);
        }

        [Test]
        public async Task TrackSearchResultsLoadedShouldSendUserParamsInQuery()
        {
            this.UserInfo.SetUserId("user-1");
            this.UserInfo.SetUserSegments(new List<string> { "us", "desktop" });
            this.UserInfo.SetForwardedFor("10.0.0.1");
            this.UserInfo.SetUserAgent("test-agent");
            SearchResultsLoadedRequest req = new SearchResultsLoadedRequest(this.Term, 10) { UserInfo = this.UserInfo };
            var (constructorio, handler) = CreateClient(this.ApiKey);

            bool res = await constructorio.Tracker.TrackSearchResultsLoaded(req);

            Assert.IsTrue(res);
            Assert.AreEqual(HttpMethod.Post, handler.Request.Method);
            Assert.AreEqual("/v2/behavioral_action/search_result_load", handler.Request.RequestUri.AbsolutePath);
            var query = HttpUtility.ParseQueryString(handler.Request.RequestUri.Query);
            Assert.AreEqual(this.ApiKey, query["key"]);
            Assert.That(query["c"], Does.StartWith("cionet-"));
            Assert.AreEqual(this.ClientId, query["i"]);
            Assert.AreEqual(this.SessionId.ToString(), query["s"]);
            Assert.AreEqual("user-1", query["ui"]);
            Assert.That(query.GetValues("us"), Is.EquivalentTo(new[] { "us", "desktop" }));
            Assert.AreEqual("true", query["beacon"]);
            Assert.IsNotNull(query["_dt"]);
            Assert.AreEqual("10.0.0.1", handler.Request.Headers.GetValues("X-Forwarded-For").Single());
            Assert.AreEqual("test-agent", string.Join(" ", handler.Request.Headers.GetValues("User-Agent")));
        }

        [Test]
        public async Task TrackSearchResultsLoadedShouldSendBodyWithResultId()
        {
            SearchResultsLoadedRequest req = new SearchResultsLoadedRequest(this.Term, 2)
            {
                UserInfo = this.UserInfo,
                ResultId = "result-id-123",
                Items = new List<TrackingItem> { new TrackingItem("item-1"), new TrackingItem("item-2", "variation-2") },
                ResultPage = 3,
                Section = "Products",
            };
            var (constructorio, handler) = CreateClient(this.ApiKey);

            await constructorio.Tracker.TrackSearchResultsLoaded(req);

            JObject body = JObject.Parse(handler.Body);
            Assert.AreEqual(this.Term, (string)body["search_term"]);
            Assert.AreEqual(2, (int)body["result_count"]);
            Assert.AreEqual("result-id-123", (string)body["result_id"]);
            Assert.AreEqual(3, (int)body["result_page"]);
            Assert.AreEqual("Products", (string)body["section"]);
            Assert.AreEqual("N/A", (string)body["url"]);
            Assert.AreEqual("item-1", (string)body["items"][0]["item_id"]);
            Assert.IsNull(body["items"][0]["variation_id"]);
            Assert.AreEqual("item-2", (string)body["items"][1]["item_id"]);
            Assert.AreEqual("variation-2", (string)body["items"][1]["variation_id"]);
            Assert.IsNull(body["i"], "user params belong in the query string, not the body");
        }

        [Test]
        public async Task TrackSearchResultsLoadedShouldOmitUnsetFieldsAndCapItems()
        {
            SearchResultsLoadedRequest req = new SearchResultsLoadedRequest(this.Term, 500)
            {
                UserInfo = this.UserInfo,
                Items = Enumerable.Range(0, 150).Select(i => new TrackingItem($"item-{i}")).ToList(),
            };
            var (constructorio, handler) = CreateClient(this.ApiKey);

            await constructorio.Tracker.TrackSearchResultsLoaded(req);

            JObject body = JObject.Parse(handler.Body);
            Assert.AreEqual(100, ((JArray)body["items"]).Count);
            Assert.IsNull(body["result_id"]);
            Assert.IsNull(body["result_page"]);
            Assert.IsNull(body["section"]);
        }

        [Test]
        public void TrackSearchResultsLoadedWithoutUserInfoShouldError()
        {
            SearchResultsLoadedRequest req = new SearchResultsLoadedRequest(this.Term, 10);
            var (constructorio, handler) = CreateClient(this.ApiKey);

            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Tracker.TrackSearchResultsLoaded(req));

            Assert.AreEqual("UserInfo with a client id and session id is required", ex.Message);
            Assert.IsNull(handler.Request, "no request should be sent");
        }

        [Test]
        public void TrackSearchResultsLoadedWithoutSessionIdShouldError()
        {
            UserInfo userInfo = new UserInfo();
            userInfo.SetClientId(this.ClientId);
            SearchResultsLoadedRequest req = new SearchResultsLoadedRequest(this.Term, 10) { UserInfo = userInfo };
            var (constructorio, _) = CreateClient(this.ApiKey);

            Assert.ThrowsAsync<ConstructorException>(() => constructorio.Tracker.TrackSearchResultsLoaded(req));
        }

        [Test]
        public void SearchResultsLoadedRequestWithoutTermShouldError()
        {
            Assert.Throws<ArgumentException>(() => new SearchResultsLoadedRequest(null, 10));
        }

        [TestCase(null)]
        [TestCase("")]
        public void TrackSearchResultsLoadedWithTermClearedAfterConstructionShouldError(string term)
        {
            SearchResultsLoadedRequest req = new SearchResultsLoadedRequest(this.Term, 10) { UserInfo = this.UserInfo, Term = term };
            var (constructorio, handler) = CreateClient(this.ApiKey);

            Assert.ThrowsAsync<ArgumentException>(() => constructorio.Tracker.TrackSearchResultsLoaded(req));
            Assert.IsNull(handler.Request, "no request should be sent");
        }

        [Test]
        public async Task TrackSearchResultsLoadedShouldOmitNullUrl()
        {
            SearchResultsLoadedRequest req = new SearchResultsLoadedRequest(this.Term, 10) { UserInfo = this.UserInfo, Url = null };
            var (constructorio, handler) = CreateClient(this.ApiKey);

            await constructorio.Tracker.TrackSearchResultsLoaded(req);

            JObject body = JObject.Parse(handler.Body);
            Assert.IsFalse(body.ContainsKey("url"), "a null url must be omitted, not sent as null");
        }

        [Test]
        public async Task TrackSearchResultsLoadedShouldBeAcceptedByApi()
        {
            SearchResultsLoadedRequest req = new SearchResultsLoadedRequest(this.Term, 1)
            {
                UserInfo = this.UserInfo,
                ResultId = Guid.NewGuid().ToString(),
                Items = new List<TrackingItem> { new TrackingItem("item-1") },
            };
            ConstructorIO constructorio = new ConstructorIO(new ConstructorioConfig(this.ApiKey));

            bool res = await constructorio.Tracker.TrackSearchResultsLoaded(req);

            Assert.IsTrue(res);
        }
    }
}
