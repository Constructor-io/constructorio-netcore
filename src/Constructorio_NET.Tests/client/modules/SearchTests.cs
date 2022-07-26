using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class SearchTest
    {
        private readonly string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private readonly string Query = "item";
        private ConstructorioConfig Config;
        private UserInfo UserInfo;

        [SetUp]
        public void Setup()
        {
            this.Config = new ConstructorioConfig(this.ApiKey);
            this.UserInfo = new UserInfo(ClientId, SessionId);
        }

        [Test]
        public void GetSearchResultsWithInvalidApiKeyShouldError()
        {
            SearchRequest req = new SearchRequest(this.Query)
            {
                UserInfo = this.UserInfo
            };
            ConstructorIO constructorio = new ConstructorIO(new ConstructorioConfig("invalidKey"));
            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Search.GetSearchResults(req));
            Assert.IsTrue(ex.Message == "Http[400]: We have no record of this key. You can find your key at app.constructor.io/dashboard.", "Correct Error is Returned");
        }

        [Test]
        public async Task GetSearchResults()
        {
            SearchRequest req = new SearchRequest(this.Query)
            {
                UserInfo = this.UserInfo
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
    }

        [Test]
        public async Task GetSearchResultsWithFilters()
        {
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>()
            {
                { "Color", new List<string>() { "green", "blue" } }
            };
            SearchRequest req = new SearchRequest(this.Query)
            {
                UserInfo = this.UserInfo,
                Filters = filters
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsWithResultParams()
        {
            SearchRequest req = new SearchRequest(this.Query)
            {
                UserInfo = this.UserInfo,
                Page = 3,
                ResultsPerPage = 1
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            Assert.AreEqual(3, (long)res.Request["page"], "total number of results expected to be 1");
            Assert.Greater(res.Response.TotalNumResults, 1, "total number of results expected to be 1");
            Assert.AreEqual(1, res.Response.Results.Count, "length of results expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsWithRedirect()
        {
            SearchRequest req = new SearchRequest("constructor")
            {
                UserInfo = this.UserInfo
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            Assert.IsNotNull(res.Response.Redirect, "Redirect should exist");
            Assert.IsNotNull(res.Response.Redirect.Data.Url, "Url should exist");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }
    }
}