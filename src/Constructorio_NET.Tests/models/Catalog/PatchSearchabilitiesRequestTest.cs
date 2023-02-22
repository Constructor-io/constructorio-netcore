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
    public class PatchSearchabilitiesRequestTest
    {
        private readonly string Section = "Search Suggestions";
        private readonly List<Searchability> Searchabilities = new List<Searchability>();

        [OneTimeSetUp]
        public void Setup()
        {
            this.Searchabilities.Add(new Searchability("testSearchability") { ExactSearchable = true });
        }

        [Test]
        public void GetRequestParameters()
        {
            PatchSearchabilitiesRequest req = new PatchSearchabilitiesRequest(Searchabilities)
            {
                Section = this.Section,
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.Section, requestParameters[Constants.SECTION]);
        }
    }
}
