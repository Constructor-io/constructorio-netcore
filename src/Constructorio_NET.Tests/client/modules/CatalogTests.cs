using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class CatalogTest
    {
        private string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private string ClientId = "r4nd-cl1ent-1d";
        private Hashtable Options = new Hashtable();
        private int SessionId = 4;
        private Hashtable UserParameters = new Hashtable();
        private StreamContent itemsStream;
        private StreamContent variationsStream;
        private StreamContent itemGroupsStream;

        [OneTimeSetUp]
        public void Setup()
        {
            JObject json = JObject.Parse(File.ReadAllText("./../../../../../.config/local.json"));
            string testApiToken = json.SelectToken("TEST_API_TOKEN").Value<string>();

            itemsStream = new StreamContent(File.OpenRead("./../../../resources/csv/items.csv"));
            itemsStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            variationsStream = new StreamContent(File.OpenRead("./../../../resources/csv/variations.csv"));
            variationsStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            itemGroupsStream = new StreamContent(File.OpenRead("./../../../resources/csv/item_groups.csv"));
            itemGroupsStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");

            this.Options = new Hashtable()
            {
                { Constants.API_KEY, this.ApiKey },
                { "apiToken", testApiToken },
            };
            this.UserParameters = new Hashtable()
            {
                { "clientId", ClientId },
                { "sessionId", SessionId },
            };
        }

        [SetUp]
        public void Delay()
        {
            Thread.Sleep(1000);
        }

        [Test, Order(1)]
        public void ReplaceCatalogWithItems()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "items", itemsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = constructorio.Catalog.ReplaceCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public void ReplaceCatalogWithVariations()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "variations", variationsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = constructorio.Catalog.ReplaceCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public void ReplaceCatalogWithItemGroups()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "item_groups", itemGroupsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = constructorio.Catalog.ReplaceCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public void ReplaceCatalogWithNoFiles()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>();
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            Assert.Throws<ConstructorException>(() => new CatalogRequest(files));
        }

        [Test]
        public void UpdateCatalogWithItems()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "items", itemsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = constructorio.Catalog.UpdateCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public void UpdateCatalogWithVariations()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "variations", variationsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = constructorio.Catalog.UpdateCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public void UpdateCatalogWithItemGroups()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "item_groups", itemGroupsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = constructorio.Catalog.UpdateCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public void PatchCatalogWithItems()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "items", itemsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = constructorio.Catalog.PatchCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public void PatchCatalogWithVariations()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "variations", variationsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = constructorio.Catalog.PatchCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }

        [Test]
        public void PatchCatalogWithItemGroups()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "item_groups", itemGroupsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = constructorio.Catalog.PatchCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }
    }
}