using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Constructorio_NET.Models.Browse;
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

            this.Config = new ConstructorioConfig(this.ApiKey)
            {
                ApiToken = testApiToken
            };
            this.UserInfo = new UserInfo(ClientId, SessionId);
        }

        [Test]
        public void GetBrowseResultsWithInvalidApiKeyShouldError()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue);
            ConstructorIO constructorio = new ConstructorIO(new ConstructorioConfig("invalidKey"));
            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Browse.GetBrowseResults(req));
            Assert.IsTrue(ex.Message == "Http[400]: We have no record of this key. You can find your key at app.constructor.io/dashboard.", "Correct Error is Returned");
        }

        [Test]
        public async Task GetBrowseResults()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue);
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetBrowseResultsWithFilters()
        {
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>()
            {
                { "Brand", new List<string>() { "XYZ" } }
            };
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue)
            {
                UserInfo = this.UserInfo,
                Filters = filters
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetBrowseResultsWithResultParams()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue)
            {
                UserInfo = this.UserInfo,
                ResultsPerPage = 1
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            Assert.AreEqual(1, (long)res.Request["num_results_per_page"], "Expect request to include page parameter");
            Assert.AreEqual(1, res.Response.TotalNumResults, "total number of results expected to be 1");
            Assert.AreEqual(1, res.Response.Results.Count, "length of results expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetBrowseResultsWithCollection()
        {
            BrowseRequest req = new BrowseRequest("collection_id", this.CollectionId)
            {
                UserInfo = this.UserInfo
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.AreEqual(res.Response.Collection.DisplayName, this.CollectionId, "display name should match");
            Assert.AreEqual(res.Response.Collection.Id, this.CollectionId, "id should match");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetBrowseItemsResults()
        {
            BrowseItemsRequest req = new BrowseItemsRequest(this.ItemIds)
            {
                UserInfo = this.UserInfo
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseItemsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetBrowseItemsResultsWithFilters()
        {
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>()
            {
                { "Brand", new List<string>() { "XYZ" } }
            };
            BrowseItemsRequest req = new BrowseItemsRequest(this.ItemIds)
            {
                UserInfo = this.UserInfo,
                Filters = filters
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseItemsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetBrowseItemsResultsWithResultParams()
        {
            BrowseItemsRequest req = new BrowseItemsRequest(this.ItemIds)
            {
                UserInfo = this.UserInfo,
                ResultsPerPage = 1
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseItemsResult(req);
            Assert.AreEqual(1, (long)res.Request["num_results_per_page"], "Expect request to include page parameter");
            Assert.AreEqual(2, res.Response.TotalNumResults, "total number of results expected to be 2");
            Assert.AreEqual(1, res.Response.Results.Count, "length of results expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetBrowseFacetsResults()
        {
            BrowseFacetsRequest req = new BrowseFacetsRequest
            {
                UserInfo = this.UserInfo
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseFacetsResponse res = await constructorio.Browse.GetBrowseFacetsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetBrowseFacetsWithResultParams()
        {
            BrowseFacetsRequest req = new BrowseFacetsRequest
            {
                UserInfo = this.UserInfo,
                ResultsPerPage = 1
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseFacetsResponse res = await constructorio.Browse.GetBrowseFacetsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.AreEqual(1, res.Response.Facets.Count, "length of facets expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetBrowseFacetsWithFmtOptionParams()
        {
            BrowseFacetsRequest req = new BrowseFacetsRequest
            {
                UserInfo = this.UserInfo,
                ShowHiddenFacets = true,
                ShowProtectedFacets = true
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseFacetsResponse res = await constructorio.Browse.GetBrowseFacetsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetBrowseFacetOptionsResults()
        {
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest(this.FilterName)
            {
                UserInfo = this.UserInfo
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseFacetOptionsResponse res = await constructorio.Browse.GetBrowseFacetOptionsResult(req);
            Assert.AreEqual(1, res.Response.Facets.Count, "length of facets expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetBrowseFacetOptionsWithFmtOptionParams()
        {
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest(this.FilterName)
            {
                UserInfo = this.UserInfo,
                ShowHiddenFacets = true,
                ShowProtectedFacets = true
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseFacetOptionsResponse res = await constructorio.Browse.GetBrowseFacetOptionsResult(req);
            Assert.GreaterOrEqual(res.Response.Facets.Count, 1, "length of facets expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }
    }
}