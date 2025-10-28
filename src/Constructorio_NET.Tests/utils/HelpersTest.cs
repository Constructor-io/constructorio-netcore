using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using Newtonsoft.Json;
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
        public async Task TestCreateRequest()
        {
            Hashtable requestBody = new Hashtable
            {
                {
                    "searchabilities", new List<Searchability>
                    {
                        new("testSearchability") { ExactSearchable = true, Hidden = false, FuzzySearchable = false, Displayable = true },
                        new("testSearchability2") { Hidden = true }
                    }
                }
            };

            // Old way of doing it
            // This allocates a StringBuilder to serialize the JSON into, and then allocates & builds the UTF16 string from that
            string serializeObjectUtf16String = JsonConvert.SerializeObject(requestBody);

            // StringContent immediately converts the UTF16 string chars to UTF8 bytes and allocates an array for them
            StringContent stringContent = new StringContent(serializeObjectUtf16String, Encoding.UTF8, "application/json");

            // new way of doing it, serializes JSON bytes directly to MemoryStream
            // A MemoryStream has to be allocated, as we need to know the length for the Content-Length header
            // Otherwise we could just write it directly to the request stream
            using NewtonsoftJsonUtf8Content newtonsoftJsonUtf8Content = new NewtonsoftJsonUtf8Content(requestBody);

            string actualString = await newtonsoftJsonUtf8Content.ReadAsStringAsync();
            string expectedString = await stringContent.ReadAsStringAsync();
            Assert.That(actualString, Is.EqualTo(expectedString));

            byte[] actualBytes = await newtonsoftJsonUtf8Content.ReadAsByteArrayAsync();
            byte[] expectedBytes = await stringContent.ReadAsByteArrayAsync();
            Assert.That(actualBytes.Length, Is.EqualTo(expectedBytes.Length));
            Assert.That(actualBytes, Is.EqualTo(expectedBytes));
        }

        [Test]
        public async Task TestNewtonsoftJsonUtf8ContentSerialization()
        {
            // Test that NewtonsoftJsonUtf8Content properly serializes various object types
            var complexObject = new Hashtable
            {
                { "string_field", "test value" },
                { "number_field", 42 },
                { "boolean_field", true },
                { "array_field", new List<string> { "item1", "item2", "item3" } },
                {
                    "nested_object", new Hashtable
                    {
                        { "nested_string", "nested value" },
                        { "nested_number", 123 }
                    }
                }
            };

            using NewtonsoftJsonUtf8Content content = new NewtonsoftJsonUtf8Content(complexObject);
            string serializedJson = await content.ReadAsStringAsync();

            // Verify the JSON can be deserialized back
            var deserialized = JsonConvert.DeserializeObject<Hashtable>(serializedJson);

            Assert.That(deserialized, Is.Not.Null, "Deserialized object should not be null");
            Assert.That(deserialized["string_field"], Is.EqualTo("test value"));
            Assert.That(deserialized["number_field"], Is.EqualTo(42L)); // JSON numbers deserialize as long
            Assert.That(deserialized["boolean_field"], Is.EqualTo(true));

            // Verify array_field
            var arrayField = deserialized["array_field"] as Newtonsoft.Json.Linq.JArray;
            Assert.That(arrayField, Is.Not.Null, "array_field should not be null");
            Assert.That(arrayField.Count, Is.EqualTo(3), "array_field should have 3 items");
            Assert.That(arrayField[0].ToString(), Is.EqualTo("item1"));
            Assert.That(arrayField[1].ToString(), Is.EqualTo("item2"));
            Assert.That(arrayField[2].ToString(), Is.EqualTo("item3"));

            // Verify nested_object
            var nestedObject = deserialized["nested_object"] as Newtonsoft.Json.Linq.JObject;
            Assert.That(nestedObject, Is.Not.Null, "nested_object should not be null");
            Assert.That(nestedObject["nested_string"].ToString(), Is.EqualTo("nested value"));
            Assert.That(nestedObject["nested_number"].ToObject<long>(), Is.EqualTo(123L));
        }

        [Test]
        public async Task TestNewtonsoftJsonUtf8ContentDispose()
        {
            // Test that NewtonsoftJsonUtf8Content properly disposes resources
            NewtonsoftJsonUtf8Content content = new NewtonsoftJsonUtf8Content(new Hashtable { { "test", "value" } });

            // Read content before disposal
            string result = await content.ReadAsStringAsync();
            Assert.That(result, Is.Not.Null.And.Not.Empty);

            // Dispose the content
            content.Dispose();

            // Attempting to read after disposal should throw
            Assert.ThrowsAsync<ObjectDisposedException>(async () => await content.ReadAsStringAsync());
        }

        [Test]
        public void TestNewtonsoftJsonUtf8ContentComputesLength()
        {
            // Test that TryComputeLength returns the correct content length
            var testData = new Hashtable { { "key", "value" } };
            using NewtonsoftJsonUtf8Content content = new NewtonsoftJsonUtf8Content(testData);

            // The content should be able to compute its length
            bool canComputeLength = content.Headers.ContentLength.HasValue;
            Assert.That(canComputeLength, Is.True, "Content should have a computable length");
            Assert.That(content.Headers.ContentLength.Value, Is.GreaterThan(0), "Content length should be greater than 0");
        }
    }
}
