using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using static System.Net.WebRequestMethods;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class CatalogTest
    {
        private readonly string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private readonly ConstructorItemGroup itemGroup = new ConstructorItemGroup("itemGroup1", "Item Group 1", JObject.Parse("{\"name\":\"value\"}"));
        private ConstructorioConfig Config;
        private StreamContent itemsStream;
        private StreamContent variationsStream;
        private StreamContent itemGroupsStream;

        [OneTimeSetUp]
        public void Setup()
        {
            JObject json = JObject.Parse(System.IO.File.ReadAllText("./../../../../../.config/local.json"));
            string testApiToken = json.SelectToken("TEST_API_TOKEN").Value<string>();

            itemsStream = new StreamContent(System.IO.File.OpenRead("./../../../resources/csv/items.csv"));
            itemsStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            variationsStream = new StreamContent(System.IO.File.OpenRead("./../../../resources/csv/variations.csv"));
            variationsStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            itemGroupsStream = new StreamContent(System.IO.File.OpenRead("./../../../resources/csv/item_groups.csv"));
            itemGroupsStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");

            this.Config = new ConstructorioConfig(this.ApiKey, testApiToken);
        }

        [SetUp]
        public void Delay()
        {
            Thread.Sleep(1000);
        }

        [Test]
        public void ReplaceCatalogWithInvalidApiTokenShouldError()
        {
            var files = new Dictionary<string, StreamContent>()
            {
                { "items", itemsStream },
            };
            var constructorio = new ConstructorIO(new ConstructorioConfig(ApiKey, "invalidKey"));
            var req = new CatalogRequest(files);
            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Catalog.ReplaceCatalog(req));
            Assert.IsTrue(ex.Message == "Http[401]: Invalid auth_token. If you've forgotten your token, you can generate a new one at app.constructor.io/dashboard", "Correct Error is Returned");
        }

        [Test]
        public async Task ReplaceCatalogWithItems()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "items", itemsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = await constructorio.Catalog.ReplaceCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public async Task ReplaceCatalogWithVariations()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "variations", variationsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = await constructorio.Catalog.ReplaceCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public async Task ReplaceCatalogWithItemGroups()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "item_groups", itemGroupsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = await constructorio.Catalog.ReplaceCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public void ReplaceCatalogWithNoFiles()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>();
            Assert.Throws<ConstructorException>(() => new CatalogRequest(files));
        }

        [Test]
        public void UpdateCatalogWithInvalidApiTokenShouldError()
        {
            var files = new Dictionary<string, StreamContent>()
            {
                { "items", itemsStream },
            };
            var constructorio = new ConstructorIO(new ConstructorioConfig(ApiKey, "invalidKey"));
            var req = new CatalogRequest(files);
            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Catalog.UpdateCatalog(req));
            Assert.IsTrue(ex.Message == "Http[401]: Invalid auth_token. If you've forgotten your token, you can generate a new one at app.constructor.io/dashboard", "Correct Error is Returned");
        }

        [Test]
        public async Task UpdateCatalogWithItems()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "items", itemsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = await constructorio.Catalog.UpdateCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public async Task UpdateCatalogWithVariations()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "variations", variationsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = await constructorio.Catalog.UpdateCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public async Task UpdateCatalogWithItemGroups()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "item_groups", itemGroupsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = await constructorio.Catalog.UpdateCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public void PatchCatalogWithInvalidApiTokenShouldError()
        {
            var files = new Dictionary<string, StreamContent>()
            {
                { "items", itemsStream },
            };
            var constructorio = new ConstructorIO(new ConstructorioConfig(ApiKey) { ApiToken = "invalidKey" });
            var req = new CatalogRequest(files);
            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Catalog.PatchCatalog(req));
            Assert.IsTrue(ex.Message == "Http[401]: Invalid auth_token. If you've forgotten your token, you can generate a new one at app.constructor.io/dashboard", "Correct Error is Returned");
        }

        [Test]
        public async Task PatchCatalogWithItems()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "items", itemsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = await constructorio.Catalog.PatchCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public async Task PatchCatalogWithVariations()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "variations", variationsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = await constructorio.Catalog.PatchCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public async Task PatchCatalogWithItemGroups()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "item_groups", itemGroupsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = await constructorio.Catalog.PatchCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public async Task AddItemGroup()
        {
            ItemGroupsRequest req = new ItemGroupsRequest(new List<ConstructorItemGroup> { itemGroup } );
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            ConstructorItemGroup res = await constructorio.Catalog.AddItemGroup(req);
            Console.WriteLine(res);
            Assert.IsTrue(res.Id == itemGroup.Id, "Id should match");
            Assert.IsTrue(res.Name == itemGroup.Name, "Name should match");
            Assert.IsTrue(res.ParentId == itemGroup.ParentId, "ParentId should match");
            Assert.IsTrue(res.Data.ToString() == itemGroup.Data.ToString(), "Data should match");
        }
    }
}