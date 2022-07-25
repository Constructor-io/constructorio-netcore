using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class AutocompleteTest
    {
        private readonly string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
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
        public async Task GetAutocompleteResults()
        {
            AutocompleteRequest req = new AutocompleteRequest("item")
            {
                UserInfo = UserInfo
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            AutocompleteResponse res = await constructorio.Autocomplete.GetAutocompleteResults(req);

            Assert.NotNull(res.ResultId, "Result id exists");
        }

        [Test]
        public async Task GetAutocompleteResultsShouldReturnResultWithVariationMap()
        {
            AutocompleteRequest req = new AutocompleteRequest("item1")
            {
                UserInfo = UserInfo,
                VariationMap = new VariationsMap()
            };
            req.VariationMap.AddGroupByRule("url", "data.url");
            req.VariationMap.AddValueRule("variation_id", AggregationTypes.First, "data.variation_id");
            req.VariationMap.AddValueRule("deactivated", AggregationTypes.First, "data.deactivated");
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            AutocompleteResponse res = await constructorio.Autocomplete.GetAutocompleteResults(req);
            res.Request.TryGetValue("variations_map", out object reqVariationsMap);

            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.NotNull(reqVariationsMap, "Variations Map was passed as parameter");
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
            Assert.GreaterOrEqual(res.Sections["Search Suggestions"].Count, 4, "Search Suggestsions Exist");
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
    }
}