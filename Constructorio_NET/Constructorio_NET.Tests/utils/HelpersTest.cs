namespace Constructorio_NET.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    [TestFixture]
    public class HelpersTest : Helpers
    {
        private string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private string ClientId = "r4nd-cl1ent-1d";
        private int SessionId = 4;
        private Hashtable Options;
        private string Query = "item";
        private string ServiceUrl = "https://ac.cnstrc.com";
        private string Version = "cionet-5.6.0";

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

            Hashtable cleanedParams = Helpers.CleanParams(parameters);
            Assert.AreEqual(expectedParameters["userId"], cleanedParams["userId"], "parameters should be cleaned");
        }

        [Test]
        public void OurEscapeDataString()
        {
            string str = "boink doink yoink";
            string expectedString = "boink%20doink%20yoink";
            string encodedString = Helpers.OurEscapeDataString(str);
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

            string url = Helpers.MakeUrl(this.Options, paths, queryParams);
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

            string url = Helpers.MakeUrl(this.Options, paths, queryParams);
            string expectedUrl = $@"https:\/\/ac.cnstrc.com\/search\/{this.Query}\?key={this.ApiKey}&c={this.Version}&filters%5BColor%5D=green&filters%5BColor%5D=blue&_dt=";
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

            string url = Helpers.MakeUrl(this.Options, paths, queryParams);
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
        { Constants.SEGMENTS, segments }
      };

            string url = Helpers.MakeUrl(this.Options, paths, queryParams);
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

            string url = Helpers.MakeUrl(this.Options, paths, queryParams);
            string expectedUrl = $@"https:\/\/ac.cnstrc.com\/search\/{this.Query}\?key={this.ApiKey}&c={this.Version}&fmt_options%5Bhidden_fields%5D=inventory&fmt_options%5Bhidden_fields%5D=margin&_dt=";
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

            string url = Helpers.MakeUrl(this.Options, paths, queryParams);
            string expectedUrl1 = "&fmt_options%5Bgroups_start%5D=current";
            string expectedUrl2 = "&fmt_options%5Bgroups_max_depth%5D=3";
            bool regexMatched1 = Regex.Match(url, expectedUrl1).Success;
            bool regexMatched2 = Regex.Match(url, expectedUrl2).Success;
            Assert.That(regexMatched1 && regexMatched2, "url should be properly formed");
        }
    }
}
