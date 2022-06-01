using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

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
        private UserInfo userInfo = null;
        private string filterName = "Color";
        private string filterValue = "Blue";

        [SetUp]
        public void Setup()
        {
            this.Options = new Hashtable()
            {
               { Constants.API_KEY, this.ApiKey }
            };
            this.UserParameters = new Hashtable()
            {
                { Constants.CLIENT_ID, ClientId },
                { Constants.SESSION_ID, SessionId }
            };
            this.userInfo = new UserInfo(ClientId, SessionId);
        }

        [Test]
        public void GetBrowseResults()
        {
            BrowseRequest req = new BrowseRequest(filterName, filterValue);
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseResponse res = constructorio.Browse.GetBrowseResults(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "result_id exists");
        }

        [Test]
        public void GetBrowseResultsWithUserInfo()
        {
            BrowseRequest req = new BrowseRequest(filterName, filterValue);
            req.UserInfo = this.userInfo;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseResponse res = constructorio.Browse.GetBrowseResults(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "result_id exists");
        }

        [Test]
        public void GetBrowseResultsWithFilters()
        {
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>()
            {
                { "Brand", new List<string>() { "XYZ" } }
            };
            BrowseRequest req = new BrowseRequest(filterName, filterValue);
            req.Filters = filters;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseResponse res = constructorio.Browse.GetBrowseResults(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "result_id exists");
        }

        [Test]
        public void GetBrowseResultsWithResultParams()
        {
            BrowseRequest req = new BrowseRequest(filterName, filterValue);
            req.ResultsPerPage = 1;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseResponse res = constructorio.Browse.GetBrowseResults(req);
            Assert.AreEqual(1, (Int64)res.Request["num_results_per_page"], "Expect request to include page parameter");
            Assert.AreEqual(res.Response.TotalNumResults, 1, "total number of results expected to be 1");
            Assert.AreEqual(1, res.Response.Results.Count, "length of results expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "result_id exists");
        }
    }

    [TestFixture]
    public class BrowseItemsTest
    {
        private string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private string ClientId = "r4nd-cl1ent-1d";
        private Hashtable Options = new Hashtable();
        private string Query = "item";
        private int SessionId = 4;
        private Hashtable UserParameters = new Hashtable();
        private UserInfo userInfo = null;
        private string filterName = "Color";
        private string filterValue = "Blue";
        private List<string> itemIds = new List<string>() { "10001", "10002" };

        [SetUp]
        public void Setup()
        {
            this.Options = new Hashtable()
            {
               { Constants.API_KEY, this.ApiKey }
            };
            this.UserParameters = new Hashtable()
            {
                { Constants.CLIENT_ID, ClientId },
                { Constants.SESSION_ID, SessionId }
            };
            this.userInfo = new UserInfo(ClientId, SessionId);
        }

        [Test]
        public void GetBrowseItemsResults()
        {
            BrowseItemsRequest req = new BrowseItemsRequest(itemIds);
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseResponse res = constructorio.Browse.GetBrowseItemsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "result_id exists");
        }

        [Test]
        public void GetBrowseItemsResultsWithUserInfo()
        {
            BrowseItemsRequest req = new BrowseItemsRequest(itemIds);
            req.UserInfo = this.userInfo;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseResponse res = constructorio.Browse.GetBrowseItemsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "result_id exists");
        }

        [Test]
        public void GetBrowseItemsResultsWithFilters()
        {
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>()
            {
                { "Brand", new List<string>() { "XYZ" } }
            };
            BrowseItemsRequest req = new BrowseItemsRequest(itemIds);
            req.Filters = filters;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseResponse res = constructorio.Browse.GetBrowseItemsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "result_id exists");
        }

        [Test]
        public void GetBrowseItemsResultsWithResultParams()
        {
            BrowseItemsRequest req = new BrowseItemsRequest(itemIds);
            req.ResultsPerPage = 1;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseResponse res = constructorio.Browse.GetBrowseItemsResult(req);
            Assert.AreEqual(1, (Int64)res.Request["num_results_per_page"], "Expect request to include page parameter");
            Assert.AreEqual(res.Response.TotalNumResults, 2, "total number of results expected to be 2");
            Assert.AreEqual(1, res.Response.Results.Count, "length of results expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "result_id exists");
        }
    }

    [TestFixture]
    public class BrowseFacetsTest
    {
        private string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private string ClientId = "r4nd-cl1ent-1d";
        private Hashtable Options = new Hashtable();
        private string Query = "item";
        private int SessionId = 4;
        private Hashtable UserParameters = new Hashtable();
        private string filterName = "Color";
        private string filterValue = "Blue";
        private List<string> itemIds = new List<string>() { "10001", "10002" };

        [SetUp]
        public void Setup()
        {
            this.Options = new Hashtable()
            {
               { Constants.API_KEY, this.ApiKey },
               { Constants.API_TOKEN, "tok_kKJsGqB1ARqjK4fv" },
            };
            this.UserParameters = new Hashtable()
            {
                { Constants.CLIENT_ID, ClientId },
                { Constants.SESSION_ID, SessionId }
            };
        }

        [Test]
        public void GetBrowseFacetsResults()
        {
            BrowseFacetsRequest req = new BrowseFacetsRequest();
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseFacetsResponse res = constructorio.Browse.GetBrowseFacetsResult(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.IsNotNull(res.ResultId, "result_id exists");
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
            Assert.IsNotNull(res.ResultId, "result_id exists");
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
            Assert.IsNotNull(res.ResultId, "result_id exists");
        }
    }

    [TestFixture]
    public class BrowseFacetOptionsTest
    {
        private string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private string ClientId = "r4nd-cl1ent-1d";
        private Hashtable Options = new Hashtable();
        private string Query = "item";
        private int SessionId = 4;
        private Hashtable UserParameters = new Hashtable();
        private string filterName = "Color";
        private List<string> itemIds = new List<string>() { "10001", "10002" };

        [SetUp]
        public void Setup()
        {
            this.Options = new Hashtable()
            {
               { Constants.API_KEY, this.ApiKey },
               { Constants.API_TOKEN, "tok_kKJsGqB1ARqjK4fv" },
            };
            this.UserParameters = new Hashtable()
            {
                { Constants.CLIENT_ID, ClientId },
                { Constants.SESSION_ID, SessionId }
            };
        }

        [Test]
        public void GetBrowseFacetOptionsResults()
        {
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest(filterName);
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseFacetOptionsResponse res = constructorio.Browse.GetBrowseFacetOptionsResult(req);
            Assert.AreEqual(res.Response.Facets.Count, 1, "length of facets expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "result_id exists");
        }

        [Test]
        public void GetBrowseFacetsWithFmtOptionParams()
        {
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest(filterName);
            req.showHiddenFacets = true;
            req.showProtectedFacets = true;
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            BrowseFacetOptionsResponse res = constructorio.Browse.GetBrowseFacetOptionsResult(req);
            Assert.GreaterOrEqual(res.Response.Facets.Count, 1, "length of facets expected to be equal to 1");
            Assert.IsNotNull(res.ResultId, "result_id exists");
        }
    }
}