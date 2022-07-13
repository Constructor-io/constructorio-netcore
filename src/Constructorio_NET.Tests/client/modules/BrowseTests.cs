using System;
using System.Collections.Generic;
using System.IO;
using Constructorio_NET.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class BrowseTest
    {
        private readonly string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private readonly string FilterName = "Color";
        private readonly string FilterValue = "Blue";
        private readonly string CollectionId = "test";
        private readonly List<string> ItemIds = new List<string>() { "10001", "10002" };
        private ConstructorioConfig Config;
        private UserInfo UserInfo;

        [OneTimeSetUp]
        public void Setup()
        {
            JObject json = JObject.Parse(File.ReadAllText("./../../../../../.config/local.json"));
            string testApiToken = json.SelectToken("TEST_API_TOKEN").Value<string>();

            this.Config = new ConstructorioConfig(this.ApiKey);
            this.Config.ApiToken = testApiToken;
            this.UserInfo = new UserInfo(ClientId, SessionId);
        }

        [Test]
        public void GetBrowseResults()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue);
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = constructorio.Browse.GetBrowseResults(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseResultsWithFilters()
        {
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>()
            {
                { "Brand", new List<string>() { "XYZ" } }
            };
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue);
            req.UserInfo = this.UserInfo;
            req.Filters = filters;
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = constructorio.Browse.GetBrowseResults(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseResultsWithResultParams()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue);
            req.UserInfo = this.UserInfo;
            req.ResultsPerPage = 1;
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = constructorio.Browse.GetBrowseResults(req);
            Assert.AreEqual(1, (long)res.Request["num_results_per_page"], "Expect request to include page parameter");
            Assert.AreEqual(1, res.Response.TotalNumResults, "total number of results expected to be 1");
            Assert.AreEqual(1, res.Response.Results.Count, "length of results expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseResultsWithCollection()
        {
            BrowseRequest req = new BrowseRequest("collection_id", this.CollectionId);
            req.UserInfo = this.UserInfo;
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = constructorio.Browse.GetBrowseResults(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.AreEqual(res.Response.Collection.DisplayName, this.CollectionId, "display name should match");
            Assert.AreEqual(res.Response.Collection.Id, this.CollectionId, "id should match");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseItemsResults()
        {
            BrowseItemsRequest req = new BrowseItemsRequest(this.ItemIds);
            req.UserInfo = this.UserInfo;
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = constructorio.Browse.GetBrowseItemsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseItemsResultsWithFilters()
        {
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>()
            {
                { "Brand", new List<string>() { "XYZ" } }
            };
            BrowseItemsRequest req = new BrowseItemsRequest(this.ItemIds);
            req.UserInfo = this.UserInfo;
            req.Filters = filters;
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = constructorio.Browse.GetBrowseItemsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseItemsResultsWithResultParams()
        {
            BrowseItemsRequest req = new BrowseItemsRequest(this.ItemIds);
            req.UserInfo = this.UserInfo;
            req.ResultsPerPage = 1;
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = constructorio.Browse.GetBrowseItemsResult(req);
            Assert.AreEqual(1, (long)res.Request["num_results_per_page"], "Expect request to include page parameter");
            Assert.AreEqual(2, res.Response.TotalNumResults, "total number of results expected to be 2");
            Assert.AreEqual(1, res.Response.Results.Count, "length of results expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseFacetsResults()
        {
            BrowseFacetsRequest req = new BrowseFacetsRequest();
            req.UserInfo = this.UserInfo;
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseFacetsResponse res = constructorio.Browse.GetBrowseFacetsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseFacetsWithResultParams()
        {
            BrowseFacetsRequest req = new BrowseFacetsRequest();
            req.UserInfo = this.UserInfo;
            req.ResultsPerPage = 1;
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseFacetsResponse res = constructorio.Browse.GetBrowseFacetsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.AreEqual(1, res.Response.Facets.Count, "length of facets expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseFacetsWithFmtOptionParams()
        {
            BrowseFacetsRequest req = new BrowseFacetsRequest();
            req.UserInfo = this.UserInfo;
            req.ShowHiddenFacets = true;
            req.ShowProtectedFacets = true;
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseFacetsResponse res = constructorio.Browse.GetBrowseFacetsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseFacetOptionsResults()
        {
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest(this.FilterName);
            req.UserInfo = this.UserInfo;
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseFacetOptionsResponse res = constructorio.Browse.GetBrowseFacetOptionsResult(req);
            Assert.AreEqual(1, res.Response.Facets.Count, "length of facets expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseFacetOptionsWithFmtOptionParams()
        {
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest(this.FilterName);
            req.UserInfo = this.UserInfo;
            req.ShowHiddenFacets = true;
            req.ShowProtectedFacets = true;
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseFacetOptionsResponse res = constructorio.Browse.GetBrowseFacetOptionsResult(req);
            Assert.GreaterOrEqual(res.Response.Facets.Count, 1, "length of facets expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }
    }
}