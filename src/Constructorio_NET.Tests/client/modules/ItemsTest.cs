using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class ItemsTest
    {
        private readonly string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private ConstructorioConfig Config;

        [OneTimeSetUp]
        public async Task Setup()
        {
            JObject json = JObject.Parse(File.ReadAllText("./../../../../../.config/local.json"));
            string testApiToken = json.SelectToken("TEST_API_TOKEN").Value<string>();

            this.Config = new ConstructorioConfig(this.ApiKey)
            {
                ApiToken = testApiToken
            };

            var constructorio = new ConstructorIO(this.Config);
            List<ConstructorItem> items = new List<ConstructorItem>();
            ConstructorItem item = TestUtils.CreateRandomItem();
            items.Add(item);
            await constructorio.Items.CreateOrReplaceItems(items, "Products", false, "stanley.peng@constructor.io");

            List<ConstructorVariation> variations = new List<ConstructorVariation>();
            ConstructorVariation variation = TestUtils.CreateRandomVariation("random-id");
            variations.Add(variation);
            await constructorio.Items.CreateOrReplaceVariations(variations, "Products");
        }

        [OneTimeTearDown]
        public async Task Cleanup()
        {
            await TestUtils.Cleanup();
        }

        [Test]
        public async Task CreateItemsWithValidOptionsShouldReturnTrue()
        {
            var constructorio = new ConstructorIO(this.Config);
            List<ConstructorItem> items = new List<ConstructorItem>();
            ConstructorItem item = TestUtils.CreateRandomItem();
            items.Add(item);
            bool result = await constructorio.Items.CreateOrReplaceItems(items, "Products");
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CreateItemsWithOptionalOptionsShouldReturnTrue()
        {
            var constructorio = new ConstructorIO(this.Config);
            List<ConstructorItem> items = new List<ConstructorItem>();
            ConstructorItem item = TestUtils.CreateRandomItem();
            items.Add(item);
            bool result = await constructorio.Items.CreateOrReplaceItems(items, "Products", false, "stanley.peng@constructor.io");
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateItemsShouldReturnTrue()
        {
            var constructorio = new ConstructorIO(this.Config);
            List<ConstructorItem> items = new List<ConstructorItem>();
            ConstructorItem item = TestUtils.PeekItem();
            items.Add(item);
            bool result = await constructorio.Items.UpdateItems(items, "Products");
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateItemsWithOptionalOptionsShouldReturnTrue()
        {
            var constructorio = new ConstructorIO(this.Config);
            List<ConstructorItem> items = new List<ConstructorItem>();
            ConstructorItem item = TestUtils.PeekItem();
            items.Add(item);
            bool result = await constructorio.Items.UpdateItems(items, "Products", false, "stanley.peng@constructor.io", CatalogRequest.OnMissingStrategy.CREATE);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task RetrieveItemsShouldReturnValidItems()
        {
            var constructorio = new ConstructorIO(this.Config);
            ItemsRequest request = new ItemsRequest();
            ItemsResponse result = await constructorio.Items.RetrieveItems(request);
            Assert.IsTrue(result.TotalCount > 0);
            Assert.IsTrue(result.Items.Count > 0);
        }

        [Test]
        public async Task RetrieveItemsWithAnIdShouldReturnValidItems()
        {
            var constructorio = new ConstructorIO(this.Config);
            ItemsRequest request = new ItemsRequest();
            request.Ids = new List<string>() { "10001" };
            ItemsResponse result = await constructorio.Items.RetrieveItems(request);
            Assert.IsTrue(result.TotalCount == 1);
            Assert.IsTrue(result.Items.Count == 1);
        }

        [Test]
        public async Task RetrieveItemsWithMultipleIdsShouldReturnValidItems()
        {
            var constructorio = new ConstructorIO(this.Config);
            ItemsRequest request = new ItemsRequest();
            request.Ids = new List<string>() { "10001", "10002" };
            ItemsResponse result = await constructorio.Items.RetrieveItems(request);
            Assert.IsTrue(result.TotalCount == 2);
            Assert.IsTrue(result.Items.Count == 2);
        }

        [Test]
        public async Task DeleteItemsWithValidOptionsShouldReturnTrue()
        {
            var constructorio = new ConstructorIO(this.Config);
            List<ConstructorItem> items = new List<ConstructorItem>();
            ConstructorItem item = new ConstructorItem();
            item.Id = "testItem";
            item.Name = "testName";
            item.Url = "testurl";
            item.ImageUrl = "testimageurl";
            items.Add(item);
            bool result = await constructorio.Items.DeleteItems(items, "Products", true, "stanley.peng@constructor.io");
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteItemsWithOnlyItemsAndSectionShouldReturnTrue()
        {
            var constructorio = new ConstructorIO(this.Config);
            List<ConstructorItem> items = new List<ConstructorItem>();
            ConstructorItem item = new ConstructorItem();
            item.Id = "testItem";
            item.Name = "testName";
            item.Url = "testurl";
            item.ImageUrl = "testimageurl";
            items.Add(item);
            bool result = await constructorio.Items.DeleteItems(items, "Products");
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CreateVariationsWithValidOptionsShouldReturnTrue()
        {
            var constructorio = new ConstructorIO(this.Config);
            List<ConstructorVariation> variations = new List<ConstructorVariation>();
            ConstructorVariation variation = TestUtils.CreateRandomVariation("random-id");
            variations.Add(variation);
            bool result = await constructorio.Items.CreateOrReplaceVariations(variations, "Products");
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CreateVariationsWithOptionalOptionsShouldReturnTrue()
        {
            var constructorio = new ConstructorIO(this.Config);
            List<ConstructorVariation> variations = new List<ConstructorVariation>();
            ConstructorVariation variation = TestUtils.CreateRandomVariation("random-id");
            variations.Add(variation);
            bool result = await constructorio.Items.CreateOrReplaceVariations(variations, "Products", false, "stanley.peng@constructor.io");
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateVariationsShouldReturnTrue()
        {
            var constructorio = new ConstructorIO(this.Config);
            List<ConstructorVariation> items = new List<ConstructorVariation>();
            ConstructorVariation item = TestUtils.PeekVariation();
            items.Add(item);
            bool result = await constructorio.Items.UpdateVariations(items, "Products");
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateVariationsWithOptionalOptionsShouldReturnTrue()
        {
            var constructorio = new ConstructorIO(this.Config);
            List<ConstructorVariation> items = new List<ConstructorVariation>();
            ConstructorVariation item = TestUtils.PeekVariation();
            items.Add(item);
            bool result = await constructorio.Items.UpdateVariations(items, "Products", false, "stanley.peng@constructor.io", CatalogRequest.OnMissingStrategy.IGNORE);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task RetrieveVariationsShouldReturnValidItems()
        {
            var constructorio = new ConstructorIO(this.Config);
            VariationsRequest request = new VariationsRequest();
            VariationsResponse result = await constructorio.Items.RetrieveVariations(request);

            Assert.IsTrue(result.TotalCount > 0);
            Assert.IsTrue(result.Variations.Count > 0);
            Assert.IsTrue(!string.IsNullOrEmpty(result.Variations[0].Name));
            Assert.IsTrue(!string.IsNullOrEmpty(result.Variations[0].ItemId));
            Assert.IsTrue(!string.IsNullOrEmpty(result.Variations[0].Url));
            Assert.IsTrue(!string.IsNullOrEmpty(result.Variations[0].Id));
            Assert.IsTrue(result.Variations[0].Data.Count > 0);
            Assert.IsTrue(result.Variations[0].Facets.Count > 0);
        }

        [Test]
        public async Task RetrieveVariationsWithAnIdShouldReturnValidItems()
        {
            var constructorio = new ConstructorIO(this.Config);
            VariationsRequest request = new VariationsRequest();
            request.Ids = new List<string>() { "20001" };
            VariationsResponse result = await constructorio.Items.RetrieveVariations(request);
            Assert.IsTrue(result.TotalCount == 1);
            Assert.IsTrue(result.Variations.Count == 1);
        }

        [Test]
        public async Task RetrieveVariationsWithMultipleIdsShouldReturnValidItems()
        {
            var constructorio = new ConstructorIO(this.Config);
            VariationsRequest request = new VariationsRequest();
            request.Ids = new List<string>() { "20001", "M0E20000000E2ZJ" };
            VariationsResponse result = await constructorio.Items.RetrieveVariations(request);
            Assert.IsTrue(result.TotalCount >= 2);
            Assert.IsTrue(result.Variations.Count >= 2);
        }
    }
}
