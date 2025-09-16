using System;
using System.Collections;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class AllTasksRequestTest
    {
        private readonly int Page = 2;
        private readonly int ResultsPerPage = 20;
        private readonly int Offset = 10;
        private readonly string StartDate = "2010-01-02";
        private readonly string EndDate = "2020-02-02";
        private readonly string Status = "QUEUED";
        private readonly string Type = "ingestion";

        [Test]
        public void AllTasksSettersSet()
        {
            AllTasksRequest req = new AllTasksRequest(this.ResultsPerPage, this.Page)
            {
                EndDate = this.EndDate,
                StartDate = this.StartDate,
                Status = this.Status,
                Type = this.Type,
                Offset = this.Offset
            };

            Assert.AreEqual(this.ResultsPerPage, req.ResultsPerPage);
            Assert.AreEqual(this.Page, req.Page);
            Assert.AreEqual(this.StartDate, req.StartDate);
            Assert.AreEqual(this.EndDate, req.EndDate);
            Assert.AreEqual(this.Status, req.Status);
            Assert.AreEqual(this.Type, req.Type);
            Assert.AreEqual(this.Offset, req.Offset);
        }

        [Test]
        public void GetRequestParameters()
        {
            AllTasksRequest req = new AllTasksRequest()
            {
                Page = this.Page,
                ResultsPerPage = this.ResultsPerPage,
                Type = this.Type,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                Status = this.Status,
                Offset = this.Offset
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.ResultsPerPage, requestParameters[Constants.RESULTS_PER_PAGE]);
            Assert.AreEqual(this.Page, requestParameters[Constants.PAGE]);
            Assert.AreEqual(this.Type, requestParameters[Constants.TYPE]);
            Assert.AreEqual(this.StartDate, requestParameters[Constants.START_DATE]);
            Assert.AreEqual(this.EndDate, requestParameters[Constants.END_DATE]);
            Assert.AreEqual(this.Status, requestParameters[Constants.STATUS]);
            Assert.AreEqual(this.Offset, requestParameters[Constants.OFFSET]);
        }
    }
}
