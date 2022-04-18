using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class SearchTest
    {
        private string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private string ClientId = "r4nd-cl1ent-1d";
        private Hashtable Options = new Hashtable();
        private string Query = "item";
        private int SessionId = 4;
        private Hashtable UserParameters = new Hashtable();

        [SetUp]
        public void Setup()
        {
            this.Options = new Hashtable()
            {
               { "apiKey", this.ApiKey }
            };
            this.UserParameters = new Hashtable()
            {
                { "clientId", ClientId },
                { "sessionId", SessionId }
            };
        }

        [Test]
        public void GetSearchResults()
        {
            SearchRequest req = new SearchRequest(this.Query);
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            SearchResponse res = constructorio.Search.GetSearchResults(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
        }

        [Test]
        public void GetSearchResultsWithFilters()
        {
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>()
            {
                { "Color", new List<string>() { "green", "blue" } }
            };
            Hashtable parameters = new Hashtable()
            {
               { "filters", filters } 
            };

            SearchRequest req = new SearchRequest(this.Query);
            req.Filters = filters;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            SearchResponse res = constructorio.Search.GetSearchResults(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
        }

        [Test]
        public void GetSearchResultsWithResultParams()
        {
            Hashtable parameters = new Hashtable()
            {
               { "page", 3 },
               { "resultsPerPage", 1 }
            };
            SearchRequest req = new SearchRequest(this.Query);
            req.Page = 3;
            req.ResultsPerPage = 1;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            SearchResponse res = constructorio.Search.GetSearchResults(req);
            Assert.AreEqual(3, (Int64)res.Request["page"], "total number of results expected to be 1");
            Assert.Greater(res.Response.TotalNumResults, 1, "total number of results expected to be 1");
            Assert.AreEqual(1, res.Response.Results.Count, "length of results expected to be equal to 1");
        }
    }
}