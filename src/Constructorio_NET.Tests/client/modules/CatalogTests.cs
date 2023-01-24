using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class CatalogTest
    {
        private readonly string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private ConstructorioConfig Config;
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
        public async Task RetrieveSearchabilitiesShouldReturnResult()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            RetrieveSearchabilitiesRequest req = new RetrieveSearchabilitiesRequest();
            SearchabilitiesResponse res = await constructorio.Catalog.RetrieveSearchabilities(req);
            Assert.IsNotNull(res.TotalCount, "Total Count should exist");
            Assert.IsNotNull(res.Searchabilities, "Searchabilities should exist");
        }

        [Test]
        public async Task RetrieveSearchabilitiesWithOptionalParamsShouldReturnResult()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            RetrieveSearchabilitiesRequest req = new RetrieveSearchabilitiesRequest
            {
                Filters = new Dictionary<string, string> { { "name", "groups" } },
                Page = 1,
                NumResultsPerPage = 1,
                SortOrder = "descending",
                SortBy = "name",
                Searchable = true
            };
            SearchabilitiesResponse res = await constructorio.Catalog.RetrieveSearchabilities(req);
            Assert.IsTrue(res.TotalCount == 1, "Total Count should exist");
            Assert.IsNotNull(res.Searchabilities.Count == 1, "Searchabilities should exist");
        }

        [Test]
        public async Task RetrieveSearchabilitiesWithNameShouldReturnResult()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            RetrieveSearchabilitiesRequest req = new RetrieveSearchabilitiesRequest
            {
                Name = "groups",
            };
            SearchabilitiesResponse res = await constructorio.Catalog.RetrieveSearchabilities(req);
            Assert.IsTrue(res.TotalCount == 1, "Total Count should exist");
            Assert.IsNotNull(res.Searchabilities.Count == 1, "Searchabilities should exist");
        }

        [Test]
        public async Task RetrieveSearchabilitiesWithOffsetShouldReturnResult()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            RetrieveSearchabilitiesRequest req = new RetrieveSearchabilitiesRequest
            {
                Filters = new Dictionary<string, string> { { "name", "groups" } },
                Offset = 5,
                NumResultsPerPage = 1,
                SortOrder = "descending",
                SortBy = "name",
                Searchable = true
            };
            SearchabilitiesResponse res = await constructorio.Catalog.RetrieveSearchabilities(req);
            Assert.IsTrue(res.TotalCount > 0, "Total Count should exist");
            Assert.IsNotNull(res.Searchabilities.Count > 0, "Searchabilities should exist");
        }

        [Test]
        public async Task PatchSearchabilitiesWithSingleSearchabilityShouldReturnResult()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            List<Searchability> searchabilities = new List<Searchability>
            {
                new Searchability() { Name = "testSearchability", ExactSearchable = true }
            };
            PatchSearchabilitiesRequest req = new PatchSearchabilitiesRequest(searchabilities);
            SearchabilitiesResponse res = await constructorio.Catalog.PatchSearchabilities(req);
            Assert.IsNotNull(res.TotalCount, "Total Count should exist");
            Assert.IsNotNull(res.Searchabilities, "Searchabilities should exist");
            Assert.IsTrue(res.Searchabilities[0].Name == searchabilities[0].Name, "Searchabilities match");
        }

        [Test]
        public async Task PatchSearchabilitiesWithMultipleSearchabilitiesShouldReturnResult()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            List<Searchability> searchabilities = new List<Searchability>
            {
                new Searchability() { Name = "testSearchability", ExactSearchable = true, Hidden = false, FuzzySearchable = false, Displayable = true },
                new Searchability() { Name = "testSearchability2", Hidden = true }
            };
            PatchSearchabilitiesRequest req = new PatchSearchabilitiesRequest(searchabilities);
            SearchabilitiesResponse res = await constructorio.Catalog.PatchSearchabilities(req);
            Assert.IsNotNull(res.TotalCount, "Total Count should exist");
            Assert.IsNotNull(res.Searchabilities, "Searchabilities should exist");
            Assert.IsTrue(res.Searchabilities[0].Name == searchabilities[0].Name, "All Searchabilities match");
            Assert.IsTrue(res.Searchabilities[1].Name == searchabilities[1].Name, "All Searchabilities match");
            Assert.IsTrue(res.Searchabilities[1].Hidden == true, "Searchability field is set");
            Assert.IsTrue(res.Searchabilities[0].Hidden == false, "Searchability field is set");
            Assert.IsTrue(res.Searchabilities[0].FuzzySearchable == false, "Searchability field is set");
            Assert.IsTrue(res.Searchabilities[0].ExactSearchable == true, "Searchability field is set");
            Assert.IsTrue(res.Searchabilities[0].Displayable == true, "Searchability field is set");
        }
    }
}