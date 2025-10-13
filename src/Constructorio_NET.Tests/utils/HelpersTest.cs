using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class HelpersTest : Helpers
    {
        private readonly string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private readonly string Query = "item";
        private readonly string ServiceUrl = "https://ac.cnstrc.com";
        private readonly string Version = "cionet-5.6.0";
        private Hashtable Options;

        [SetUp]
        public void Setup()
        {
            this.Options = new Hashtable()
            {
                { Constants.SERVICE_URL, this.ServiceUrl },
                { Constants.API_KEY, this.ApiKey },
                { Constants.VERSION, this.Version }
            };
        }

        [Test]
        public void CleanParams()
        {
            Hashtable filters = new Hashtable()
            {
                { "size", "large" },
                { "color", "green" },
            };
            Hashtable parameters = new Hashtable()
            {
                { "filters", filters },
                { "userId", "boink doink yoink" }, // contains non-breaking spaces
                { "section", "Products" },
            };
            Hashtable expectedParameters = new Hashtable()
            {
                { "filters", filters },
                { "userId", "boink doink yoink" }, // contains non-breaking spaces
                { "section", "Products" },
            };

            Hashtable cleanedParams = CleanParams(parameters);
            Assert.AreEqual(expectedParameters["userId"], cleanedParams["userId"], "parameters should be cleaned");
        }

        [Test]
        public void OurEscapeDataString()
        {
            string str = "boink doink yoink";
            string expectedString = "boink%20doink%20yoink";
            string encodedString = OurEscapeDataString(str);
            Assert.AreEqual(expectedString, encodedString, "should encode non breaking space");
        }

        [Test]
        public void MakeUrlSearch()
        {
            List<string> paths = new List<string> { "search", this.Query };
            Hashtable queryParams = new Hashtable()
            {
                { Constants.CLIENT_ID, ClientId },
                { Constants.SESSION_ID, SessionId },
                { Constants.SECTION, "Search Suggestions" },
            };

            string url = MakeUrl(this.Options, paths, queryParams);
            string expectedUrl = $@"https:\/\/ac.cnstrc.com\/search\/{this.Query}\?key={this.ApiKey}&c={this.Version}&i={this.ClientId}&s={this.SessionId}&section=Search%20Suggestions&_dt=";
            bool regexMatched = Regex.Match(url, expectedUrl).Success;
            Assert.That(regexMatched, "url should be properly formed");
        }

        [Test]
        public void MakeUrlSearchWithFilters()
        {
            List<string> paths = new List<string> { "search", this.Query };
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>()
            {
                { "Color", new List<string>() { "green", "blue" } }
            };
            Hashtable queryParams = new Hashtable()
            {
                { Constants.FILTERS, filters }
            };

            string url = MakeUrl(this.Options, paths, queryParams);
            string expectedUrl = $@"https:\/\/ac.cnstrc.com\/search\/{this.Query}\?key={this.ApiKey}&c={this.Version}&filters%5BColor%5D=green&filters%5BColor%5D=blue&_dt=";
            bool regexMatched = Regex.Match(url, expectedUrl).Success;
            Assert.That(regexMatched, "url should be properly formed");
        }

        [Test]
        public void MakeUrlShouldEncodeSpaceProperly()
        {
            List<string> paths = new List<string> { "search space", this.Query };
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>()
            {
                { "Color", new List<string>() { "green shirt", "blue" } }
            };
            Hashtable queryParams = new Hashtable()
            {
                { Constants.FILTERS, filters }
            };

            string url = MakeUrl(this.Options, paths, queryParams);
            string expectedUrl = $@"https:\/\/ac.cnstrc.com\/search%20space\/{this.Query}\?key={this.ApiKey}&c={this.Version}&filters%5BColor%5D=green%20shirt&filters%5BColor%5D=blue&_dt=";
            bool regexMatched = Regex.Match(url, expectedUrl).Success;
            Assert.That(regexMatched, "url should be properly formed");
        }

        [Test]
        public void MakeUrlSearchWithTestCells()
        {
            List<string> paths = new List<string> { "search", this.Query };
            Dictionary<string, string> testCells = new Dictionary<string, string>()
            {
                { "test1", "testCellA" },
                { "test2", "testCellB" }
            };
            Hashtable queryParams = new Hashtable()
            {
                { Constants.TEST_CELLS, testCells }
            };

            string url = MakeUrl(this.Options, paths, queryParams);
            string expectedUrl = $@"https:\/\/ac.cnstrc.com\/search\/{this.Query}\?key={this.ApiKey}&c={this.Version}&ef-test1=testCellA&ef-test2=testCellB&_dt=";
            bool regexMatched = Regex.Match(url, expectedUrl).Success;
            Assert.That(regexMatched, "url should be properly formed");
        }

        [Test]
        public void MakeUrlSearchWithSegments()
        {
            List<string> paths = new List<string> { "search", this.Query };
            List<string> segments = new List<string>() { "mobile-web", "under-30" };
            Hashtable queryParams = new Hashtable()
            {
                { Constants.USER_SEGMENTS, segments }
            };

            string url = MakeUrl(this.Options, paths, queryParams);
            string expectedUrl = "&us=mobile-web&us=under-30";
            bool regexMatched = Regex.Match(url, expectedUrl).Success;
            Assert.That(regexMatched, "url should be properly formed");
        }

        [Test]
        public void MakeUrlSearchWithHiddenFields()
        {
            List<string> paths = new List<string> { "search", this.Query };
            List<string> hiddenFields = new List<string>() { "inventory", "margin" };
            Hashtable queryParams = new Hashtable()
            {
                { Constants.HIDDEN_FIELDS, hiddenFields }
            };

            string url = MakeUrl(this.Options, paths, queryParams);
            string expectedUrl = $@"https:\/\/ac.cnstrc.com\/search\/{this.Query}\?key={this.ApiKey}&c={this.Version}&fmt_options%5Bhidden_fields%5D=inventory&fmt_options%5Bhidden_fields%5D=margin&_dt=";
            bool regexMatched = Regex.Match(url, expectedUrl).Success;
            Assert.That(regexMatched, "url should be properly formed");
        }

        [Test]
        public void MakeUrlSearchWithHiddenFacets()
        {
            List<string> paths = new List<string> { "search", this.Query };
            List<string> hiddenFacets = new List<string>() { "inventory", "margin" };
            Hashtable queryParams = new Hashtable()
            {
                { Constants.HIDDEN_FACETS, hiddenFacets }
            };

            string url = MakeUrl(this.Options, paths, queryParams);
            string expectedUrl = $@"https:\/\/ac.cnstrc.com\/search\/{this.Query}\?key={this.ApiKey}&c={this.Version}&fmt_options%5Bhidden_facets%5D=inventory&fmt_options%5Bhidden_facets%5D=margin&_dt=";
            bool regexMatched = Regex.Match(url, expectedUrl).Success;
            Assert.That(regexMatched, "url should be properly formed");
        }

        [Test]
        public void MakeUrlSearchWithFmtOptions()
        {
            List<string> paths = new List<string> { "search", this.Query };
            Dictionary<string, string> fmtOptions = new Dictionary<string, string>()
            {
                { "groups_max_depth", "3" },
                { "groups_start", "current" }
            };
            Hashtable queryParams = new Hashtable()
            {
                { Constants.SECTION, "Search Suggestions" },
                { Constants.FMT_OPTIONS, fmtOptions }
            };

            string url = MakeUrl(this.Options, paths, queryParams);
            string expectedUrl1 = "&fmt_options%5Bgroups_start%5D=current";
            string expectedUrl2 = "&fmt_options%5Bgroups_max_depth%5D=3";
            bool regexMatched1 = Regex.Match(url, expectedUrl1).Success;
            bool regexMatched2 = Regex.Match(url, expectedUrl2).Success;
            Assert.That(regexMatched1 && regexMatched2, "url should be properly formed");
        }

        [Test]
        public void MakeUrlSearchWithFiltersPerSection()
        {
            List<string> paths = new List<string> { "search", this.Query };
            Dictionary<string, Dictionary<string, List<string>>> filtersPerSection = new Dictionary<string, Dictionary<string, List<string>>>
            {
                { "Products", new Dictionary<string, List<string>>() { { "Color", new List<string>() { "blue", "green" } } } }
            };
            Hashtable queryParams = new Hashtable()
            {
                { Constants.FILTERS_PER_SECTION, filtersPerSection },
            };

            string url = MakeUrl(this.Options, paths, queryParams);
            string filterParameterBlue = "&filters%5BProducts%5D%5BColor%5D=blue";
            string filterParameterGreen = "&filters%5BProducts%5D%5BColor%5D=green";
            bool hasColorBlueFilter = Regex.Match(url, filterParameterBlue).Success;
            bool hasColorGreenFilter = Regex.Match(url, filterParameterGreen).Success;
            Assert.That(hasColorBlueFilter && hasColorGreenFilter, "url is properly formed and has all filters applied");
        }

        [Test]
        public void MakeUrlSearchWithFiltersContainingSpecialCharacters()
        {
            List<string> paths = new List<string> { "search", this.Query };
            Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>()
            {
                { "Size & Fit", new List<string>() { "Small (S)", "Large (L)" } }
            };
            Hashtable queryParams = new Hashtable()
            {
                { Constants.FILTERS, filters }
            };

            string url = MakeUrl(this.Options, paths, queryParams);
            string expectedFilterGroup = "filters%5BSize%20%26%20Fit%5D";
            string expectedValue1 = "Small%20%28S%29";
            string expectedValue2 = "Large%20%28L%29";
            bool hasFilterGroup = Regex.Match(url, expectedFilterGroup).Success;
            bool hasValue1 = Regex.Match(url, expectedValue1).Success;
            bool hasValue2 = Regex.Match(url, expectedValue2).Success;
            Assert.That(hasFilterGroup && hasValue1 && hasValue2, "url should properly encode special characters in filters");
        }

        [Test]
        public void MakeUrlSearchWithIntegerParameter()
        {
            List<string> paths = new List<string> { "search", this.Query };
            Hashtable queryParams = new Hashtable()
            {
                { Constants.PAGE, 2 },
                { Constants.OFFSET, 10 }
            };

            string url = MakeUrl(this.Options, paths, queryParams);
            bool hasPage = Regex.Match(url, "&page=2").Success;
            bool hasOffset = Regex.Match(url, "&offset=10").Success;
            Assert.That(hasPage && hasOffset, "url should properly handle integer parameters");
        }

        [Test]
        public void MakeUrlSearchWithBooleanParameter()
        {
            List<string> paths = new List<string> { "search", this.Query };
            Hashtable queryParams = new Hashtable()
            {
                { Constants.SHOW_HIDDEN_FACETS, true },
                { Constants.SHOW_PROTECTED_FACETS, false }
            };

            string url = MakeUrl(this.Options, paths, queryParams);
            bool hasShowHidden = Regex.Match(url, "&show_hidden_facets=True").Success;
            bool hasShowProtected = Regex.Match(url, "&show_protected_facets=False").Success;
            Assert.That(hasShowHidden && hasShowProtected, "url should properly handle boolean parameters");
        }

        [Test]
        public void MakeUrlSearchWithAnswers()
        {
            List<string> paths = new List<string> { "search", this.Query };
            List<List<string>> answers = new List<List<string>>()
            {
                new List<string>() { "1", "2" },
                new List<string>() { "3" }
            };
            Hashtable queryParams = new Hashtable()
            {
                { Constants.ANSWERS, answers }
            };

            string url = MakeUrl(this.Options, paths, queryParams);
            string expectedAnswer1 = "&a=1%2C2";
            string expectedAnswer2 = "&a=3";
            bool hasAnswer1 = Regex.Match(url, expectedAnswer1).Success;
            bool hasAnswer2 = Regex.Match(url, expectedAnswer2).Success;
            Assert.That(hasAnswer1 && hasAnswer2, "url should properly encode quiz answers");
        }
    }
}
