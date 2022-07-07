using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class TasksTest
    {
        private string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private Hashtable Options = new Hashtable();
        private int TaskId;

        [OneTimeSetUp]
        public void Setup()
        {
            JObject json = JObject.Parse(File.ReadAllText("./../../../../../.config/local.json"));
            string testApiToken = json.SelectToken("TEST_API_TOKEN").Value<string>();

            this.Options = new Hashtable()
            {
               { Constants.API_KEY, this.ApiKey },
               { Constants.API_TOKEN, testApiToken }
            };

            StreamContent itemsStream = new StreamContent(File.OpenRead("./../../../resources/csv/items.csv"));
            itemsStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "items", itemsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = constructorio.Catalog.ReplaceCatalog(req);
            this.TaskId = res.TaskId;
        }

        [Test]
        public void GetTaskShouldReturnResult()
        {
            TaskRequest req = new TaskRequest(this.TaskId);
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            TaskResponse res = constructorio.Tasks.GetTask(req);

            Assert.NotNull(res.Status, "Status exists");
            Assert.NotNull(res.Type, "Type exists");
            Assert.NotNull(res.SubmissionTime, "Submission Time exists");
            Assert.NotNull(res.Status, "Status exists");
            Assert.AreEqual(res.Id, this.TaskId, "Id is same as provided Task Id");
        }

        [Test]
        public void GetAllTasksShouldReturnResult()
        {
            AllTasksRequest req = new AllTasksRequest();
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            AllTasksResponse res = constructorio.Tasks.GetAllTasks(req);

            Assert.NotNull(res.StatusCounts, "Status Counts exists");
            Assert.GreaterOrEqual(res.TotalCount, 1, "At least 1 task exists");
            Assert.GreaterOrEqual(res.Tasks.Count, 1, "At least 1 task exists");
        }
    }
}