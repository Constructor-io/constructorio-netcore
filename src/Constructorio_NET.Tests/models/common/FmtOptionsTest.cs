using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class FmtOptionsTest
    {
        [Test]
        public void GetQueryParametersEmpty()
        {
            FmtOptions fmtOptions = new FmtOptions();
            Hashtable result = fmtOptions.GetQueryParameters();
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetQueryParametersGroupsMaxDepth()
        {
            FmtOptions fmtOptions = new FmtOptions { GroupsMaxDepth = 3 };
            Hashtable result = fmtOptions.GetQueryParameters();
            Assert.AreEqual("3", result[$"{Constants.FMT_OPTIONS}[{Constants.GROUPS_MAX_DEPTH}]"]);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetQueryParametersGroupsStart()
        {
            FmtOptions fmtOptions = new FmtOptions { GroupsStart = "current" };
            Hashtable result = fmtOptions.GetQueryParameters();
            Assert.AreEqual("current", result[$"{Constants.FMT_OPTIONS}[{Constants.GROUPS_START}]"]);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetQueryParametersShowHiddenFields()
        {
            FmtOptions fmtOptions = new FmtOptions { ShowHiddenFields = true };
            Hashtable result = fmtOptions.GetQueryParameters();
            Assert.AreEqual("true", result[$"{Constants.FMT_OPTIONS}[{Constants.SHOW_HIDDEN_FIELDS}]"]);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetQueryParametersShowHiddenFieldsFalse()
        {
            FmtOptions fmtOptions = new FmtOptions { ShowHiddenFields = false };
            Hashtable result = fmtOptions.GetQueryParameters();
            Assert.AreEqual("false", result[$"{Constants.FMT_OPTIONS}[{Constants.SHOW_HIDDEN_FIELDS}]"]);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetQueryParametersVariationsReturnType()
        {
            FmtOptions fmtOptions = new FmtOptions { VariationsReturnType = "all" };
            Hashtable result = fmtOptions.GetQueryParameters();
            Assert.AreEqual("all", result[$"{Constants.FMT_OPTIONS}[{Constants.VARIATIONS_RETURN_TYPE}]"]);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetQueryParametersShowHiddenFacets()
        {
            FmtOptions fmtOptions = new FmtOptions { ShowHiddenFacets = true };
            Hashtable result = fmtOptions.GetQueryParameters();
            Assert.AreEqual("true", result[$"{Constants.FMT_OPTIONS}[{Constants.SHOW_HIDDEN_FACETS}]"]);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetQueryParametersShowProtectedFacets()
        {
            FmtOptions fmtOptions = new FmtOptions { ShowProtectedFacets = true };
            Hashtable result = fmtOptions.GetQueryParameters();
            Assert.AreEqual("true", result[$"{Constants.FMT_OPTIONS}[{Constants.SHOW_PROTECTED_FACETS}]"]);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetQueryParametersFields()
        {
            FmtOptions fmtOptions = new FmtOptions { Fields = new List<string> { "id", "name" } };
            Hashtable result = fmtOptions.GetQueryParameters();
            List<string> fields = (List<string>)result[$"{Constants.FMT_OPTIONS}[{Constants.FIELDS}]"];
            Assert.AreEqual(new List<string> { "id", "name" }, fields);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetQueryParametersHiddenFields()
        {
            FmtOptions fmtOptions = new FmtOptions { HiddenFields = new List<string> { "inventory", "margin" } };
            Hashtable result = fmtOptions.GetQueryParameters();
            List<string> hiddenFields = (List<string>)result[$"{Constants.FMT_OPTIONS}[{Constants.HIDDEN_FIELDS}]"];
            Assert.AreEqual(new List<string> { "inventory", "margin" }, hiddenFields);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetQueryParametersHiddenFacets()
        {
            FmtOptions fmtOptions = new FmtOptions { HiddenFacets = new List<string> { "brand", "category" } };
            Hashtable result = fmtOptions.GetQueryParameters();
            List<string> hiddenFacets = (List<string>)result[$"{Constants.FMT_OPTIONS}[{Constants.HIDDEN_FACETS}]"];
            Assert.AreEqual(new List<string> { "brand", "category" }, hiddenFacets);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetQueryParametersAllProperties()
        {
            FmtOptions fmtOptions = new FmtOptions
            {
                GroupsMaxDepth = 5,
                GroupsStart = "top",
                ShowHiddenFields = true,
                VariationsReturnType = "all",
                ShowHiddenFacets = true,
                ShowProtectedFacets = false,
                Fields = new List<string> { "id" },
                HiddenFields = new List<string> { "inventory" },
                HiddenFacets = new List<string> { "brand" },
            };

            Hashtable result = fmtOptions.GetQueryParameters();
            Assert.AreEqual(9, result.Count);
            Assert.AreEqual("5", result[$"{Constants.FMT_OPTIONS}[{Constants.GROUPS_MAX_DEPTH}]"]);
            Assert.AreEqual("top", result[$"{Constants.FMT_OPTIONS}[{Constants.GROUPS_START}]"]);
            Assert.AreEqual("true", result[$"{Constants.FMT_OPTIONS}[{Constants.SHOW_HIDDEN_FIELDS}]"]);
            Assert.AreEqual("all", result[$"{Constants.FMT_OPTIONS}[{Constants.VARIATIONS_RETURN_TYPE}]"]);
            Assert.AreEqual("true", result[$"{Constants.FMT_OPTIONS}[{Constants.SHOW_HIDDEN_FACETS}]"]);
            Assert.AreEqual("false", result[$"{Constants.FMT_OPTIONS}[{Constants.SHOW_PROTECTED_FACETS}]"]);
            Assert.AreEqual(new List<string> { "id" }, result[$"{Constants.FMT_OPTIONS}[{Constants.FIELDS}]"]);
            Assert.AreEqual(new List<string> { "inventory" }, result[$"{Constants.FMT_OPTIONS}[{Constants.HIDDEN_FIELDS}]"]);
            Assert.AreEqual(new List<string> { "brand" }, result[$"{Constants.FMT_OPTIONS}[{Constants.HIDDEN_FACETS}]"]);
        }
    }
}
