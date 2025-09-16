using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class RetrieveSearchabilitiesRequestTest
    {
        private readonly Dictionary<string, string> Filters = new Dictionary<string, string>()
        {
            { "exact_searchable", "true" }
        };
        private readonly int Page = 2;
        private readonly string Section = "Search Suggestions";
        private readonly string SortBy = "Price";
        private readonly string SortOrder = "Ascending";
        private readonly bool Searchable = true;
        private readonly int Offset = 10;
        private readonly int NumResultsPerPage = 10;
        private readonly string Name = "groups";

        [Test]
        public void GetRequestParameters()
        {
            RetrieveSearchabilitiesRequest req = new RetrieveSearchabilitiesRequest()
            {
                Page = this.Page,
                Section = this.Section,
                SortBy = this.SortBy,
                SortOrder = SortOrder,
                Filters = this.Filters,
                Name = this.Name,
                Offset = this.Offset,
                NumResultsPerPage = this.NumResultsPerPage,
                Searchable = this.Searchable
            };

            Dictionary<string, List<string>> formattedFilters = new Dictionary<string, List<string>>();
            foreach (var filter in this.Filters)
            {
                formattedFilters.Add(filter.Key, new List<string> { filter.Value });
            }

            formattedFilters.Add("name", new List<string> { this.Name });

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.Searchable, requestParameters[Constants.SEARCHABLE]);
            Assert.AreEqual(formattedFilters, requestParameters[Constants.FILTERS]);
            Assert.AreEqual(this.Offset, requestParameters[Constants.OFFSET]);
            Assert.AreEqual(this.NumResultsPerPage, requestParameters[Constants.RESULTS_PER_PAGE]);
            Assert.AreEqual(this.Page, requestParameters[Constants.PAGE]);
            Assert.AreEqual(this.Section, requestParameters[Constants.SECTION]);
            Assert.AreEqual(this.SortBy, requestParameters[Constants.SORT_BY]);
            Assert.AreEqual(this.SortOrder, requestParameters[Constants.SORT_ORDER]);
        }
    }
}
