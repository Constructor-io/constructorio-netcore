using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class TasksTest
    {
        private readonly string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private ConstructorioConfig Config;
        private int TaskId;

        [OneTimeSetUp]
        public async Task Setup()
        {
            JObject json = JObject.Parse(File.ReadAllText("./../../../../../.config/local.json"));
            string testApiToken = json.SelectToken("TEST_API_TOKEN").Value<string>();

            this.Config = new ConstructorioConfig(this.ApiKey)
            {
                ApiToken = testApiToken
            };

            StreamContent itemsStream = new StreamContent(File.OpenRead("./../../../resources/csv/items.csv"));
            itemsStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "items", itemsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = await constructorio.Catalog.ReplaceCatalog(req);
            this.TaskId = res.TaskId;
        }

        [Test]
        public async Task GetTaskShouldReturnResult()
        {
            TaskRequest req = new TaskRequest(this.TaskId);
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            TaskResponse res = await constructorio.Tasks.GetTask(req);

            Assert.NotNull(res.Status, "Status exists");
            Assert.NotNull(res.Type, "Type exists");
            Assert.NotNull(res.SubmissionTime, "Submission Time exists");
            Assert.NotNull(res.Status, "Status exists");
            Assert.AreEqual(res.Id, this.TaskId, "Id is same as provided Task Id");
        }

        [Test]
        public async Task GetAllTasksShouldReturnResult()
        {
            AllTasksRequest req = new AllTasksRequest();
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            AllTasksResponse res = await constructorio.Tasks.GetAllTasks(req);

            Assert.NotNull(res.StatusCounts, "Status Counts exists");
            Assert.GreaterOrEqual(res.TotalCount, 1, "At least 1 task exists");
            Assert.GreaterOrEqual(res.Tasks.Count, 1, "At least 1 task exists");
        }
    }
}