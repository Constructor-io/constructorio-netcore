using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class FacetsTestV2
    {
        private readonly string ApiKey = Environment.GetEnvironmentVariable("TEST_CATALOG_FACETS_V2_API_KEY");
        private ConstructorioConfig Config;
        private static List<FacetV2> CreatedFacets = new List<FacetV2>();

        [OneTimeSetUp]
        public void Setup()
        {
            JObject json = JObject.Parse(File.ReadAllText("./../../../../../.config/local.json"));
            string testApiToken = json.SelectToken("TEST_API_TOKEN").Value<string>();
            this.Config = new ConstructorioConfig(this.ApiKey, testApiToken);
        }

        [SetUp]
        public async Task Delay()
        {
            await Task.Delay(1000);
        }

        [OneTimeTearDown]
        public async Task Cleanup()
        {
            var constructorio = new ConstructorIO(Config);
            foreach (FacetV2 facet in CreatedFacets)
            {
                try
                {
                    await constructorio.Catalog.DeleteFacetConfigV2(facet.Name);
                }
                catch (Exception e)
                {
                    // Do Nothing
                    Console.WriteLine(e);
                }
            }
        }

        internal static FacetV2 CreateRandomFacet(FacetTypeV2 facetType)
        {
            string name = Guid.NewGuid().ToString();
            string pathInMetadata = $"metadata.{name}";

            FacetV2 facet = new FacetV2(name, pathInMetadata, facetType);

            CreatedFacets.Add(facet);

            return facet;
        }

        [Test]
        public async Task CreateFacetConfigV2()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetV2 facet = CreateRandomFacet(FacetTypeV2.Multiple);
            FacetV2 createdFacet = await constructorio.Catalog.CreateFacetConfigV2(facet);

            Assert.IsNotNull(createdFacet, "Facet not created");
            Assert.AreEqual(facet.Name, createdFacet.Name, message: "Facet created is different");
            Assert.AreEqual(facet.PathInMetadata, createdFacet.PathInMetadata, message: "PathInMetadata is different");
        }

        [Test]
        public async Task CreateFacetConfigV2WithSection()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetV2 facet = CreateRandomFacet(FacetTypeV2.Multiple);
            FacetV2 createdFacet = await constructorio.Catalog.CreateFacetConfigV2(facet, "Products");

            Assert.IsNotNull(createdFacet, "Facet not created");
            Assert.AreEqual(facet.Name, createdFacet.Name, message: "Facet created is different");
        }

        [Test]
        public async Task CreateRangedFacetConfigV2()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetV2 facet = CreateRandomFacet(FacetTypeV2.Range);
            facet.RangeType = FacetRangeType.Static;
            facet.RangeFormat = FacetRangeFormat.Boundaries;
            FacetV2 createdFacet = await constructorio.Catalog.CreateFacetConfigV2(facet, "Products");

            Assert.IsNotNull(createdFacet, "Facet not created");
            Assert.AreEqual(facet.Name, createdFacet.Name, message: "Facet created is different");
        }

        [Test]
        public async Task CreateHierarchicalFacetConfigV2()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetV2 facet = CreateRandomFacet(FacetTypeV2.Hierarchical);
            FacetV2 createdFacet = await constructorio.Catalog.CreateFacetConfigV2(facet, "Products");

            Assert.IsNotNull(createdFacet, "Facet not created");
            Assert.AreEqual(facet.Name, createdFacet.Name, message: "Facet created is different");
            Assert.AreEqual(FacetTypeV2.Hierarchical, createdFacet.Type, "Facet type should be hierarchical");
        }

        [Test]
        public void CreateFacetConfigV2_ThrowsWhenNullFacet()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await constructorio.Catalog.CreateFacetConfigV2(null));
        }

        [Test]
        public async Task CreateFacetConfigV2_ErrorWhenAlreadyExists()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetV2 facet = CreateRandomFacet(FacetTypeV2.Multiple);
            await constructorio.Catalog.CreateFacetConfigV2(facet);

            Assert.ThrowsAsync<ConstructorException>(async () =>
                await constructorio.Catalog.CreateFacetConfigV2(facet));
        }

        [Test]
        public async Task GetAllFacetConfigsV2()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetV2GetAllResponse facetResponse = await constructorio.Catalog.GetAllFacetConfigsV2();

            Assert.IsTrue(facetResponse.Facets.Count > 0 || facetResponse.TotalCount >= 0, "Response should contain facets array");
        }

        [Test]
        public async Task GetAllFacetConfigsV2WithSection()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetV2GetAllResponse facetResponse = await constructorio.Catalog.GetAllFacetConfigsV2(section: "Products");

            Assert.IsNotNull(facetResponse, "Response should not be null");
        }

        [Test]
        public async Task GetAllFacetConfigsV2WithPagination()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            PaginationOptions pagination = new PaginationOptions();
            pagination.NumResultsPerPage = 2;
            pagination.Offset = 0;
            FacetV2GetAllResponse facetResponse = await constructorio.Catalog.GetAllFacetConfigsV2(pagination);

            Assert.IsNotNull(facetResponse, "Response should not be null");
            Assert.AreEqual(2, facetResponse.Facets.Count, "Should respect NumResultsPerPage");
        }

        [Test]
        public async Task GetAllFacetConfigsV2WithOffset()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            PaginationOptions pagination = new PaginationOptions();
            pagination.Offset = 0;
            pagination.NumResultsPerPage = 10;
            FacetV2GetAllResponse facetResponse = await constructorio.Catalog.GetAllFacetConfigsV2(pagination);

            Assert.IsNotNull(facetResponse, "Response should not be null");
            Assert.IsNotNull(facetResponse.Facets, "Facets array should not be null");
        }

        [Test]
        public async Task GetFacetConfigV2()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetV2 facetToFetch = CreateRandomFacet(FacetTypeV2.Multiple);
            await constructorio.Catalog.CreateFacetConfigV2(facetToFetch);

            FacetV2 facet = await constructorio.Catalog.GetFacetConfigV2(facetToFetch.Name);

            Assert.AreEqual(facet.Name, facetToFetch.Name, "Incorrect facet fetched");
            Assert.IsNotNull(facet.PathInMetadata, "PathInMetadata should be set");
        }

        [Test]
        public void GetFacetConfigV2_ThrowsWhenNameEmpty()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await constructorio.Catalog.GetFacetConfigV2(string.Empty));
        }

        [Test]
        public void GetFacetConfigV2_ErrorWhenNotFound()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ConstructorException>(async () =>
                await constructorio.Catalog.GetFacetConfigV2("non-existent-facet-" + Guid.NewGuid()));
        }

        [Test]
        public async Task PartiallyUpdateFacetConfigV2()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetV2 facet = CreateRandomFacet(FacetTypeV2.Multiple);
            facet.DisplayName = "unchanged";
            facet.Position = 1;
            await constructorio.Catalog.CreateFacetConfigV2(facet);

            facet.DisplayName = "changed";
            facet.Position = 2;

            FacetV2 changedFacet = await constructorio.Catalog.PartiallyUpdateFacetConfigV2(facet);

            Assert.AreEqual(facet.DisplayName, changedFacet.DisplayName, "Facet display name not changed correctly");
            Assert.AreEqual(facet.Position, changedFacet.Position, "Facet position not changed correctly");
            Assert.AreEqual(facet.PathInMetadata, changedFacet.PathInMetadata, "PathInMetadata should be preserved");
        }

        [Test]
        public void PartiallyUpdateFacetConfigV2_ThrowsWhenNull()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await constructorio.Catalog.PartiallyUpdateFacetConfigV2(null));
        }

        [Test]
        public void PartiallyUpdateFacetConfigV2_ThrowsWhenNameEmpty()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetV2 facet = new FacetV2();

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await constructorio.Catalog.PartiallyUpdateFacetConfigV2(facet));
        }

        [Test]
        public async Task ReplaceFacetConfigV2()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetV2 facet = CreateRandomFacet(FacetTypeV2.Multiple);
            facet.DisplayName = "unchanged";
            facet.Position = 1;
            await constructorio.Catalog.CreateFacetConfigV2(facet);

            facet.DisplayName = "changed";
            facet.Position = null;

            FacetV2 newFacet = await constructorio.Catalog.ReplaceFacetConfigV2(facet);

            Assert.AreEqual("changed", newFacet.DisplayName, "Display Name not properly updated.");
            Assert.AreEqual(facet.Name, newFacet.Name, "Name should match");
            Assert.AreEqual(facet.PathInMetadata, newFacet.PathInMetadata, "PathInMetadata should match");
        }

        [Test]
        public void ReplaceFacetConfigV2_ThrowsWhenNull()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await constructorio.Catalog.ReplaceFacetConfigV2(null));
        }

        [Test]
        public void ReplaceFacetConfigV2_ThrowsWhenNameEmpty()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetV2 facet = new FacetV2();

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await constructorio.Catalog.ReplaceFacetConfigV2(facet));
        }

        [Test]
        public async Task DeleteFacetConfigV2()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetV2 facetToDelete = CreateRandomFacet(FacetTypeV2.Multiple);
            await constructorio.Catalog.CreateFacetConfigV2(facetToDelete);

            FacetV2 deletedFacet = await constructorio.Catalog.DeleteFacetConfigV2(facetToDelete.Name);

            Assert.AreEqual(deletedFacet.Name, facetToDelete.Name, "Wrong facet deleted");
            CreatedFacets.RemoveAt(CreatedFacets.Count - 1);
        }

        [Test]
        public async Task DeleteFacetConfigV2WithSection()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetV2 facetToDelete = CreateRandomFacet(FacetTypeV2.Multiple);
            await constructorio.Catalog.CreateFacetConfigV2(facetToDelete, "Products");

            FacetV2 deletedFacet = await constructorio.Catalog.DeleteFacetConfigV2(facetToDelete.Name, "Products");

            Assert.AreEqual(deletedFacet.Name, facetToDelete.Name, "Wrong facet deleted");
            CreatedFacets.RemoveAt(CreatedFacets.Count - 1);
        }

        [Test]
        public void DeleteFacetConfigV2_ThrowsWhenNameEmpty()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await constructorio.Catalog.DeleteFacetConfigV2(string.Empty));
        }

        [Test]
        public void DeleteFacetConfigV2_ErrorWhenNotFound()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ConstructorException>(async () =>
                await constructorio.Catalog.DeleteFacetConfigV2("non-existent-facet-" + Guid.NewGuid()));
        }

        [Test]
        public async Task CreateFacetConfigV2WithNewFields()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetV2 facet = CreateRandomFacet(FacetTypeV2.Multiple);
            facet.Countable = false;
            facet.OptionsLimit = 100;
            FacetV2 createdFacet = await constructorio.Catalog.CreateFacetConfigV2(facet);

            Assert.IsNotNull(createdFacet, "Facet not created");
            Assert.AreEqual(facet.Name, createdFacet.Name, message: "Facet created is different");
            Assert.IsNotNull(createdFacet.CreatedAt, "CreatedAt should be set");
        }

        [Test]
        public async Task BatchPartiallyUpdateFacetConfigsV2()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            List<FacetV2> newFacets = new List<FacetV2>();
            FacetV2 facet;
            for (var i = 0; i < 3; ++i)
            {
                facet = CreateRandomFacet(FacetTypeV2.Multiple);
                facet.DisplayName = "unchanged";
                facet.Position = 1;
                await constructorio.Catalog.CreateFacetConfigV2(facet);
                newFacets.Add(facet);
            }

            newFacets[0].DisplayName = "changed";
            newFacets[1].Position = 2;
            newFacets[2].DisplayName = "also_changed";

            List<FacetV2> changedFacets = await constructorio.Catalog.BatchPartiallyUpdateFacetConfigsV2(newFacets);

            Assert.AreEqual(newFacets[0].DisplayName, changedFacets[0].DisplayName, "Facet display name not changed correctly");
            Assert.AreEqual(newFacets[1].Position, changedFacets[1].Position, "Facet position not changed correctly");
            Assert.AreEqual(newFacets[2].DisplayName, changedFacets[2].DisplayName, "Facet display name not changed correctly");
        }

        [Test]
        public void BatchPartiallyUpdateFacetConfigsV2_ThrowsWhenNull()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await constructorio.Catalog.BatchPartiallyUpdateFacetConfigsV2(null));
        }

        [Test]
        public void BatchPartiallyUpdateFacetConfigsV2_ThrowsWhenEmpty()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await constructorio.Catalog.BatchPartiallyUpdateFacetConfigsV2(new List<FacetV2>()));
        }

        [Test]
        public async Task CreateOrReplaceFacetConfigsV2()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            List<FacetV2> newFacets = new List<FacetV2>();

            for (var i = 0; i < 2; ++i)
            {
                FacetV2 facet = CreateRandomFacet(FacetTypeV2.Multiple);
                facet.DisplayName = "original";
                newFacets.Add(facet);
            }

            List<FacetV2> createdFacets = await constructorio.Catalog.CreateOrReplaceFacetConfigsV2(newFacets);

            Assert.IsNotNull(createdFacets, "Response facets should not be null");
        }

        [Test]
        public void CreateOrReplaceFacetConfigsV2_ThrowsWhenNull()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await constructorio.Catalog.CreateOrReplaceFacetConfigsV2(null));
        }

        [Test]
        public void CreateOrReplaceFacetConfigsV2_ThrowsWhenEmpty()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await constructorio.Catalog.CreateOrReplaceFacetConfigsV2(new List<FacetV2>()));
        }
    }
}
