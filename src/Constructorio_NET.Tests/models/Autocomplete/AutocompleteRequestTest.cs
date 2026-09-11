using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class AutocompleteRequestTest
    {
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private readonly string Query = "item";
        private readonly Dictionary<string, List<string>> Filters = new Dictionary<string, List<string>>()
        {
            { "Color", new List<string>() { "green", "blue" } }
        };
        private readonly Dictionary<string, Dictionary<string, List<string>>> FiltersPerSection = new Dictionary<string, Dictionary<string, List<string>>>()
        {
            { "Products", new Dictionary<string, List<string>>() { { "Color", new List<string>() { "green", "blue" } } } }
        };
        private readonly string UserId = "user1";
        private readonly List<string> UserSegments = new List<string>() { "us", "desktop" };
        private readonly Dictionary<string, string> TestCells = new Dictionary<string, string>()
        {
            { "test1", "original" },
        };
        private readonly string IP = "1,2,3";
        private readonly string OS = "Mac";
        private UserInfo UserInfo;

        [OneTimeSetUp]
        public void Setup()
        {
            this.UserInfo = new UserInfo(ClientId, SessionId);
            this.UserInfo.SetUserId(this.UserId);
            this.UserInfo.SetUserSegments(this.UserSegments);
            this.UserInfo.SetUserAgent(this.OS);
            this.UserInfo.SetForwardedFor(this.IP);
        }

        [Test]
        public void GetRequestParameters()
        {
            AutocompleteRequest req = new AutocompleteRequest(this.Query)
            {
                UserInfo = this.UserInfo,
                Filters = this.Filters,
                TestCells = this.TestCells,
                FiltersPerSection = this.FiltersPerSection,
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.ClientId, requestParameters[Constants.CLIENT_ID]);
            Assert.AreEqual(this.SessionId, requestParameters[Constants.SESSION_ID]);
            Assert.AreEqual(this.UserId, requestParameters[Constants.USER_ID]);
            Assert.AreEqual(this.UserSegments, requestParameters[Constants.USER_SEGMENTS]);
            Assert.AreEqual(this.Filters, requestParameters[Constants.FILTERS]);
            Assert.AreEqual(this.TestCells, requestParameters[Constants.TEST_CELLS]);
            Assert.AreEqual(this.FiltersPerSection, requestParameters[Constants.FILTERS_PER_SECTION]);
        }

        [Test]
        public void GetRequestParametersWithPreFilterExpression()
        {
            ValuePreFilterExpression filterByBrand = new ValuePreFilterExpression("Brand", "XYZ");
            AutocompleteRequest req = new AutocompleteRequest(this.Query)
            {
                PreFilterExpression = filterByBrand,
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.IsNotNull(requestParameters[Constants.PRE_FILTER_EXPRESSION]);
            Assert.IsTrue(requestParameters[Constants.PRE_FILTER_EXPRESSION].ToString().Contains("Brand"));
            Assert.IsTrue(requestParameters[Constants.PRE_FILTER_EXPRESSION].ToString().Contains("XYZ"));
        }

        [Test]
        public void GetRequestParametersWithPreFilterExpressionPerSection()
        {
            ValuePreFilterExpression filterProducts = new ValuePreFilterExpression("Brand", "XYZ");
            ValuePreFilterExpression filterSuggestions = new ValuePreFilterExpression("group_id", "All");
            AutocompleteRequest req = new AutocompleteRequest(this.Query)
            {
                PreFilterExpressionPerSection = new Dictionary<string, PreFilterExpression>
                {
                    { "Products", filterProducts },
                    { "Search Suggestions", filterSuggestions },
                },
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Dictionary<string, PreFilterExpression> perSection = (Dictionary<string, PreFilterExpression>)requestParameters[Constants.PRE_FILTER_EXPRESSION_PER_SECTION];
            Assert.IsNotNull(perSection);
            Assert.AreEqual(2, perSection.Count);
            Assert.IsTrue(perSection.ContainsKey("Products"));
            Assert.IsTrue(perSection.ContainsKey("Search Suggestions"));
        }

        [Test]
        public void PreFilterExpressionPerSectionThrowsOnDuplicateKeyInInitializer()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            {
                AutocompleteRequest req = new AutocompleteRequest(this.Query)
                {
                    PreFilterExpressionPerSection = new Dictionary<string, PreFilterExpression>
                    {
                        { "Products", new ValuePreFilterExpression("Brand", "XYZ") },
                        { "Products", new ValuePreFilterExpression("group_id", "All") },
                    },
                };
            });
            Assert.That(ex.Message, Does.Contain("same key has already been added"));
        }

        [Test]
        public void GetRequestParametersThrowsWhenPreFilterExpressionAndPerSectionBothSet()
        {
            AutocompleteRequest req = new AutocompleteRequest(this.Query)
            {
                PreFilterExpression = new ValuePreFilterExpression("Brand", "XYZ"),
                PreFilterExpressionPerSection = new Dictionary<string, PreFilterExpression>
                {
                    { "Products", new ValuePreFilterExpression("group_id", "All") },
                },
            };

            ArgumentException ex = Assert.Throws<ArgumentException>(() => req.GetRequestParameters());
            Assert.That(ex.Message, Does.Contain("mutually exclusive"));
        }

        [Test]
        public void GetRequestParametersDoesNotThrowWhenPerSectionIsEmpty()
        {
            ValuePreFilterExpression filterByBrand = new ValuePreFilterExpression("Brand", "XYZ");
            AutocompleteRequest req = new AutocompleteRequest(this.Query)
            {
                PreFilterExpression = filterByBrand,
                PreFilterExpressionPerSection = new Dictionary<string, PreFilterExpression>(),
            };

            Hashtable requestParameters = null;
            Assert.DoesNotThrow(() => requestParameters = req.GetRequestParameters());
            Assert.IsNotNull(requestParameters[Constants.PRE_FILTER_EXPRESSION]);
            Assert.IsFalse(requestParameters.ContainsKey(Constants.PRE_FILTER_EXPRESSION_PER_SECTION), "an empty per-section dictionary must not be added to the parameters at all");
        }

        [TestCase("")]
        [TestCase("   ")]
        public void GetRequestParametersThrowsWhenPreFilterExpressionPerSectionKeyIsBlank(string blankSection)
        {
            AutocompleteRequest req = new AutocompleteRequest(this.Query)
            {
                PreFilterExpressionPerSection = new Dictionary<string, PreFilterExpression>
                {
                    { blankSection, new ValuePreFilterExpression("group_id", "All") },
                },
            };

            ArgumentException ex = Assert.Throws<ArgumentException>(() => req.GetRequestParameters());
            Assert.That(ex.Message, Does.Contain("must not be null, empty, or whitespace"));
        }

        [Test]
        public void GetRequestParametersThrowsWhenOneOfSeveralSectionsIsBlank()
        {
            AutocompleteRequest req = new AutocompleteRequest(this.Query)
            {
                PreFilterExpressionPerSection = new Dictionary<string, PreFilterExpression>
                {
                    { "Products", new ValuePreFilterExpression("Brand", "XYZ") },
                    { "   ", new ValuePreFilterExpression("group_id", "All") },
                    { "Search Suggestions", new ValuePreFilterExpression("Brand", "XYZ") },
                },
            };

            ArgumentException ex = Assert.Throws<ArgumentException>(() => req.GetRequestParameters());
            Assert.That(ex.Message, Does.Contain("must not be null, empty, or whitespace"));
        }

        [Test]
        public void GetRequestParametersWithoutPreFilterExpression()
        {
            AutocompleteRequest req = new AutocompleteRequest(this.Query);

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.IsFalse(requestParameters.ContainsKey(Constants.PRE_FILTER_EXPRESSION));
            Assert.IsFalse(requestParameters.ContainsKey(Constants.PRE_FILTER_EXPRESSION_PER_SECTION));
        }

        [Test]
        public void GetRequestHeaders()
        {
            AutocompleteRequest req = new AutocompleteRequest(this.Query)
            {
                UserInfo = this.UserInfo,
            };

            Dictionary<string, string> requestHeaders = req.GetRequestHeaders();
            Assert.AreEqual(this.OS, requestHeaders[Constants.USER_AGENT]);
            Assert.AreEqual(this.IP, requestHeaders[Constants.USER_IP]);
        }

        [Test]
        public void AutocompleteRequestWithInvalidQuery()
        {
            Assert.Throws<ArgumentException>(() => new AutocompleteRequest(null));
        }
    }
}
