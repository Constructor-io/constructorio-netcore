using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class AutocompleteTest
    {
        private readonly string ApiKey = "key_vM4GkLckwiuxwyRA";
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private ConstructorioConfig Config;
        private UserInfo UserInfo;

        [SetUp]
        public void Setup()
        {
            this.Config = new ConstructorioConfig(this.ApiKey);
            this.UserInfo = new UserInfo(ClientId, SessionId);
        }

        [Test]
        public void GetAutocompleteResultsWithInvalidApiKeyShouldError()
        {
            AutocompleteRequest req = new AutocompleteRequest("item") { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(new ConstructorioConfig("invalidKey"));
            var ex = Assert.ThrowsAsync<ConstructorException>(
                () => constructorio.Autocomplete.GetAutocompleteResults(req)
            );
            Assert.IsTrue(
                ex.Message
                    == "Http[400]: You have supplied an invalid `key` or `autocomplete_key`. You can find your key at app.constructor.io/dashboard/accounts/api_integration.",
                "Correct Error is Returned"
            );
        }

        [Test]
        public async Task GetAutocompleteResults()
        {
            AutocompleteRequest req = new AutocompleteRequest("item") { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            AutocompleteResponse res = await constructorio.Autocomplete.GetAutocompleteResults(req);
            res.Sections.TryGetValue("Products", out List<Result> Products);
            Dictionary<string, object> labels = Products[0].Labels;

            labels.TryGetValue("is_sponsored", out object isSponsored);

            Assert.AreEqual(true, (bool)isSponsored);
            Assert.NotNull(res.ResultId, "Result id exists");
        }

        [Test]
        public async Task GetAutocompleteResultsShouldReturnResultWithVariationsMap()
        {
            AutocompleteRequest req = new AutocompleteRequest("item1")
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
            AutocompleteResponse res = await constructorio.Autocomplete.GetAutocompleteResults(req);
            res.Request.TryGetValue("variations_map", out object reqVariationsMap);
            JObject variationMapResult = JObject.Parse(
                "    {\r\n  \"filter_by\": {\r\n    \"and\": [\r\n      {\r\n        \"not\": {\r\n          \"field\": \"data.brand\",\r\n          \"value\": \"Best Brand\"\r\n        }\r\n      }\r\n    ]\r\n  },\r\n  \"group_by\": [\r\n    {\r\n      \"name\": \"url\",\r\n      \"field\": \"data.url\"\r\n    }\r\n  ],\r\n  \"values\": {\r\n    \"variation_id\": {\r\n      \"aggregation\": \"first\",\r\n      \"field\": \"data.variation_id\"\r\n    },\r\n    \"deactivated\": {\r\n      \"aggregation\": \"first\",\r\n      \"field\": \"data.deactivated\"\r\n    }\r\n  },\r\n  \"dtype\": \"object\"\r\n}"
            );

            Assert.NotNull(res.ResultId, "Result id exists");
            var actualVariationMap = JObject.Parse(reqVariationsMap.ToString());

            Assert.IsTrue(actualVariationMap.ContainsKey("filter_by"), "Should contain filter_by");
            Assert.IsTrue(actualVariationMap.ContainsKey("group_by"), "Should contain group_by");
            Assert.IsTrue(actualVariationMap.ContainsKey("values"), "Should contain values");
        }

        [Test]
        public async Task GetAutocompleteResultsShouldReturnResultWithSearchSuggestionOnly()
        {
            AutocompleteRequest req = new AutocompleteRequest("jacket")
            {
                UserInfo = UserInfo,
                ResultsPerSection = new Dictionary<string, int> { { "Search Suggestions", 10 } }
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            AutocompleteResponse res = await constructorio.Autocomplete.GetAutocompleteResults(req);

            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.AreEqual(0, res.Sections["Products"].Count, "Products don't exist");
            Assert.GreaterOrEqual(
                res.Sections["Search Suggestions"].Count,
                4,
                "Search Suggestsions Exist"
            );
        }

        [Test]
        public async Task GetAutocompleteResultsShouldReturnResultWithHiddenFields()
        {
            AutocompleteRequest req = new AutocompleteRequest("item1")
            {
                UserInfo = UserInfo,
                HiddenFields = new List<string> { "testField" }
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            AutocompleteResponse res = await constructorio.Autocomplete.GetAutocompleteResults(req);

            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.GreaterOrEqual(res.Sections["Products"].Count, 5, "Results exist");
        }

        [Test]
        public async Task GetAutocompleteResultsShouldReturnResultWithMultipleFilters()
        {
            AutocompleteRequest req = new AutocompleteRequest("item")
            {
                UserInfo = UserInfo,
                Filters = new Dictionary<string, List<string>>()
            };
            req.Filters.Add("group_id", new List<string> { "All" });
            req.Filters.Add("Brand", new List<string> { "XYZ" });
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            AutocompleteResponse res = await constructorio.Autocomplete.GetAutocompleteResults(req);

            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.GreaterOrEqual(res.Sections["Products"].Count, 1, "Results exist");
        }

        [Test]
        public async Task GetAutocompleteResultsShouldReturnResultWithFiltersPerSection()
        {
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>();

            AutocompleteRequest req = new AutocompleteRequest("item")
            {
                UserInfo = UserInfo,
                FiltersPerSection = new Dictionary<string, Dictionary<string, List<string>>>()
            };

            filters.Add("group_id", new List<string> { "All" });
            req.FiltersPerSection.Add("Products", filters);
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            AutocompleteResponse res = await constructorio.Autocomplete.GetAutocompleteResults(req);

            Assert.NotNull(res.ResultId, "Result id exists");

            object responseFilters;
            res.Request.TryGetValue("filters", out responseFilters);

            JObject parsedResponseFilters = (JObject)responseFilters;
            Assert.NotNull(parsedResponseFilters, "Filters exist in response");

            JObject productsSection = (JObject)parsedResponseFilters["Products"];
            Assert.NotNull(productsSection, "Section exist in filters");

            JArray filterValues = (JArray)productsSection["group_id"];
            Assert.GreaterOrEqual(filterValues.Count, 1, "Results exist");
            Assert.AreEqual("All", filterValues.First.ToString());
        }
    }
}
