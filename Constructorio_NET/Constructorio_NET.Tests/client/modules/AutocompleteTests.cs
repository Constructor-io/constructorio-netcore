using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class AutocompleteTest
    {
        private string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private Hashtable Options = new Hashtable();

        [SetUp]
        public void Setup()
        {
            this.Options = new Hashtable()
            {
               { Constants.API_KEY, this.ApiKey }
            };
        }

        [Test]
        public void GetAutocompleteResults()
        {
            AutocompleteRequest req = new AutocompleteRequest("item");
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            AutocompleteResponse res = constructorio.Autocomplete.GetAutocompleteResults(req);

            Assert.NotNull(res.ResultId, "Result id exists");
        }

        [Test]
        public void GetAutocompleteResultsShouldReturnResultWithVariationMap()
        {
            AutocompleteRequest req = new AutocompleteRequest("item1");
            req.VariationMap = new VariationsMap();
            req.VariationMap.addGroupByRule("url", "data.url");
            req.VariationMap.addValueRule("variation_id", AggregationTypes.First, "data.variation_id");
            req.VariationMap.addValueRule("deactivated", AggregationTypes.First, "data.deactivated");
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            AutocompleteResponse res = constructorio.Autocomplete.GetAutocompleteResults(req);
            res.Request.TryGetValue("variations_map", out object reqVariationsMap);

            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.NotNull(reqVariationsMap, "Variations Map was passed as parameter");
        }

        [Test]
        public void GetAutocompleteResultsShouldReturnResultWithSearchSuggestionOnly()
        {
            AutocompleteRequest req = new AutocompleteRequest("jacket");
            req.ResultsPerSection = new Dictionary<string, int> { { "Search Suggestions", 10 } };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            AutocompleteResponse res = constructorio.Autocomplete.GetAutocompleteResults(req);

            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.AreEqual(res.Sections["Products"].Count, 0, "Products don't exist");
            Assert.GreaterOrEqual(res.Sections["Search Suggestions"].Count, 4, "Search Suggestsions Exist");
        }

        [Test]
        public void GetAutocompleteResultsShouldReturnResultWithHiddenFields()
        {
            AutocompleteRequest req = new AutocompleteRequest("item1");
            req.HiddenFields = new List<string> { "testField" };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            AutocompleteResponse res = constructorio.Autocomplete.GetAutocompleteResults(req);

            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.GreaterOrEqual(res.Sections["Products"].Count, 5, "Results exist");
        }

        [Test]
        public void GetAutocompleteResultsShouldReturnResultWithMultipleFilters()
        {
            AutocompleteRequest req = new AutocompleteRequest("item");
            req.Filters = new Dictionary<string, List<string>>();
            req.Filters.Add("group_id", new List<string> { "All" });
            req.Filters.Add("Brand", new List<string> { "XYZ" });
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            AutocompleteResponse res = constructorio.Autocomplete.GetAutocompleteResults(req);

            Assert.NotNull(res.ResultId, "Result id exists");
            Assert.GreaterOrEqual(res.Sections["Products"].Count, 1, "Results exist");
        }
    }
}