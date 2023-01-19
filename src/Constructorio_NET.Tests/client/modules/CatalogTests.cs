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
        private readonly ConstructorItemGroup itemGroup1 = new ConstructorItemGroup("itemGroup1", "Item Group 1", JObject.Parse("{\"name\":\"value\"}"));
        private readonly ConstructorItemGroup itemGroup2 = new ConstructorItemGroup("itemGroup2", "Item Group 2", JObject.Parse("{\"name\":\"value\"}"));
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
            ItemGroupsRequest req = new ItemGroupsRequest(new List<ConstructorItemGroup> { itemGroup1 } );
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            ConstructorItemGroup res = await constructorio.Catalog.AddItemGroup(req);
            Assert.IsTrue(res.Id == itemGroup1.Id, "Id should match");
            Assert.IsTrue(res.Name == itemGroup1.Name, "Name should match");
            Assert.IsTrue(res.ParentId == itemGroup1.ParentId, "ParentId should match");
            Assert.IsTrue(res.Data.ToString() == itemGroup1.Data.ToString(), "Data should match");
        }

        [Test]
        public async Task UpdateItemGroup()
        {
            string newName = "New Name";
            JObject newData = JObject.Parse("{\"value\":\"name\"}");
            itemGroup1.Name = newName;
            itemGroup1.Data = newData;
            ItemGroupsRequest req = new ItemGroupsRequest(new List<ConstructorItemGroup> { itemGroup1 } );
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            ConstructorItemGroup res = await constructorio.Catalog.AddItemGroup(req);
            Assert.IsTrue(res.Id == itemGroup1.Id, "Id should match");
            Assert.IsTrue(res.Name == newName, "Name should match");
            Assert.IsTrue(res.ParentId == itemGroup1.ParentId, "ParentId should match");
            Assert.IsTrue(res.Data.ToString() == newData.ToString(), "Data should match");
        }

        [Test]
        public async Task AddItemGroups()
        {
            List<ConstructorItemGroup> itemGroups = new List<ConstructorItemGroup>();
            ConstructorItemGroup nestedItemGroup = new ConstructorItemGroup("NestedItemGroup1", "Nested Item Group 1");
            nestedItemGroup.Children = new List<ConstructorItemGroup> { new ConstructorItemGroup("SubNestedItemGroup1", "Sub Nested Item Group 1") };
            itemGroups.Add(nestedItemGroup);
            ItemGroupsRequest req = new ItemGroupsRequest(itemGroups);
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            ItemGroupsResponse res = await constructorio.Catalog.AddItemGroups(req);
            Assert.IsTrue(res.ItemGroups.Processed > 0);
            Assert.IsNotNull(res.ItemGroups.Inserted);
            Assert.IsNotNull(res.ItemGroups.Updated);
        }

        [Test]
        public async Task UpdateItemGroups()
        {
            List<ConstructorItemGroup> itemGroups = new List<ConstructorItemGroup>();
            itemGroup1.Name = "Group Item 1";
            itemGroups.Add(itemGroup1);
            itemGroups.Add(itemGroup2);
            ItemGroupsRequest req = new ItemGroupsRequest(itemGroups);
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            ItemGroupsResponse res = await constructorio.Catalog.UpdateItemGroups(req);
            Assert.IsTrue(res.ItemGroups.Processed > 0);
            Assert.IsNotNull(res.ItemGroups.Inserted);
            Assert.IsNotNull(res.ItemGroups.Updated);
        }

        [Test]
        public async Task GetItemGroup()
        {
            ItemGroupsRequest req = new ItemGroupsRequest();
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            await constructorio.Catalog.AddItemGroup(new ItemGroupsRequest(new List<ConstructorItemGroup> { itemGroup1 }));
            ItemGroupsGetResponse res = await constructorio.Catalog.GetItemGroup(req);
            Assert.IsTrue(res.TotalCount > 0, "Total Count should exist");
            Assert.IsNotEmpty(res.ItemGroups, "Item groups should be returned");
        }

        [Test]
        public async Task GetItemGroupWithGroupId()
        {
            ItemGroupsRequest req = new ItemGroupsRequest(itemGroup1.Id);
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            await constructorio.Catalog.AddItemGroup(new ItemGroupsRequest(new List<ConstructorItemGroup> { itemGroup1 }));
            ItemGroupsGetResponse res = await constructorio.Catalog.GetItemGroup(req);
            Assert.IsTrue(res.TotalCount > 0, "Total Count should exist");
            Assert.IsNotEmpty(res.ItemGroups, "Item groups should be returned");
            Assert.IsTrue(res.ItemGroups[0].Id == itemGroup1.Id);
            Assert.IsTrue(res.ItemGroups[0].Name == itemGroup1.Name);
        }

        [Test]
        public async Task DeleteItemGroups()
        {
            ItemGroupsRequest req = new ItemGroupsRequest();
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            string res = await constructorio.Catalog.DeleteItemGroups(req);
            Assert.IsTrue(res == "We've started deleting all of your groups. This may take some time to complete.");
        }
    }
}