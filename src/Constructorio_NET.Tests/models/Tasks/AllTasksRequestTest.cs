using System;
using System.Collections;
using Constructorio_NET.Models.Tasks;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class AllTasksRequestTest
    {
        private readonly int Page = 2;
        private readonly int ResultsPerPage = 20;

        [Test]
        public void AllTasksRequestSetsPageAndResultsPerPage()
        {
            AllTasksRequest req = new AllTasksRequest(this.ResultsPerPage, this.Page);

            Assert.AreEqual(this.ResultsPerPage, req.ResultsPerPage);
            Assert.AreEqual(this.Page, req.Page);
        }

        [Test]
        public void GetRequestParameters()
        {
            AllTasksRequest req = new AllTasksRequest()
            {
                Page = this.Page,
                ResultsPerPage = this.ResultsPerPage,
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.ResultsPerPage, requestParameters[Constants.RESULTS_PER_PAGE]);
            Assert.AreEqual(this.Page, requestParameters[Constants.PAGE]);
        }
    }
}
