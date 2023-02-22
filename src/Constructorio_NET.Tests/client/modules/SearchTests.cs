using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class SearchTest
    {
        private readonly string ApiKey = "key_vM4GkLckwiuxwyRA";
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
            Assert.IsTrue(ex.Message == "Http[400]: You have supplied an invalid `key` or `autocomplete_key`. You can find your key at app.constructor.io/dashboard/accounts/api_integration.", "Correct Error is Returned");
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
            Assert.IsNotNull(res.Response.Facets[0].Max);
            Assert.IsNotNull(res.Response.Facets[0].Min);
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsWithPreFilterExpression()
        {
            JObject preFilterExpression = JObject.Parse(@"{
                or: [
                    {
                    and:
                        [
                        { name: 'group_id', value: 'BrandXY' },
                        { name: 'Color', value: 'red' },
                    ],
                    },
                    {
                    and:
                        [
                        { name: 'Color', value: 'blue' },
                        { name: 'Brand', value: 'XYZ' },
                    ],
                    },
                ],
            }");
            SearchRequest req = new SearchRequest(this.Query)
            {
                UserInfo = this.UserInfo,
                PreFilterExpression = preFilterExpression,

            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            res.Request.TryGetValue("pre_filter_expression", out object reqPreFilterExpression);

            Assert.AreEqual(reqPreFilterExpression, preFilterExpression, "Pre Filter Expression is sent in request");
            Assert.AreEqual(2, res.Response.Results.Count, "total number of results expected to be 2");
            Assert.IsTrue(
                res.Response.Results.TrueForAll(result =>
                {
                    var facetValue = result.Data.Facets.Find(facet => facet.Name == "Color");
                    return facetValue.Values.Contains("red") || facetValue.Values.Contains("blue");
                }),
                "Result set consists of only filtered items");
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
            SearchRequest req = new SearchRequest("rolling")
            {
                UserInfo = this.UserInfo
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            Assert.IsNotNull(res.Response.Redirect, "Redirect should exist");
            Assert.IsNotNull(res.Response.Redirect.Data.Url, "Url should exist");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsShouldReturnResultWithRefinedContent()
        {
            SearchRequest req = new SearchRequest("item")
            {
                UserInfo = this.UserInfo
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            Assert.Greater(res.Response.TotalNumResults, 0, "total number of results expected to be greater than 0");
            Assert.Greater(res.Response.Results.Count, 0, "length of results expected to be greater than 0");
            Assert.Greater(res.Response.Facets.Count, 0, "length of facets expected to be greater than 0");
            Assert.AreEqual(3, res.Response.RefinedContent.Count, "length of refined content expected to be 3");
            Assert.NotNull(res.Response.RefinedContent[0], "refined content data exists");
            Assert.AreEqual("Content 1 Body", res.Response.RefinedContent[0].Data["body"], "refined content body is correct");
            Assert.AreEqual("Content 1 Header", res.Response.RefinedContent[0].Data["header"], "refined content header is correct");
            Assert.AreEqual("Content 1 desktop alt text", res.Response.RefinedContent[0].Data["altText"], "refined content altText is correct");
            Assert.AreEqual("https://constructor.io/wp-content/uploads/2022/09/groceryshop-2022-r2.png", res.Response.RefinedContent[0].Data["ctaLink"], "refined content ctaLink is correct");
            Assert.AreEqual("Content 1 CTA Button", res.Response.RefinedContent[0].Data["ctaText"], "refined content ctaText is correct");
            Assert.AreEqual("https://constructor.io/wp-content/uploads/2022/09/groceryshop-2022-r2.png", res.Response.RefinedContent[0].Data["assetUrl"], "refined content assetUrl is correct");
            Assert.AreEqual("https://constructor.io/wp-content/uploads/2022/09/groceryshop-2022-r2.png", res.Response.RefinedContent[0].Data["mobileAssetUrl"], "refined content mobileAssetUrl is correct");
            Assert.AreEqual("Content 1 mobile alt text", res.Response.RefinedContent[0].Data["mobileAssetAltText"], "refined content mobileAssetAltText is correct");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsShouldReturnResultWithVariationsMap()
        {
            SearchRequest req = new SearchRequest("item1")
            {
                UserInfo = UserInfo,
                VariationsMap = new VariationsMap()
            };
            req.VariationsMap.AddGroupByRule("url", "data.url");
            req.VariationsMap.AddValueRule("variation_id", AggregationTypes.First, "data.variation_id");
            req.VariationsMap.AddValueRule("deactivated", AggregationTypes.First, "data.deactivated");
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            res.Request.TryGetValue("variations_map", out object reqVariationsMap);

            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.NotNull(reqVariationsMap, "Variations Map was passed as parameter");
            Assert.NotNull(res.Response.Results[0].VariationsMap, "Variations Map exists");
        }
    }
}