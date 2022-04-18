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
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            SearchResponse res = constructorio.Search.GetSearchResults(this.Query, new Hashtable(), this.UserParameters);
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

            ConstructorIO constructorio = new ConstructorIO(this.Options);
            SearchResponse res = constructorio.Search.GetSearchResults(this.Query, parameters, this.UserParameters);
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
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            SearchResponse res = constructorio.Search.GetSearchResults(this.Query, parameters, this.UserParameters);
            Assert.AreEqual((Int64)res.Request["page"], 3, "total number of results expected to be 1");
            Assert.Greater(res.Response.TotalNumResults, 1, "total number of results expected to be 1");
            Assert.AreEqual(res.Response.Results.Count, 1, "length of results expected to be equal to 1");
        }

        [Test]
        public void GetSearchResultsFromRequest()
        {
            Hashtable parameters = new Hashtable()
            {
               { "page", 3 },
               { "resultsPerPage", 1 }
            };
            SearchRequest request = new SearchRequest("thing");
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            constructorio.Search.GetSearchResultsFromRequest(request);
        }
    }
}