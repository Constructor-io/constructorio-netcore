using NUnit.Framework;
using System;
using System.Collections;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class SearchTest
    {
        private string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private string Query = "item";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetSearchResults()
        {
            Hashtable parameters = new Hashtable()
            {
               { "apiKey", this.ApiKey }
            };
            ConstructorIO constructorio = new ConstructorIO(parameters);
            SearchResponse res = constructorio.Search.GetSearchResults(this.Query, new Hashtable(), new Hashtable());
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
        }

        [Test]
        public void GetSearchResultsWithFilters()
        {
            Hashtable parameters = new Hashtable()
            {
               { "apiKey", this.ApiKey }
            };
            ConstructorIO constructorio = new ConstructorIO(parameters);
            SearchResponse res = constructorio.Search.GetSearchResults(this.Query, new Hashtable(), new Hashtable());
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
        }
    }
}