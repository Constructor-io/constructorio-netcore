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
            SearchRequest req = new SearchRequest(this.Query) { UserInfo = this.UserInfo };
            ConstructorIO constructorio = new ConstructorIO(new ConstructorioConfig("invalidKey"));
            var ex = Assert.ThrowsAsync<ConstructorException>(
                () => constructorio.Search.GetSearchResults(req)
            );
            Assert.IsTrue(
                ex.Message
                    == "Http[400]: You have supplied an invalid `key` or `autocomplete_key`. You can find your key at app.constructor.io/dashboard/accounts/api_integration.",
                "Correct Error is Returned"
            );
        }

        [Test]
        public async Task GetSearchResults()
        {
            SearchRequest req = new SearchRequest(this.Query) { UserInfo = this.UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            Dictionary<string, object> labels = res.Response.Results[0].Labels;

            labels.TryGetValue("is_sponsored", out object isSponsored);

            Assert.AreEqual((bool)isSponsored, true);
            Assert.Greater(
                res.Response.TotalNumResults,
                0,
                "total number of results expected to be greater than 0"
            );
            Assert.Greater(
                res.Response.Results.Count,
                0,
                "length of results expected to be greater than 0"
            );
            Assert.Greater(
                res.Response.Facets.Count,
                0,
                "length of facets expected to be greater than 0"
            );
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsGroupData()
        {
            SearchRequest req = new SearchRequest(this.Query) { UserInfo = this.UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);

            Assert.IsNotNull(res.Response.Groups, "Groups should not be null");
            Assert.IsNotNull(res.Response.Groups[0].Data, "Groups[0].Data should not be null");
        }

        [Test]
        public async Task GetSearchResultsWithFilters()
        {
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>()
            {
                {
                    "Color",
                    new List<string>() { "green", "blue" }
                }
            };
            SearchRequest req = new SearchRequest(this.Query)
            {
                UserInfo = this.UserInfo,
                Filters = filters
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            Assert.Greater(
                res.Response.TotalNumResults,
                0,
                "total number of results expected to be greater than 0"
            );
            Assert.Greater(
                res.Response.Results.Count,
                0,
                "length of results expected to be greater than 0"
            );
            Assert.Greater(
                res.Response.Facets.Count,
                0,
                "length of facets expected to be greater than 0"
            );
            Assert.IsNotNull(res.Response.Facets[0].Max);
            Assert.IsNotNull(res.Response.Facets[0].Min);
            Assert.IsNotNull(res.Response.Facets[0].Data, "data object expected to exist");
            Assert.IsNotNull(res.Response.Facets[0].Hidden, "hidden field expected to exist");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsWithPreFilterExpressionJson()
        {
            JObject preFilterExpressionJObject = JObject.Parse(
                @"{
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
            }"
            );
            JsonPrefilterExpression preFilterExpression = new JsonPrefilterExpression(
                preFilterExpressionJObject
            );
            SearchRequest req = new SearchRequest(this.Query)
            {
                UserInfo = this.UserInfo,
                PreFilterExpression = preFilterExpression,
            };

            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            res.Request.TryGetValue("pre_filter_expression", out object reqPreFilterExpression);

            Assert.AreEqual(
                reqPreFilterExpression,
                JObject.Parse(preFilterExpression.GetExpression()),
                "Pre Filter Expression is sent in request"
            );
            Assert.AreEqual(
                2,
                res.Response.Results.Count,
                "Total number of results expected to be 2"
            );
            Assert.IsTrue(
                res.Response.Results.TrueForAll(result =>
                {
                    var facetValue = result.Data.Facets.Find(facet => facet.Name == "Color");
                    return facetValue.Values.Contains("red") || facetValue.Values.Contains("blue");
                }),
                "Result set contains items with Facet.Color other than red or blue"
            );
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsWithPreFilterExpressionJsonString()
        {
            string preFilterExpressionJson =
                @"{
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
            }";
            JsonPrefilterExpression preFilterExpression = new JsonPrefilterExpression(
                preFilterExpressionJson
            );
            SearchRequest req = new SearchRequest(this.Query)
            {
                UserInfo = this.UserInfo,
                PreFilterExpression = preFilterExpression,
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            res.Request.TryGetValue("pre_filter_expression", out object reqPreFilterExpression);

            Assert.AreEqual(
                reqPreFilterExpression,
                JObject.Parse(preFilterExpression.GetExpression()),
                "Pre Filter Expression is sent in request"
            );
            Assert.AreEqual(
                2,
                res.Response.Results.Count,
                "Total number of results expected to be 2"
            );
            Assert.IsTrue(
                res.Response.Results.TrueForAll(result =>
                {
                    var facetValue = result.Data.Facets.Find(facet => facet.Name == "Color");
                    return facetValue.Values.Contains("red") || facetValue.Values.Contains("blue");
                }),
                "Result set contains items with Facet.Color other than red or blue"
            );
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsWithPreFilterExpression()
        {
            ValuePreFilterExpression filterByGroupId1 = new ValuePreFilterExpression(
                "group_id",
                "BrandXY"
            );
            ValuePreFilterExpression filterByColor1 = new ValuePreFilterExpression("Color", "red");
            AndPreFilterExpression filterByBothGroupIdAndColor1 = new AndPreFilterExpression(
                new List<PreFilterExpression> { filterByGroupId1, filterByColor1 }
            );

            ValuePreFilterExpression filterByBrand2 = new ValuePreFilterExpression("Brand", "XYZ");
            ValuePreFilterExpression filterByColor2 = new ValuePreFilterExpression("Color", "blue");
            AndPreFilterExpression filterByBothBrandAndColor2 = new AndPreFilterExpression(
                new List<PreFilterExpression> { filterByBrand2, filterByColor2 }
            );

            OrPreFilterExpression preFilterExpression = new OrPreFilterExpression();
            preFilterExpression.Or = new List<PreFilterExpression>
            {
                filterByBothGroupIdAndColor1,
                filterByBothBrandAndColor2
            };

            SearchRequest req = new SearchRequest(this.Query)
            {
                UserInfo = this.UserInfo,
                PreFilterExpression = preFilterExpression,
            };

            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            res.Request.TryGetValue("pre_filter_expression", out object reqPreFilterExpression);

            Assert.AreEqual(
                reqPreFilterExpression,
                JObject.Parse(preFilterExpression.GetExpression()),
                "Pre Filter Expression is sent in request"
            );
            Assert.AreEqual(
                2,
                res.Response.Results.Count,
                "total number of results expected to be 2"
            );
            Assert.IsTrue(
                res.Response.Results.TrueForAll(result =>
                {
                    var facetValue = result.Data.Facets.Find(facet => facet.Name == "Color");
                    return facetValue.Values.Contains("red") || facetValue.Values.Contains("blue");
                }),
                "Result set contains items with Facet.Color other than red or blue"
            );
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsWithPreFilterExpressionNot()
        {
            ValuePreFilterExpression filterByColor1 = new ValuePreFilterExpression("Color", "Blue");
            NotPreFilterExpression preFilterExpression = new NotPreFilterExpression(filterByColor1);

            SearchRequest req = new SearchRequest(this.Query)
            {
                UserInfo = this.UserInfo,
                PreFilterExpression = preFilterExpression,
            };

            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            res.Request.TryGetValue("pre_filter_expression", out object reqPreFilterExpression);

            Assert.AreEqual(
                reqPreFilterExpression,
                JObject.Parse(preFilterExpression.GetExpression()),
                "Pre Filter Expression is sent in request"
            );
            Assert.AreEqual(
                5,
                res.Response.Results.Count,
                "Total number of results expected to be 5"
            );
            Assert.IsTrue(
                res.Response.Results.TrueForAll(result =>
                {
                    var facetValue = result.Data.Facets.Find(facet => facet.Name == "Color");
                    return facetValue == null || !facetValue.Values.Contains("Blue");
                }),
                "Result set contains facet.Color = Blue"
            );
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsWithPreFilterExpressionRange()
        {
            RangePreFilterExpression rangeFilterByPrice = new RangePreFilterExpression(
                "price_01",
                new List<string> { "0", "20" }
            );

            SearchRequest req = new SearchRequest("item2")
            {
                UserInfo = this.UserInfo,
                PreFilterExpression = rangeFilterByPrice,
            };

            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            res.Request.TryGetValue("pre_filter_expression", out object reqPreFilterExpression);

            Assert.AreEqual(
                reqPreFilterExpression,
                JObject.Parse(rangeFilterByPrice.GetExpression()),
                "Pre Filter Expression is sent in request"
            );
            Assert.AreEqual(
                1,
                res.Response.Results.Count,
                "Total number of results expected to be 1"
            );

            Assert.IsTrue(
                res.Response.Results.TrueForAll(result =>
                {
                    var facetValue = double.Parse(
                        result.Data.Facets.Find(facet => facet.Name == "price_01").Values[0]
                    );
                    return facetValue < 20 && facetValue > 0;
                }),
                "Result set consists of only filtered items"
            );
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsWithPreFilterExpressionMultiple()
        {
            ValuePreFilterExpression filterByGroupId1 = new ValuePreFilterExpression(
                "group_id",
                "StyleB"
            );
            ValuePreFilterExpression filterByColor1 = new ValuePreFilterExpression("Color", "red");
            OrPreFilterExpression orExpression = new OrPreFilterExpression(
                new List<PreFilterExpression> { filterByColor1, filterByGroupId1 }
            );

            ValuePreFilterExpression filterByBrand2 = new ValuePreFilterExpression("Brand", "XYZ");
            ValuePreFilterExpression filterByColor2 = new ValuePreFilterExpression("Color", "blue");
            AndPreFilterExpression andExpression = new AndPreFilterExpression(
                new List<PreFilterExpression> { filterByBrand2, filterByColor2 }
            );

            ValuePreFilterExpression valueExpression = new ValuePreFilterExpression(
                "Color",
                "silver"
            );

            RangePreFilterExpression rangeExpression = new RangePreFilterExpression(
                "price_01",
                new List<string> { "0", "20" }
            );

            OrPreFilterExpression preFilterExpression = new OrPreFilterExpression(
                new List<PreFilterExpression>
                {
                    orExpression,
                    andExpression,
                    valueExpression,
                    rangeExpression
                }
            );

            SearchRequest req = new SearchRequest("item2")
            {
                UserInfo = this.UserInfo,
                PreFilterExpression = preFilterExpression,
            };

            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            res.Request.TryGetValue("pre_filter_expression", out object reqPreFilterExpression);

            Assert.AreEqual(
                reqPreFilterExpression,
                JObject.Parse(preFilterExpression.GetExpression()),
                "Pre Filter Expression differs in request"
            );
            Assert.AreEqual(
                4,
                res.Response.Results.Count,
                "Total number of results expected to be 3"
            );
            Assert.IsTrue(
                res.Response.Results.TrueForAll(result =>
                {
                    var groupIdObj = result.Data.Groups;
                    var facetPriceObj = result.Data.Facets.Find(facet => facet.Name == "price_01");
                    var facetColorObj = result.Data.Facets.Find(facet => facet.Name == "Color");
                    var facetBrandsObj = result.Data.Facets.Find(facet => facet.Name == "Brand");

                    double? facetPrice = null;
                    if (facetPriceObj != null)
                    {
                        facetPrice = double.Parse(facetPriceObj.Values[0]);
                    }

                    List<string> facetColors = new List<string>();
                    if (facetColorObj != null)
                    {
                        facetColors = facetColorObj.Values;
                    }

                    List<string> facetBrands = new List<string>();
                    if (facetBrandsObj != null)
                    {
                        facetBrands = facetBrandsObj.Values;
                    }

                    List<string> groupIds = new List<string>();
                    if (groupIdObj != null)
                    {
                        groupIds = groupIdObj.ConvertAll<string>(group => group.GroupId);
                    }

                    if (groupIds.Contains("StyleB"))
                    {
                        return true;
                    }
                    else if (facetColors.Contains("red"))
                    {
                        return true;
                    }
                    else if (facetBrands.Contains("XYZ") && facetColors.Contains("blue"))
                    {
                        return true;
                    }
                    else if (facetColors.Contains("silver"))
                    {
                        return true;
                    }
                    else if (facetPrice != null && facetPrice > 0 && facetPrice < 20)
                    {
                        return true;
                    }

                    return false;
                }),
                "Result set contains items other than filtered items"
            );
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
            Assert.AreEqual(
                3,
                (long)res.Request["page"],
                "total number of results expected to be 1"
            );
            Assert.AreEqual(
                1,
                (long)res.Request["num_results_per_page"],
                "Expect request to include num_results_per_page parameter"
            );
            Assert.Greater(
                res.Response.TotalNumResults,
                1,
                "total number of results expected to be 1"
            );
            Assert.AreEqual(
                1,
                res.Response.Results.Count,
                "length of results expected to be equal to 1"
            );
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsWithResultParamsWithOffset()
        {
            SearchRequest req = new SearchRequest(this.Query)
            {
                UserInfo = this.UserInfo,
                Offset = 3,
                ResultsPerPage = 1
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            Assert.AreEqual(
                3,
                (long)res.Request["offset"],
                "total number of results expected to be 1"
            );
            Assert.Greater(
                res.Response.TotalNumResults,
                1,
                "total number of results expected to be 1"
            );
            Assert.AreEqual(
                1,
                res.Response.Results.Count,
                "length of results expected to be equal to 1"
            );
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsWithRedirect()
        {
            SearchRequest req = new SearchRequest("rolling") { UserInfo = this.UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            Assert.IsNotNull(res.Response.Redirect, "Redirect should exist");
            Assert.IsNotNull(res.Response.Redirect.Data.Url, "Url should exist");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsShouldReturnResultWithRefinedContent()
        {
            SearchRequest req = new SearchRequest("item") { UserInfo = this.UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            Assert.Greater(
                res.Response.TotalNumResults,
                0,
                "total number of results expected to be greater than 0"
            );
            Assert.Greater(
                res.Response.Results.Count,
                0,
                "length of results expected to be greater than 0"
            );
            Assert.Greater(
                res.Response.Facets.Count,
                0,
                "length of facets expected to be greater than 0"
            );
            Assert.AreEqual(
                3,
                res.Response.RefinedContent.Count,
                "length of refined content expected to be 3"
            );
            Assert.NotNull(res.Response.RefinedContent[0], "refined content data exists");
            Assert.AreEqual(
                "Content 1 Body",
                res.Response.RefinedContent[0].Data["body"],
                "refined content body is correct"
            );
            Assert.AreEqual(
                "Content 1 Header",
                res.Response.RefinedContent[0].Data["header"],
                "refined content header is correct"
            );
            Assert.AreEqual(
                "Content 1 desktop alt text",
                res.Response.RefinedContent[0].Data["altText"],
                "refined content altText is correct"
            );
            Assert.AreEqual(
                "https://constructor.io/wp-content/uploads/2022/09/groceryshop-2022-r2.png",
                res.Response.RefinedContent[0].Data["ctaLink"],
                "refined content ctaLink is correct"
            );
            Assert.AreEqual(
                "Content 1 CTA Button",
                res.Response.RefinedContent[0].Data["ctaText"],
                "refined content ctaText is correct"
            );
            Assert.AreEqual(
                "https://constructor.io/wp-content/uploads/2022/09/groceryshop-2022-r2.png",
                res.Response.RefinedContent[0].Data["assetUrl"],
                "refined content assetUrl is correct"
            );
            Assert.AreEqual(
                "https://constructor.io/wp-content/uploads/2022/09/groceryshop-2022-r2.png",
                res.Response.RefinedContent[0].Data["mobileAssetUrl"],
                "refined content mobileAssetUrl is correct"
            );
            Assert.AreEqual(
                "Content 1 mobile alt text",
                res.Response.RefinedContent[0].Data["mobileAssetAltText"],
                "refined content mobileAssetAltText is correct"
            );
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetSearchResultsShouldReturnResultWithResultSources()
        {
            SearchRequest req = new SearchRequest("item") { UserInfo = this.UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            Assert.NotNull(res.Response.ResultSources.TokenMatch, "token match exists");
            Assert.NotNull(res.Response.ResultSources.EmbeddingsMatch, "embeddings match exists");
            Assert.AreEqual(
                5,
                res.Response.ResultSources.TokenMatch.Count,
                "number of token matches expected to be 5"
            );
            Assert.AreEqual(
                0,
                res.Response.ResultSources.EmbeddingsMatch.Count,
                "number of embeddings matches expected to be 0"
            );
        }

        [Test]
        public async Task GetSearchResultsShouldReturnResultWithHiddenFields()
        {
            string requestedHiddenField = "testField";
            SearchRequest req = new SearchRequest("item1")
            {
                UserInfo = this.UserInfo,
                HiddenFields = new List<string> { requestedHiddenField }
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            var returnedHiddenfield = res.Response.Results[0].Data.Metadata[requestedHiddenField];

            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.NotNull(returnedHiddenfield, "Hidden field returned");
        }

        [Test]
        public async Task GetSearchResultsShouldReturnResultWithHiddenFacets()
        {
            string requestedHiddenFacet = "Brand";
            SearchRequest req = new SearchRequest("item1")
            {
                UserInfo = this.UserInfo,
                HiddenFacets = new List<string> { requestedHiddenFacet }
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            FilterFacet returnedHiddenFacet = res.Response.Facets.Find(el => el.Hidden);

            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.True(
                requestedHiddenFacet == returnedHiddenFacet.DisplayName,
                "Hidden facet returned"
            );
            Assert.True(returnedHiddenFacet.Hidden, "Returned facet is hidden");
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
            req.VariationsMap.AddValueRule(
                "variation_id",
                AggregationTypes.First,
                "data.variation_id"
            );
            req.VariationsMap.AddValueRule(
                "deactivated",
                AggregationTypes.First,
                "data.deactivated"
            );
            req.VariationsMap.AddFilterByRule(
                "{\"and\":[{\"not\":{\"field\":\"data.brand\",\"value\":\"Best Brand\"}}]}"
            );
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SearchResponse res = await constructorio.Search.GetSearchResults(req);
            res.Request.TryGetValue("variations_map", out object reqVariationsMap);
            JObject variationMapResult = JObject.Parse(
                "    {\r\n  \"filter_by\": {\r\n    \"and\": [\r\n      {\r\n        \"not\": {\r\n          \"field\": \"data.brand\",\r\n          \"value\": \"Best Brand\"\r\n        }\r\n      }\r\n    ]\r\n  },\r\n  \"group_by\": [\r\n    {\r\n      \"name\": \"url\",\r\n      \"field\": \"data.url\"\r\n    }\r\n  ],\r\n  \"values\": {\r\n    \"variation_id\": {\r\n      \"aggregation\": \"first\",\r\n      \"field\": \"data.variation_id\"\r\n    },\r\n    \"deactivated\": {\r\n      \"aggregation\": \"first\",\r\n      \"field\": \"data.deactivated\"\r\n    }\r\n  },\r\n  \"dtype\": \"object\"\r\n}"
            );

            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.AreEqual(
                JObject.Parse(reqVariationsMap.ToString()),
                variationMapResult,
                "Variations Map was passed as parameter"
            );
            Assert.NotNull(res.Response.Results[0].VariationsMap, "Variations Map exists");
        }

        [Test]
        public void GetSearchResultsWithPageAndOffset()
        {
            SearchRequest req = new SearchRequest(this.Query)
            {
                UserInfo = this.UserInfo,
                Page = 1,
                Offset = 2,
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var ex = Assert.ThrowsAsync<ConstructorException>(
                () => constructorio.Search.GetSearchResults(req)
            );
            Assert.IsTrue(
                ex.Message == "Http[400]: offset, page are mutually exclusive",
                "Correct error is returned"
            );
        }
    }
}

