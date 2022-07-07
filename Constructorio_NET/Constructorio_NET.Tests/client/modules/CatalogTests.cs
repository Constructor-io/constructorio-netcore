using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class CatalogTest
    {
        private string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private string ClientId = "r4nd-cl1ent-1d";
        private Hashtable Options = new Hashtable();
        private string Query = "item";
        private int SessionId = 4;
        private Hashtable UserParameters = new Hashtable();

        [SetUp]
        public void Setup()
        {
            JObject json = JObject.Parse(File.ReadAllText("./../../../../../.config/local.json"));
            string testApiToken = json.SelectToken("TEST_API_TOKEN").Value<string>();

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

        [Test]
        public void ReplaceCatalogWithItems()
        {
            StreamContent itemsStream = new StreamContent(File.OpenRead("./../../../resources/csv/items.csv"));
            itemsStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
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
            StreamContent variationsStream = new StreamContent(File.OpenRead("./../../../resources/csv/variations.csv"));
            variationsStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
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
            StreamContent itemGroupsStream = new StreamContent(File.OpenRead("./../../../resources/csv/item_groups.csv"));
            itemGroupsStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
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
            StreamContent stream = new StreamContent(File.OpenRead("./../../../resources/csv/items.csv"));
            stream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "items", stream },
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
            StreamContent stream = new StreamContent(File.OpenRead("./../../../resources/csv/variations.csv"));
            stream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "variations", stream },
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
            StreamContent stream = new StreamContent(File.OpenRead("./../../../resources/csv/item_groups.csv"));
            stream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "item_groups", stream },
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
            StreamContent stream = new StreamContent(File.OpenRead("./../../../resources/csv/items.csv"));
            stream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "items", stream },
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
            StreamContent stream = new StreamContent(File.OpenRead("./../../../resources/csv/variations.csv"));
            stream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "variations", stream },
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
            StreamContent stream = new StreamContent(File.OpenRead("./../../../resources/csv/item_groups.csv"));
            stream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "item_groups", stream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            CatalogRequest req = new CatalogRequest(files);
            CatalogResponse res = constructorio.Catalog.PatchCatalog(req);
            Assert.IsNotNull(res.TaskId, "TaskId should exist");
            Assert.IsNotNull(res.TaskStatusPath, "TaskStatusPath should exist");
        }
    }
}