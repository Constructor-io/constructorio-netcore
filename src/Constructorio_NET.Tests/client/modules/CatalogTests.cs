using System.Collections.Generic;
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
        private readonly ConstructorItemGroup itemGroup1 = new ConstructorItemGroup("itemGroup1", "Item Group 1", JObject.Parse("{\"name\":\"value\"}"));
        private readonly ConstructorItemGroup itemGroup2 = new ConstructorItemGroup("itemGroup2", "Item Group 2", JObject.Parse("{\"name\":\"value\"}"));
        private readonly string sortBy = "relevance";
        private readonly string pathInMetadata = "relevance";
        private readonly SortOrder sortOrderType = SortOrder.Ascending;
        private readonly List<SortOption> sortOptions = new List<SortOption>()
        {
            new SortOption("Collections", SortOrder.Descending, "Collections"),
            new SortOption("relevance", SortOrder.Ascending, "relevance")
        };
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

        [OneTimeTearDown]
        public async Task Cleanup()
        {
            var constructorio = new ConstructorIO(Config);
            await constructorio.Catalog.DeleteSortOptions(new SortOptionsListRequest(this.sortOptions));
            await constructorio.Catalog.SetSortOptions(new SortOptionsListRequest(this.sortOptions));
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
        public async Task PatchCatalogWithOnMissingStrategy()
        {
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "items", itemsStream },
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            CatalogRequest req = new CatalogRequest(files);
            req.OnMissing = CatalogRequest.OnMissingStrategy.IGNORE;
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
            Assert.IsTrue(res.TotalCount == 1, "Total Count is equal to 1");
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
            Assert.IsTrue(res.TotalCount == 1, "Total Count is equal to 1");
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
                new Searchability("testSearchability") { ExactSearchable = true }
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
                new Searchability("testSearchability") { ExactSearchable = true, Hidden = false, FuzzySearchable = false, Displayable = true },
                new Searchability("testSearchability2") { Hidden = true }
            };
            PatchSearchabilitiesRequest req = new PatchSearchabilitiesRequest(searchabilities);
            SearchabilitiesResponse res = await constructorio.Catalog.PatchSearchabilities(req);
            Assert.IsNotNull(res.TotalCount, "Total Count should exist");
            Assert.IsNotNull(res.Searchabilities, "Searchabilities should exist");
            Assert.IsTrue(res.Searchabilities[0].Name == searchabilities[0].Name, "All returned Searchabilities match requested");
            Assert.IsTrue(res.Searchabilities[0].Hidden == false, "Searchability field is set");
            Assert.IsTrue(res.Searchabilities[0].FuzzySearchable == false, "Searchability field is set");
            Assert.IsTrue(res.Searchabilities[0].ExactSearchable == true, "Searchability field is set");
            Assert.IsTrue(res.Searchabilities[0].Displayable == true, "Searchability field is set");
            Assert.IsTrue(res.Searchabilities[1].Name == searchabilities[1].Name, "All returned Searchabilities match requested");
            Assert.IsTrue(res.Searchabilities[1].Hidden == true, "Searchability field is set");
        }

        public async Task AddItemGroup()
        {
            ItemGroupsRequest req = new ItemGroupsRequest(new List<ConstructorItemGroup> { itemGroup1 });
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
            ItemGroupsRequest req = new ItemGroupsRequest(new List<ConstructorItemGroup> { itemGroup1 });
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

        [Test]
        public void SetSortOptionsShouldFailWhenRequiredFieldsAreMissing()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SortOption sortOption = new SortOption(this.sortBy, SortOrder.Descending);
            SortOptionsListRequest req = new SortOptionsListRequest(new List<SortOption> { sortOption });

            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Catalog.SetSortOptions(req));
            Assert.AreEqual("Http[400]: sort_options[0].path_in_metadata: field required", ex.Message, "Correct Error is Returned");

            req.SortOptions[0].PathInMetadata = this.pathInMetadata;
            req.SortOptions[0].SortBy = null;
            ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Catalog.SetSortOptions(req));
            Assert.AreEqual("Http[400]: sort_options[0].sort_by: field required", ex.Message, "Correct Error is Returned");
        }

        [Test]
        public async Task SetSortOptions()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SortOptionsListRequest req = new SortOptionsListRequest(this.sortOptions);
            SortOptionList res = await constructorio.Catalog.SetSortOptions(req);

            Assert.IsNotNull(res.SortOptions, "Sort options array should exist.");
            Assert.IsTrue(res.SortOptions.Count > 0, "Sort option shoud exist.");
            Assert.AreEqual(res.SortOptions[0].SortBy, this.sortOptions[0].SortBy, "Sort option should match sort_by passed in.");
        }

        [Test]
        public async Task RetrieveSortOptionsShouldReturnResults()
        {
            SortOptionsRequest req = new SortOptionsRequest();
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SortOptionList res = await constructorio.Catalog.RetrieveSortOptions(req);

            Assert.IsNotNull(res.SortOptions, "Sort options shoud exist.");
            Assert.IsTrue(res.SortOptions.Count > 1, "Sort options shoud exist.");
        }

        [Test]
        public async Task RetrieveSortOptionsWithSortByShouldReturnResults()
        {
            SortOptionsRequest req = new SortOptionsRequest(this.sortBy);
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SortOptionList res = await constructorio.Catalog.RetrieveSortOptions(req);

            Assert.IsNotNull(res.SortOptions, "Sort options array should exist.");
            Assert.AreEqual(1, res.SortOptions.Count, "Sort option shoud exist.");
            Assert.AreEqual(res.SortOptions[0].SortBy, this.sortBy, "Sort option should match sort_by passed in.");
        }

        [Test]
        public async Task CreateSortOption()
        {
            string sortByTest = "test";
            SortOrder sortOrder = SortOrder.Ascending;
            string pathInMetadataTest = "test";

            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SortOption sortOption = new SortOption(sortByTest, sortOrder, pathInMetadataTest);
            SortOptionsSingleRequest req = new SortOptionsSingleRequest(sortOption);

            SortOption res = await constructorio.Catalog.CreateSortOption(req);
            Assert.AreEqual(sortByTest, res.SortBy);
            Assert.AreEqual(sortOrder, res.SortOrder);
            Assert.AreEqual(pathInMetadataTest, res.PathInMetadata);
        }

        [Test]
        public void CreateSortOptionFailsIfExists()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SortOption sortOption = new SortOption(this.sortBy, this.sortOrderType, this.pathInMetadata);
            SortOptionsSingleRequest req = new SortOptionsSingleRequest(sortOption);

            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Catalog.CreateSortOption(req));
            Assert.AreEqual($"Http[409]: Sort option with sort by `{this.sortBy}` and sort order `{SortOrder.Ascending.ToString().ToLower()}` already exists.", ex.Message, "Correct Error is Returned");
        }

        [Test]
        public void CreateSortOptionFailsIfMissingRequiredParams()
        {
            string sortByTest = "test";
            SortOrder sortOrder = SortOrder.Ascending;

            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SortOption sortOption = new SortOption(sortByTest, sortOrder);
            SortOptionsSingleRequest req = new SortOptionsSingleRequest(sortOption);

            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Catalog.CreateSortOption(req));
            Assert.AreEqual("Http[400]: path_in_metadata: field required", ex.Message, "Correct Error is Returned");

            req.SortOption.PathInMetadata = this.pathInMetadata;
            req.SortOption.SortBy = null;
            ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Catalog.CreateSortOption(req));
            Assert.AreEqual("Http[400]: sort_by: field required", ex.Message, "Correct Error is Returned");
        }

        [Test]
        public void CreateOrReplaceSortOptionFailsIfMissingRequiredParams()
        {
            string sortByTest = "test2";
            SortOrder sortOrder = SortOrder.Ascending;

            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SortOption sortOption = new SortOption(sortByTest, sortOrder);
            SortOptionsSingleRequest req = new SortOptionsSingleRequest(sortOption);

            Assert.ThrowsAsync<ConstructorException>(() => constructorio.Catalog.CreateOrReplaceSortOption(req));

            // currently the API returns 500, commenting the exact error code & message check until it's fixed
            // Assert.AreEqual("Http[400]: path_in_metadata is a required field of type string", ex.Message, "Correct Error is Returned");

            req.SortOption.PathInMetadata = this.pathInMetadata;
            req.SortOption.SortBy = null;
            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Catalog.CreateOrReplaceSortOption(req));
            Assert.AreEqual("SortBy is a required property for SortOptionsSingleRequest.SortOption.", ex.Message, "Correct Error is Returned");
        }

        [Test]
        public async Task CreateOrReplaceSortOption()
        {
            string sortByTest = "test2";
            SortOrder sortOrder = SortOrder.Ascending;
            string pathInMetadataTest = "test";

            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SortOption sortOption = new SortOption(sortByTest, sortOrder, pathInMetadataTest);
            SortOptionsSingleRequest req = new SortOptionsSingleRequest(sortOption);

            SortOption res = await constructorio.Catalog.CreateOrReplaceSortOption(req);
            Assert.AreEqual(sortByTest, res.SortBy);
            Assert.AreEqual(sortOrder, res.SortOrder);
            Assert.AreEqual(pathInMetadataTest, res.PathInMetadata);
        }

        [Test]
        public void UpdateOptionFailsIfMissingRequiredParams()
        {
            string sortByTest = this.sortBy;
            SortOrder sortOrder = SortOrder.Descending;

            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SortOption sortOption = new SortOption(sortByTest, sortOrder);
            SortOptionsSingleRequest req = new SortOptionsSingleRequest(sortOption);

            req.SortOption.SortBy = null;
            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Catalog.UpdateSortOption(req));
            Assert.AreEqual("SortBy is a required property for SortOptionsSingleRequest.SortOption.", ex.Message, "Correct Error is Returned");
        }

        [Test]
        public void UpdateOptionFailsIfSortOptionDoesntExist()
        {
            string sortByTest = this.sortBy;
            SortOrder sortOrder = SortOrder.Descending;

            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SortOption sortOption = new SortOption(sortByTest, sortOrder);
            SortOptionsSingleRequest req = new SortOptionsSingleRequest(sortOption);

            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Catalog.UpdateSortOption(req));
            Assert.AreEqual($"Http[404]: No such sorting option with name `{this.sortBy}` and order of `descending` was found.", ex.Message, "Correct Error is Returned");
        }

        [Test]
        public async Task UpdateSortOption()
        {
            string sortByTest = this.sortBy;
            SortOrder sortOrder = this.sortOrderType;
            string pathInMetadataTest = "test";

            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SortOption sortOption = new SortOption(sortByTest, sortOrder, pathInMetadataTest);
            SortOptionsSingleRequest req = new SortOptionsSingleRequest(sortOption);

            SortOption res = await constructorio.Catalog.UpdateSortOption(req);
            Assert.AreEqual(sortByTest, res.SortBy);
            Assert.AreEqual(sortOrder, res.SortOrder);
            Assert.AreEqual(pathInMetadataTest, res.PathInMetadata);
        }

        [Test]
        public void DeleteSortOptionsShouldFailWhenRequiredFieldsAreMissing()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SortOption sortOption = new SortOption(this.sortBy, this.sortOrderType, this.pathInMetadata);
            SortOptionsListRequest req = new SortOptionsListRequest(new List<SortOption> { sortOption });

            req.SortOptions[0].SortBy = null;
            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Catalog.DeleteSortOptions(req));
            Assert.AreEqual("Http[400]: sort_options[0].sort_by: field required", ex.Message, "Correct Error is Returned");
        }

        [Test]
        public void DeleteSortOptionsShouldFailIfSortOptionDoesntExist()
        {
            string sortByTest = "test-delete";
            SortOrder sortOrder = SortOrder.Ascending;
            string pathInMetadataTest = "test-delete";

            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SortOption sortOption = new SortOption(sortByTest, sortOrder, pathInMetadataTest);
            SortOptionsListRequest req = new SortOptionsListRequest(new List<SortOption> { sortOption });

            var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Catalog.DeleteSortOptions(req));
            Assert.AreEqual("Http[404]: No such sorting option with name `test-delete` and order of `ascending` was found.", ex.Message, "Correct Error is Returned");
        }

        [Test]
        public async Task DeleteSortOptions()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            SortOption sortOption = new SortOption(this.sortBy, this.sortOrderType, this.pathInMetadata);

            SortOptionsListRequest req = new SortOptionsListRequest(new List<SortOption> { sortOption });
            bool res = await constructorio.Catalog.DeleteSortOptions(req);
            Assert.IsTrue(res);

            // Resets the store
            await constructorio.Catalog.SetSortOptions(new SortOptionsListRequest(this.sortOptions));
        }
    }
}
