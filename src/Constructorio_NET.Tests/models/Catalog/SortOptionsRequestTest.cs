using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class SortOptionsTest
    {
        private readonly string Section = "Search Suggestions";

        [Test]
        public void GetRequestParameters()
        {
            SortOptionsRequest req = new SortOptionsRequest()
            {
                Section = this.Section
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.Section, requestParameters[Constants.SECTION]);
        }

        [Test]
        public void GetRequestParametersWithNoDefaultValues()
        {
            SortOptionsRequest req = new SortOptionsRequest();

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual("Products", requestParameters[Constants.SECTION]);
            Assert.AreEqual(null, requestParameters[Constants.SORT_OPTION]);
        }

        [Test]
        public void SortOptionsShouldSerialize()
        {
            SortOption sortOption = new SortOption("test", SortOrderType.Ascending);
            SortOptionsListRequest req = new SortOptionsListRequest(new List<SortOption>() { sortOption })
            {
                Section = this.Section
            };
            Assert.AreEqual(
                $"{{\"sort_options\":[{{\"sort_by\":\"test\",\"sort_order\":\"ascending\"}}]}}",
                JsonConvert.SerializeObject(req.GetSortOptionsList())
            );
        }
    }
}
