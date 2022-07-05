using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class BrowseTest
    {
        private string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private string ClientId = "r4nd-cl1ent-1d";
        private Hashtable Options = new Hashtable();
        private int SessionId = 4;
        private Hashtable UserParameters = new Hashtable();
        private UserInfo UserInfo = null;
        private string FilterName = "Color";
        private string FilterValue = "Blue";
        private string CollectionId = "test";
        private List<string> ItemIds = new List<string>() { "10001", "10002" };

        [OneTimeSetUp]
        public void Setup()
        {
            JObject json = JObject.Parse(File.ReadAllText("./../../../../../.config/local.json"));
            string testApiToken = json.SelectToken("TEST_API_TOKEN").Value<string>();

            this.Options = new Hashtable()
            {
               { Constants.API_KEY, this.ApiKey },
               { Constants.API_TOKEN, testApiToken }
            };
            this.UserParameters = new Hashtable()
            {
                { Constants.CLIENT_ID, ClientId },
                { Constants.SESSION_ID, SessionId }
            };
            this.UserInfo = new UserInfo(ClientId, SessionId);
        }

        [Test]
        public void GetBrowseResults()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue);
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseResponse res = constructorio.Browse.GetBrowseResults(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseResultsWithUserInfo()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue);
            req.UserInfo = this.UserInfo;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
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
            req.Filters = filters;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
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
            req.ResultsPerPage = 1;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseResponse res = constructorio.Browse.GetBrowseResults(req);
            Assert.AreEqual(1, (Int64)res.Request["num_results_per_page"], "Expect request to include page parameter");
            Assert.AreEqual(res.Response.TotalNumResults, 1, "total number of results expected to be 1");
            Assert.AreEqual(1, res.Response.Results.Count, "length of results expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseResultsWithCollection()
        {
            BrowseRequest req = new BrowseRequest("collection_id", this.CollectionId);
            ConstructorIO constructorio = new ConstructorIO(this.Options);
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
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseResponse res = constructorio.Browse.GetBrowseItemsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseItemsResultsWithUserInfo()
        {
            BrowseItemsRequest req = new BrowseItemsRequest(this.ItemIds);
            req.UserInfo = this.UserInfo;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
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
            req.Filters = filters;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
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
            req.ResultsPerPage = 1;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseResponse res = constructorio.Browse.GetBrowseItemsResult(req);
            Assert.AreEqual(1, (Int64)res.Request["num_results_per_page"], "Expect request to include page parameter");
            Assert.AreEqual(res.Response.TotalNumResults, 2, "total number of results expected to be 2");
            Assert.AreEqual(1, res.Response.Results.Count, "length of results expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseFacetsResults()
        {
            BrowseFacetsRequest req = new BrowseFacetsRequest();
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseFacetsResponse res = constructorio.Browse.GetBrowseFacetsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseFacetsWithResultParams()
        {
            BrowseFacetsRequest req = new BrowseFacetsRequest();
            req.ResultsPerPage = 1;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseFacetsResponse res = constructorio.Browse.GetBrowseFacetsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.AreEqual(res.Response.Facets.Count, 1, "length of facets expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseFacetsWithFmtOptionParams()
        {
            BrowseFacetsRequest req = new BrowseFacetsRequest();
            req.showHiddenFacets = true;
            req.showProtectedFacets = true;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseFacetsResponse res = constructorio.Browse.GetBrowseFacetsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseFacetOptionsResults()
        {
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest(this.FilterName);
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseFacetOptionsResponse res = constructorio.Browse.GetBrowseFacetOptionsResult(req);
            Assert.AreEqual(res.Response.Facets.Count, 1, "length of facets expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public void GetBrowseFacetOptionsWithFmtOptionParams()
        {
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest(this.FilterName);
            req.showHiddenFacets = true;
            req.showProtectedFacets = true;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseFacetOptionsResponse res = constructorio.Browse.GetBrowseFacetOptionsResult(req);
            Assert.GreaterOrEqual(res.Response.Facets.Count, 1, "length of facets expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }
    }
}