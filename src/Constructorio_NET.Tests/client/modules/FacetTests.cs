using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class FacetsTest
    {
        private readonly string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private ConstructorioConfig Config;
        private static List<Facet> CreatedFacets = new List<Facet>();

        [OneTimeSetUp]
        public void Setup()
        {
            JObject json = JObject.Parse(File.ReadAllText("./../../../../../.config/local.json"));
            string testApiToken = json.SelectToken("TEST_API_TOKEN").Value<string>();
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
            foreach (Facet facet in CreatedFacets)
            {
                try
                {
                    await constructorio.Catalog.DeleteFacetConfig(facet.Name);
                }
                catch (Exception e)
                {
                    // Do Nothing
                    Console.WriteLine(e);
                }
            }
        }

        internal static Facet CreateRandomFacet(FacetType facetType)
        {
            string name = Guid.NewGuid().ToString();

            Facet facet = new Facet(name, facetType);

            CreatedFacets.Add(facet);

            return facet;
        }

        [Test]
        public async Task CreateFacetConfig()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            Facet facet = CreateRandomFacet(FacetType.Multiple);
            Facet createdFacet = await constructorio.Catalog.CreateFacetConfig(facet);

            Assert.IsNotNull(createdFacet, "Facet not created");
            Assert.AreEqual(facet.Name, createdFacet.Name, message: "Facet created is different");
        }

        [Test]
        public async Task AddFacetConfigWithSection()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            Facet facet = CreateRandomFacet(FacetType.Multiple);
            Facet createdFacet = await constructorio.Catalog.CreateFacetConfig(facet, "Products");

            Assert.IsNotNull(createdFacet, "Facet not created");
            Assert.AreEqual(facet.Name, createdFacet.Name, message: "Facet created is different");
        }

        [Test]
        public async Task AddRangedFacetConfig()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            Facet facet = CreateRandomFacet(FacetType.Range);
            facet.RangeType = FacetRangeType.Static;
            facet.RangeFormat = FacetRangeFormat.Boundaries;
            Facet createdFacet = await constructorio.Catalog.CreateFacetConfig(facet, "Products");

            Assert.IsNotNull(createdFacet, "Facet not created");
            Assert.AreEqual(facet.Name, createdFacet.Name, message: "Facet created is different");
        }

        [Test]
        public async Task AddRangedFacetConfigWithRangeFormatMissing()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            Facet facet = CreateRandomFacet(FacetType.Range);
            facet.RangeType = FacetRangeType.Static;

            bool success;
            try
            {
                await constructorio.Catalog.CreateFacetConfig(facet, "Products");
                success = true;
            }
            catch
            {
                success = false;
            }

            Assert.IsFalse(success, "Facet wrongly created");
        }

        [Test]
        public async Task GetAllFacetConfigs()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetGetAllResponse facetResponse = await constructorio.Catalog.GetAllFacetConfigs();

            Assert.IsTrue(facetResponse.Facets.Count > 0, "No facets fetched");
        }

        [Test]
        public async Task GetAllFacetConfigsWithSection()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetGetAllResponse facetResponse = await constructorio.Catalog.GetAllFacetConfigs(section: "Products");

            Assert.IsTrue(facetResponse.Facets.Count > 0, "No facets fetched");
        }

        [Test]
        public async Task GetAllFacetConfigsWithPagination()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            PageRequest pagination = new PageRequest();
            pagination.NumResultsPerPage = 2;
            pagination.Offset = 2;
            FacetGetAllResponse facetResponse = await constructorio.Catalog.GetAllFacetConfigs(pagination);

            Assert.IsTrue(facetResponse.Facets.Count == 2, "GetAllFacetConfigsWithPagination returns incorrect num_results_per_page");
        }

        [Test]
        public async Task GetFacetConfig()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            Facet facetToFetch = CreateRandomFacet(FacetType.Multiple);
            await constructorio.Catalog.CreateFacetConfig(facetToFetch);

            Facet facet = await constructorio.Catalog.GetFacetConfig(facetToFetch.Name);

            Assert.AreEqual(facet.Name, facetToFetch.Name, "Incorrect facet fetched");
        }

        [Test]
        public async Task BatchPartiallyUpdateFacetConfigs()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            List<Facet> newFacets = new List<Facet>();
            Facet facet;
            for (var i = 0; i < 3; ++i)
            {
                facet = CreateRandomFacet(FacetType.Multiple);
                facet.DisplayName = "unchanged";
                facet.Position = 1;
                facet.RangeInclusive = FacetRangeInclusive.Below;
                await constructorio.Catalog.CreateFacetConfig(facet);
                newFacets.Add(facet);
            }

            newFacets[0].DisplayName = "changed";
            newFacets[1].Position = 2;
            newFacets[2].Type = FacetType.Range;
            newFacets[2].RangeFormat = FacetRangeFormat.Boundaries;
            newFacets[2].Position = -1;
            newFacets[2].RangeInclusive = FacetRangeInclusive.Null;

            List<Facet> changedFacets = await constructorio.Catalog.BatchPartiallyUpdateFacetConfigs(newFacets);

            Assert.AreEqual(newFacets[0].DisplayName, changedFacets[0].DisplayName, "Facet display name not changed correctly");
            Assert.AreEqual(newFacets[1].Position, changedFacets[1].Position, "Facet position not changed correctly");
            Assert.AreEqual(newFacets[2].Type, changedFacets[2].Type, "Facet type not changed to range correctly");
            Assert.AreEqual(newFacets[2].Position, changedFacets[2].Position, "Facet Postition not set to null");
            Assert.AreEqual(newFacets[2].RangeInclusive, changedFacets[2].RangeInclusive, "Facet RangeInclusive not set to null");
        }

        [Test]
        public async Task PartiallyUpdateFacetConfig()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            Facet facet = CreateRandomFacet(FacetType.Multiple);
            facet.DisplayName = "unchanged";
            facet.Position = 1;
            facet.Type = FacetType.Multiple;
            await constructorio.Catalog.CreateFacetConfig(facet);

            facet.DisplayName = "changed";
            facet.Position = 2;
            facet.Type = FacetType.Range;
            facet.RangeFormat = FacetRangeFormat.Boundaries;

            Facet changedFacet = await constructorio.Catalog.PartiallyUpdateFacetConfig(facet);

            Assert.AreEqual(facet.DisplayName, changedFacet.DisplayName, "Facet display name not changed correctly");
            Assert.AreEqual(facet.Position, changedFacet.Position, "Facet position not changed correctly");
            Assert.AreEqual(facet.Type, changedFacet.Type, "Facet type not changed to range correctly");
        }

        [Test]
        public async Task PartiallyUpdateFacetConfigWithNullValues()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            Facet facet = CreateRandomFacet(FacetType.Multiple);
            facet.DisplayName = "unchanged";
            facet.Position = 1;
            facet.RangeInclusive = FacetRangeInclusive.Above;
            await constructorio.Catalog.CreateFacetConfig(facet);

            facet.DisplayName = "changed";
            facet.Position = -1;
            facet.RangeInclusive = FacetRangeInclusive.Null;

            Facet changedFacet = await constructorio.Catalog.PartiallyUpdateFacetConfig(facet);

            Assert.AreEqual(facet.DisplayName, changedFacet.DisplayName, "Facet display name not changed correctly");
            Assert.AreEqual(null, changedFacet.RangeInclusive, "Facet position not changed correctly");
            Assert.AreEqual(null, changedFacet.Position, "Facet type not changed to range correctly");
        }

        [Test]
        public async Task UpdateFacetConfig()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            Facet facet = CreateRandomFacet(FacetType.Multiple);
            facet.DisplayName = "unchanged";
            facet.Position = 1;
            facet.RangeInclusive = FacetRangeInclusive.Below;
            await constructorio.Catalog.CreateFacetConfig(facet);

            facet.DisplayName = "changed";
            facet.Position = null;
            facet.RangeInclusive = null;

            Facet newFacet = await constructorio.Catalog.UpdateFacetConfig(facet);

            Assert.AreEqual("changed", newFacet.DisplayName, "Display Name not properly updated.");
            Assert.AreEqual(null, newFacet.Position, "Position should be default value (null)");
            Assert.AreEqual(null, newFacet.RangeInclusive, "Range Inclusive should be default value (null)");
        }

        [Test]
        public async Task DeleteFacetConfig()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            Facet facetToDelete = CreateRandomFacet(FacetType.Multiple);
            await constructorio.Catalog.CreateFacetConfig(facetToDelete);

            Facet deletedFacet = await constructorio.Catalog.DeleteFacetConfig(facetToDelete.Name);

            Assert.AreEqual(deletedFacet.Name, facetToDelete.Name, "Wrong facet deleted");
            CreatedFacets.RemoveAt(CreatedFacets.Count - 1);
        }

        [Test]
        public async Task DeleteFacetConfigWithSection()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            Facet facetToDelete = CreateRandomFacet(FacetType.Multiple);
            await constructorio.Catalog.CreateFacetConfig(facetToDelete, "Products");

            Facet deletedFacet = await constructorio.Catalog.DeleteFacetConfig(facetToDelete.Name, "Products");

            Assert.AreEqual(deletedFacet.Name, facetToDelete.Name, "Wrong facet deleted");
            CreatedFacets.RemoveAt(CreatedFacets.Count - 1);
        }
    }
}