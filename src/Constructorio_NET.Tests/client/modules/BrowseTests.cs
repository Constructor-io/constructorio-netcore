using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class BrowseTest
    {
        private readonly string ApiKey = "key_vM4GkLckwiuxwyRA";
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

            this.Config = new ConstructorioConfig(this.ApiKey) { ApiToken = testApiToken };
            this.UserInfo = new UserInfo(ClientId, SessionId);
        }

        [Test]
        public void GetBrowseResultsWithInvalidApiKeyShouldError()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue);
            ConstructorIO constructorio = new ConstructorIO(new ConstructorioConfig("invalidKey"));
            var ex = Assert.ThrowsAsync<ConstructorException>(
                () => constructorio.Browse.GetBrowseResults(req)
            );
            Assert.IsTrue(
                ex.Message
                    == "Http[400]: You have supplied an invalid `key` or `autocomplete_key`. You can find your key at app.constructor.io/dashboard/accounts/api_integration.",
                "Correct Error is Returned"
            );
        }

        [Test]
        public async Task GetBrowseResults()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue);
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            Dictionary<string, object> labels = res.Response.Results[0].Labels;

            labels.TryGetValue("is_sponsored", out object isSponsored);

            Assert.AreEqual(true, (bool)isSponsored);
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
        public async Task GetBrowseResultGroupsData()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue);
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);

            Assert.IsNotNull(res.Response.Groups, "Groups should not be null");
            Assert.IsNotNull(res.Response.Groups[0].Data, "Groups[0].Data should not be null");
        }

        [Test]
        public async Task GetBrowseResultsWithFilters()
        {
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>()
            {
                {
                    "Brand",
                    new List<string>() { "XYZ" }
                }
            };
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue)
            {
                UserInfo = this.UserInfo,
                Filters = filters
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
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
            Assert.IsNotNull(res.Response.Facets[0].Data, "data object expected to exist");
            Assert.IsNotNull(res.Response.Facets[0].Hidden, "hidden field expected to exist");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetBrowseResultsWithPreFilterExpressionJson()
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
            BrowseRequest req = new BrowseRequest("group_id", "All")
            {
                UserInfo = this.UserInfo,
                PreFilterExpression = preFilterExpression,
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            res.Request.TryGetValue("pre_filter_expression", out object reqPreFilterExpression);

            Assert.AreEqual(
                reqPreFilterExpression,
                JObject.Parse(preFilterExpression.GetExpression()),
                "Pre Filter Expression differs in request"
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
        public async Task GetBrowseResultsWithPreFilterExpressionJsonString()
        {
            string preFilterExpressionJObject =
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
                preFilterExpressionJObject
            );
            BrowseRequest req = new BrowseRequest("group_id", "All")
            {
                UserInfo = this.UserInfo,
                PreFilterExpression = preFilterExpression,
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            res.Request.TryGetValue("pre_filter_expression", out object reqPreFilterExpression);

            Assert.AreEqual(
                reqPreFilterExpression,
                JObject.Parse(preFilterExpression.GetExpression()),
                "Pre Filter Expression differs in request"
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
        public async Task GetBrowseResultsWithPreFilterExpression()
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

            BrowseRequest req = new BrowseRequest("group_id", "All")
            {
                UserInfo = this.UserInfo,
                PreFilterExpression = preFilterExpression,
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            res.Request.TryGetValue("pre_filter_expression", out object reqPreFilterExpression);

            Assert.AreEqual(
                reqPreFilterExpression,
                JObject.Parse(preFilterExpression.GetExpression()),
                "Pre Filter Expression differs in request"
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
        public async Task GetBrowseResultsWithPreFilterExpressionNot()
        {
            ValuePreFilterExpression filterByColor1 = new ValuePreFilterExpression("Color", "Blue");
            NotPreFilterExpression preFilterExpression = new NotPreFilterExpression(filterByColor1);

            BrowseRequest req = new BrowseRequest("group_id", "All")
            {
                UserInfo = this.UserInfo,
                PreFilterExpression = preFilterExpression,
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            res.Request.TryGetValue("pre_filter_expression", out object reqPreFilterExpression);

            Assert.AreEqual(
                reqPreFilterExpression,
                JObject.Parse(preFilterExpression.GetExpression()),
                "Pre Filter Expression differs in request"
            );
            Assert.AreEqual(
                9,
                res.Response.Results.Count,
                "Total number of results expected to be 9"
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
        public async Task GetBrowseResultsWithPreFilterExpressionRange()
        {
            RangePreFilterExpression rangeFilterByPrice = new RangePreFilterExpression(
                "price_01",
                new List<string> { "0", "20" }
            );

            BrowseRequest req = new BrowseRequest("group_id", "All")
            {
                UserInfo = this.UserInfo,
                PreFilterExpression = rangeFilterByPrice,
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            res.Request.TryGetValue("pre_filter_expression", out object reqPreFilterExpression);

            Assert.AreEqual(
                reqPreFilterExpression,
                JObject.Parse(rangeFilterByPrice.GetExpression()),
                "Pre Filter Expression differs in request"
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
        public async Task GetBrowseResultsWithPreFilterExpressionMultiple()
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

            BrowseRequest req = new BrowseRequest("group_id", "All")
            {
                UserInfo = this.UserInfo,
                PreFilterExpression = preFilterExpression,
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            res.Request.TryGetValue("pre_filter_expression", out object reqPreFilterExpression);

            Assert.AreEqual(
                reqPreFilterExpression,
                JObject.Parse(preFilterExpression.GetExpression()),
                "Pre Filter Expression differs in request"
            );
            Assert.AreEqual(
                7,
                res.Response.Results.Count,
                "Total number of results expected to be 7"
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
        public async Task GetBrowseResultsWithResultParams()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue)
            {
                UserInfo = this.UserInfo,
                Page = 1,
                ResultsPerPage = 1
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            Assert.AreEqual(
                1,
                (long)res.Request["page"],
                "total number of results expected to be 1"
            );
            Assert.AreEqual(
                1,
                (long)res.Request["num_results_per_page"],
                "Expect request to include num_results_per_page parameter"
            );
            Assert.AreEqual(
                1,
                res.Response.TotalNumResults,
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
        public async Task GetBrowseResultsWithResultParamsWithOffset()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, "yellow")
            {
                UserInfo = this.UserInfo,
                Offset = 1,
                ResultsPerPage = 1
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            Assert.AreEqual(
                1,
                (long)res.Request["offset"],
                "total number of results expected to be 1"
            );
            Assert.AreEqual(
                1,
                (long)res.Request["num_results_per_page"],
                "Expect request to include num_results_per_page parameter"
            );
            Assert.AreEqual(
                2,
                res.Response.TotalNumResults,
                "total number of results expected to be 2"
            );
            Assert.AreEqual(
                1,
                res.Response.Results.Count,
                "length of results expected to be equal to 1"
            );
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
                this.CollectionId,
                res.Response.Collection.DisplayName,
                "display name should match"
            );
            Assert.AreEqual(this.CollectionId, res.Response.Collection.Id, "id should match");
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetBrowseResultsShouldReturnResultWithHiddenFields()
        {
            string requestedHiddenField = "testField";
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue)
            {
                UserInfo = this.UserInfo,
                HiddenFields = new List<string> { requestedHiddenField }
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            var returnedHiddenfield = res.Response.Results[0].Data.Metadata[requestedHiddenField];

            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.NotNull(returnedHiddenfield, "Hidden field returned");
        }

        [Test]
        public async Task GetBrowseResultsShouldReturnResultWithHiddenFacets()
        {
            string requestedHiddenFacet = "Brand";
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue)
            {
                UserInfo = this.UserInfo,
                HiddenFacets = new List<string> { requestedHiddenFacet }
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            FilterFacet returnedHiddenFacet = res.Response.Facets.Find(el => el.Hidden);

            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.True(
                requestedHiddenFacet == returnedHiddenFacet.DisplayName,
                "Hidden facet returned"
            );
            Assert.True(returnedHiddenFacet.Hidden, "Returned facet is hidden");
        }

        [Test]
        public async Task GetBrowseResultsShouldReturnResultWithVariationsMap()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue)
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
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            res.Request.TryGetValue("variations_map", out object reqVariationsMap);
            JObject variationMapResult = JObject.Parse(
                "{ \"filter_by\": { \"type\": \"and\", \"and\": [{ \"type\": \"not\", \"not\": { \"type\": \"single\", \"field\": \"data.brand\", \"value\": \"Best Brand\" }}]}, \"group_by\": [{ \"name\": \"url\", \"field\": \"data.url\"}], \"values\": { \"variation_id\": { \"aggregation\": \"first\", \"field\": \"data.variation_id\"}, \"deactivated\": { \"aggregation\": \"first\", \"field\": \"data.deactivated\"}}, \"dtype\": \"object\" }"
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
        public async Task GetBrowseResultsShouldReturnResultWithResultSources()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue)
            {
                UserInfo = UserInfo,
                Page = 1,
                ResultsPerPage = 1
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
            Assert.NotNull(res.Response.ResultSources.TokenMatch, "token match exists");
            Assert.NotNull(res.Response.ResultSources.EmbeddingsMatch, "embeddings match exists");
            Assert.AreEqual(
                1,
                res.Response.ResultSources.TokenMatch.Count,
                "number of token matches expected to be 1"
            );
            Assert.AreEqual(
                0,
                res.Response.ResultSources.EmbeddingsMatch.Count,
                "number of embeddings matches expected to be 0"
            );
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
        public async Task GetBrowseItemsResultsWithFilters()
        {
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>()
            {
                {
                    "Brand",
                    new List<string>() { "XYZ" }
                }
            };
            BrowseItemsRequest req = new BrowseItemsRequest(this.ItemIds)
            {
                UserInfo = this.UserInfo,
                Filters = filters
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseItemsResult(req);
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
            Assert.IsNotNull(res.Response.Facets[0].Data, "data object expected to exist");
            Assert.IsNotNull(res.Response.Facets[0].Hidden, "hidden field expected to exist");
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
            Assert.AreEqual(
                1,
                (long)res.Request["num_results_per_page"],
                "Expect request to include page parameter"
            );
            Assert.AreEqual(
                2,
                res.Response.TotalNumResults,
                "total number of results expected to be 2"
            );
            Assert.AreEqual(
                1,
                res.Response.Results.Count,
                "length of results expected to be equal to 1"
            );
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetBrowseFacetsResults()
        {
            BrowseFacetsRequest req = new BrowseFacetsRequest { UserInfo = this.UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseFacetsResponse res = await constructorio.Browse.GetBrowseFacetsResult(req);
            Assert.Greater(
                res.Response.TotalNumResults,
                0,
                "total number of results expected to be greater than 0"
            );
            Assert.Greater(
                res.Response.Facets.Count,
                0,
                "length of facets expected to be greater than 0"
            );
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
            Assert.Greater(
                res.Response.TotalNumResults,
                0,
                "total number of results expected to be greater than 0"
            );
            Assert.AreEqual(
                1,
                res.Response.Facets.Count,
                "length of facets expected to be equal to 1"
            );
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
            Assert.Greater(
                res.Response.TotalNumResults,
                0,
                "total number of results expected to be greater than 0"
            );
            Assert.Greater(
                res.Response.Facets.Count,
                0,
                "length of facets expected to be greater than 0"
            );
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
            BrowseFacetOptionsResponse res = await constructorio.Browse.GetBrowseFacetOptionsResult(
                req
            );
            Assert.AreEqual(
                1,
                res.Response.Facets.Count,
                "length of facets expected to be equal to 1"
            );
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
            BrowseFacetOptionsResponse res = await constructorio.Browse.GetBrowseFacetOptionsResult(
                req
            );
            Assert.GreaterOrEqual(
                res.Response.Facets.Count,
                1,
                "length of facets expected to be equal to 1"
            );
            Assert.IsNotNull(res.ResultId, "ResultId should exist");
        }

        [Test]
        public async Task GetBrowseResultsShouldReturnResultWithRefinedContent()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue);
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            BrowseResponse res = await constructorio.Browse.GetBrowseResults(req);
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
        public void GetBrowseResultsWithPageAndOffset()
        {
            BrowseRequest req = new BrowseRequest(this.FilterName, this.FilterValue)
            {
                UserInfo = this.UserInfo,
                Page = 1,
                Offset = 2,
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var ex = Assert.ThrowsAsync<ConstructorException>(
                () => constructorio.Browse.GetBrowseResults(req)
            );
            Assert.IsTrue(
                ex.Message == "Http[400]: offset, page are mutually exclusive",
                "Correct error is returned"
            );
        }
    }
}
