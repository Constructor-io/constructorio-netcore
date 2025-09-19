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
    public class CancellationTokenTests
    {
        private readonly string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private readonly string ClientId = "r4nd-cl1ent-1d";
        private readonly int SessionId = 4;
        private ConstructorioConfig Config;
        private UserInfo UserInfo;

        [SetUp]
        public void Setup()
        {
            JObject json = JObject.Parse(File.ReadAllText("./../../../../../.config/local.json"));
            string testApiToken = json.SelectToken("TEST_API_TOKEN").Value<string>();
            this.Config = new ConstructorioConfig(this.ApiKey) { ApiToken = testApiToken };
            this.UserInfo = new UserInfo(ClientId, SessionId);
        }

        [Test]
        public void GetAutocompleteResults_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            AutocompleteRequest req = new AutocompleteRequest("item") { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Autocomplete.GetAutocompleteResults(req, cancelledToken)
            );
        }

        [Test]
        public void GetAutocompleteResults_WithTimeoutToken_ThrowsTaskCanceledException()
        {
            // Arrange
            AutocompleteRequest req = new AutocompleteRequest("item") { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(1));

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Autocomplete.GetAutocompleteResults(req, cts.Token)
            );
        }

        [Test]
        public async Task GetAutocompleteResults_WithValidToken_CompletesSuccessfully()
        {
            // Arrange
            AutocompleteRequest req = new AutocompleteRequest("item") { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            // Act
            AutocompleteResponse result = await constructorio.Autocomplete.GetAutocompleteResults(req, cts.Token);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ResultId);
        }

        [Test]
        public void GetSearchResults_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            SearchRequest req = new SearchRequest("item") { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Search.GetSearchResults(req, cancelledToken)
            );
        }

        [Test]
        public void GetSearchResults_WithTimeoutToken_ThrowsTaskCanceledException()
        {
            // Arrange
            SearchRequest req = new SearchRequest("item") { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(1));

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Search.GetSearchResults(req, cts.Token)
            );
        }

        [Test]
        public async Task GetSearchResults_WithValidToken_CompletesSuccessfully()
        {
            // Arrange
            SearchRequest req = new SearchRequest("item") { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            // Act
            SearchResponse result = await constructorio.Search.GetSearchResults(req, cts.Token);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ResultId);
        }

        [Test]
        public void GetBrowseResults_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            BrowseRequest req = new BrowseRequest("group_id", "Brand") { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Browse.GetBrowseResults(req, cancelledToken)
            );
        }

        [Test]
        public void GetBrowseItemsResult_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            List<string> itemIds = new List<string> { "test_item" };
            BrowseItemsRequest req = new BrowseItemsRequest(itemIds) { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Browse.GetBrowseItemsResult(req, cancelledToken)
            );
        }

        [Test]
        public void GetBrowseFacetsResult_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            BrowseFacetsRequest req = new BrowseFacetsRequest() { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Browse.GetBrowseFacetsResult(req, cancelledToken)
            );
        }

        [Test]
        public void GetBrowseFacetOptionsResult_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            BrowseFacetOptionsRequest req = new BrowseFacetOptionsRequest("Brand") { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Browse.GetBrowseFacetOptionsResult(req, cancelledToken)
            );
        }

        [Test]
        public void ReplaceCatalog_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            var files = CreateTestFiles();
            CatalogRequest req = new CatalogRequest(files);
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Catalog.ReplaceCatalog(req, cancelledToken)
            );

            // Cleanup
            DisposeFiles(files);
        }

        [Test]
        public void UpdateCatalog_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            var files = CreateTestFiles();
            CatalogRequest req = new CatalogRequest(files);
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Catalog.UpdateCatalog(req, cancelledToken)
            );

            // Cleanup
            DisposeFiles(files);
        }

        [Test]
        public void PatchCatalog_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            var files = CreateTestFiles();
            CatalogRequest req = new CatalogRequest(files);
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Catalog.PatchCatalog(req, cancelledToken)
            );

            // Cleanup
            DisposeFiles(files);
        }

        [Test]
        public void AddItemGroup_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            var itemGroup = new ConstructorItemGroup { Id = "test_group", Name = "Test Group" };
            ItemGroupsRequest req = new ItemGroupsRequest(new List<ConstructorItemGroup> { itemGroup });
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Catalog.AddItemGroup(req, cancelledToken)
            );
        }

        [Test]
        public void CreateOrReplaceItems_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            var item = TestUtils.CreateRandomItem();
            List<ConstructorItem> items = new List<ConstructorItem> { item };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Items.CreateOrReplaceItems(items, "Products", false, null, cancelledToken)
            );
        }

        [Test]
        public void DeleteItems_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            var item = TestUtils.CreateRandomItem();
            List<ConstructorItem> items = new List<ConstructorItem> { item };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Items.DeleteItems(items, "Products", cancelledToken)
            );
        }

        [Test]
        public void GetRecommendationsResults_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            RecommendationsRequest req = new RecommendationsRequest("item_page_1")
            {
                UserInfo = UserInfo,
                ItemIds = new List<string> { "power_drill" }
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Recommendations.GetRecommendationsResults(req, cancelledToken)
            );
        }

        [Test]
        public async Task GetRecommendationsResults_WithValidToken_CompletesSuccessfully()
        {
            // Arrange
            RecommendationsRequest req = new RecommendationsRequest("item_page_1")
            {
                UserInfo = UserInfo,
                ItemIds = new List<string> { "power_drill" }
            };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            // Act
            RecommendationsResponse result = await constructorio.Recommendations.GetRecommendationsResults(req, cts.Token);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ResultId);
        }

        [Test]
        public void GetNextQuestion_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            QuizRequest req = new QuizRequest("test-quiz") { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Quizzes.GetNextQuestion(req, cancelledToken)
            );
        }

        [Test]
        public void GetResults_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            QuizRequest req = new QuizRequest("test-quiz") { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Quizzes.GetResults(req, cancelledToken)
            );
        }

        [Test]
        public void GetAllTasks_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            AllTasksRequest req = new AllTasksRequest();
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Tasks.GetAllTasks(req, cancelledToken)
            );
        }

        [Test]
        public void GetTask_WithPreCancelledToken_ThrowsTaskCanceledException()
        {
            // Arrange
            TaskRequest req = new TaskRequest(123);
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var cancelledToken = new CancellationToken(true);

            // Act & Assert
            Assert.ThrowsAsync<TaskCanceledException>(
                async () => await constructorio.Tasks.GetTask(req, cancelledToken)
            );
        }

        [Test]
        public void GetAutocompleteResults_CancelledDuringExecution_ThrowsTaskCanceledException()
        {
            // Arrange
            AutocompleteRequest req = new AutocompleteRequest("item") { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            using var cts = new CancellationTokenSource();

            // Act & Assert
            var task = Assert.ThrowsAsync<TaskCanceledException>(async () =>
            {
                var apiTask = constructorio.Autocomplete.GetAutocompleteResults(req, cts.Token);
                cts.CancelAfter(50); // Cancel after 50ms
                await apiTask;
            });

            Assert.IsNotNull(task);
        }

        [Test]
        public void GetSearchResults_CancelledDuringExecution_ThrowsTaskCanceledException()
        {
            // Arrange
            SearchRequest req = new SearchRequest("item") { UserInfo = UserInfo };
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            using var cts = new CancellationTokenSource();

            // Act & Assert
            var task = Assert.ThrowsAsync<TaskCanceledException>(async () =>
            {
                var apiTask = constructorio.Search.GetSearchResults(req, cts.Token);
                cts.CancelAfter(50); // Cancel after 50ms
                await apiTask;
            });

            Assert.IsNotNull(task);
        }

        private Dictionary<string, StreamContent> CreateTestFiles()
        {
            var files = new Dictionary<string, StreamContent>();

            // Create minimal test CSV content
            string csvContent = "item_name,id\nTest Item,test_item_123";
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(csvContent));
            var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("text/csv");

            files.Add("items", streamContent);

            return files;
        }

        private void DisposeFiles(Dictionary<string, StreamContent> files)
        {
            foreach (var file in files.Values)
            {
                file?.Dispose();
            }
        }
    }
}
