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
    public class FacetOptionTest
    {
        private readonly string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private readonly string facetGroupName = "test_facet_options";
        private ConstructorioConfig Config;
        private static List<FacetOption> CreatedFacetOptions = new List<FacetOption>();

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
            foreach (FacetOption facetOption in CreatedFacetOptions)
            {
                try
                {
                    await constructorio.Catalog.DeleteFacetOption(facetOption.Value, this.facetGroupName);
                }
                catch (Exception e)
                {
                    // Do nothing
                    Console.WriteLine(e);
                }
            }
        }

        internal static FacetOption CreateRandomFacetOption()
        {
            string value = Guid.NewGuid().ToString();

            FacetOption facetOption = new FacetOption(value);
            facetOption.DisplayName = "test";
            facetOption.Hidden = true;

            CreatedFacetOptions.Add(facetOption);

            return facetOption;
        }

        [Test]
        public async Task CreateFacetOption()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetOption facetOption = CreateRandomFacetOption();

            FacetOption facetOptionCreated = await constructorio.Catalog.CreateFacetOption(facetOption, this.facetGroupName);
            Assert.AreEqual(facetOption.Value, facetOptionCreated.Value, "Facet Option created with incorrect Value");
            Assert.AreEqual(facetOption.DisplayName, facetOptionCreated.DisplayName, "Facet Option created with incorrect Display Name");
            Assert.AreEqual(facetOption.Hidden, facetOptionCreated.Hidden, "Facet Option created with incorrect Hidden attribute");
        }

        [Test]
        public async Task DeleteFacetOption()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetOption facetOption = CreateRandomFacetOption();

            await constructorio.Catalog.CreateFacetOption(facetOption, this.facetGroupName);

            FacetOption facetOptionDeleted = await constructorio.Catalog.DeleteFacetOption(facetOption.Value, this.facetGroupName);
            Assert.AreEqual(facetOption.Value, facetOptionDeleted.Value, "Facet Option created with incorrect Value");
            Assert.AreEqual(facetOption.DisplayName, facetOptionDeleted.DisplayName, "Facet Option created with incorrect Display Name");
            Assert.AreEqual(facetOption.Hidden, facetOptionDeleted.Hidden, "Facet Option created with incorrect Hidden attribute");

            CreatedFacetOptions.RemoveAt(CreatedFacetOptions.Count - 1);
        }

        [Test]
        public async Task GetAllFacetOptions()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetOption facetOption = CreateRandomFacetOption();
            await constructorio.Catalog.CreateFacetOption(facetOption, this.facetGroupName);

            FacetOptionsGetAllResponse facetOptionsResponse = await constructorio.Catalog.GetAllFacetOptions(this.facetGroupName);

            Assert.IsTrue(facetOptionsResponse.FacetOptions.Count > 0, "No Facet Options fetched");
        }

        [Test]
        public async Task GetAllFacetConfigsWithPagination()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetOption facetOption;
            for (int i = 0; i < 4; ++i)
            {
                facetOption = CreateRandomFacetOption();
                await constructorio.Catalog.CreateFacetOption(facetOption, this.facetGroupName);
            }

            PaginationOptions pagination = new PaginationOptions();
            pagination.NumResultsPerPage = 2;
            pagination.Offset = 2;
            FacetGetAllResponse facetResponse = await constructorio.Catalog.GetAllFacetConfigs(pagination);

            Assert.IsTrue(facetResponse.Facets.Count == 2, "GetAllFacetConfigsWithPagination returns incorrect num_results_per_page");
        }

        [Test]
        public async Task BatchCreateFacetOptions()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            List<FacetOption> newFacetOptions = new List<FacetOption>();
            for (int i = 0; i < 2; ++i)
            {
                FacetOption facetOption = CreateRandomFacetOption();
                facetOption.DisplayName = "Facet " + i.ToString();
                newFacetOptions.Add(facetOption);
            }

            List<FacetOption> createdFacets = await constructorio.Catalog.BatchCreateOrUpdateFacetOptions(newFacetOptions, this.facetGroupName);
            FacetOption facetOption0 = createdFacets.Find((f) => f.Value == newFacetOptions[0].Value);
            FacetOption facetOption1 = createdFacets.Find((f) => f.Value == newFacetOptions[1].Value);

            Assert.AreEqual(newFacetOptions[0].DisplayName, facetOption0.DisplayName, "Created Facet Option has incorrect Display Name");
            Assert.AreEqual(newFacetOptions[1].DisplayName, facetOption1.DisplayName, "Created Facet Option has incorrect Display Name");
        }

        [Test]
        public async Task BatchUpdateFacetOptions()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            System.Collections.Hashtable data = new System.Collections.Hashtable();
            data.Add("hello", 3);
            List<FacetOption> newFacetOptions = new List<FacetOption>();
            for (int i = 0; i < 2; ++i)
            {
                FacetOption facetOption = CreateRandomFacetOption();
                facetOption.Position = 1;
                facetOption.Data = data;
                newFacetOptions.Add(facetOption);
            }

            await constructorio.Catalog.BatchCreateOrUpdateFacetOptions(newFacetOptions, this.facetGroupName);

            newFacetOptions[0].DisplayName = "0";
            newFacetOptions[0].Position = null;
            newFacetOptions[0].Data = null;

            newFacetOptions[1].DisplayName = "1";
            newFacetOptions[1].Position = -1;
            newFacetOptions[1].Data = new System.Collections.Hashtable();

            List<FacetOption> updatedFacets = await constructorio.Catalog.BatchCreateOrUpdateFacetOptions(newFacetOptions, this.facetGroupName);
            FacetOption facetOption0 = updatedFacets.Find((f) => f.Value == newFacetOptions[0].Value);
            FacetOption facetOption1 = updatedFacets.Find((f) => f.Value == newFacetOptions[1].Value);

            Assert.AreEqual("0", facetOption0.DisplayName, "Updated Facet Option has incorrectly updated Display Name");
            Assert.AreEqual(1, facetOption0.Position, "Updated Facet Option incorrectly updated Position");
            Assert.AreEqual(data, facetOption0.Data, "Updated Facet Option incorrectly updated Data");
            Assert.AreEqual("1", facetOption1.DisplayName, "Updated Facet Option has incorrectly updated Display Name");
            Assert.AreEqual(null, facetOption1.Position, "Updated Facet Option has incorrectly updated Position");
            Assert.AreEqual(new System.Collections.Hashtable(), facetOption1.Data, "Updated Facet Option has incorrectly updated Data");
        }

        [Test]
        public async Task GetFacetOption()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetOption facetOption = CreateRandomFacetOption();
            await constructorio.Catalog.CreateFacetOption(facetOption, this.facetGroupName);

            FacetOption fetchedFacetOption = await constructorio.Catalog.GetFacetOption(facetOption.Value, this.facetGroupName);

            Assert.AreEqual(facetOption.DisplayName, fetchedFacetOption.DisplayName, "Fetched Facet Option has different Display Name");
            Assert.AreEqual(facetOption.Value, fetchedFacetOption.Value, "Fetched Facet Option has different Value");
            Assert.AreEqual(facetOption.Hidden, fetchedFacetOption.Hidden, "Fetched Facet Option has different Hidden value");
        }

        [Test]
        public async Task ReplaceFacetOption()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetOption facetOption = CreateRandomFacetOption();
            await constructorio.Catalog.CreateFacetOption(facetOption, this.facetGroupName);

            facetOption.DisplayName = "updated";
            facetOption.Position = 2;
            facetOption.Hidden = false;
            facetOption.Data = new System.Collections.Hashtable();
            facetOption.Data.Add("hello", 3);

            FacetOption replacedFacetOption = await constructorio.Catalog.ReplaceFacetOption(facetOption, this.facetGroupName);

            Assert.AreEqual(facetOption.DisplayName, replacedFacetOption.DisplayName, "Replaced Facet Option Display Name was not updated");
            Assert.AreEqual(facetOption.Position, replacedFacetOption.Position, "Replaced Facet Option Position was not updated");
            Assert.AreEqual(facetOption.Hidden, replacedFacetOption.Hidden, "Replaced Facet Option Hidden was not updated");
            Assert.AreEqual(facetOption.Data, replacedFacetOption.Data, "Replaced Facet Option Data was not updated");
        }

        [Test]
        public async Task ReplaceFacetOptionDefaults()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetOption facetOption = CreateRandomFacetOption();
            await constructorio.Catalog.CreateFacetOption(facetOption, this.facetGroupName);

            facetOption.DisplayName = null;
            facetOption.Position = null;
            facetOption.Hidden = null;
            facetOption.Data = null;

            FacetOption replacedFacetOption = await constructorio.Catalog.ReplaceFacetOption(facetOption, this.facetGroupName);

            Assert.AreEqual(facetOption.Value, replacedFacetOption.Value, "Replaced Facet Option has incorrect Value");
            Assert.AreEqual(null, replacedFacetOption.DisplayName, "Replaced Facet Option has non-default Display Name");
            Assert.AreEqual(null, replacedFacetOption.Position, "Replaced Facet Option has non-default Position");
            Assert.AreEqual(false, replacedFacetOption.Hidden, "Replaced Facet Option has non-default Hidden field");
            Assert.AreEqual(null, replacedFacetOption.Data, "Replaced Facet Option has non-default Data field");
        }

        [Test]
        public async Task PartiallyUpdateFacetOption()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            FacetOption facetOption = CreateRandomFacetOption();
            System.Collections.Hashtable data = new System.Collections.Hashtable();
            data.Add("hello", 3);
            facetOption.Data = data;
            facetOption.Position = 2;
            facetOption.Hidden = false;

            await constructorio.Catalog.CreateFacetOption(facetOption, this.facetGroupName);

            facetOption.DisplayName = "updated";
            facetOption.Position = null;
            facetOption.Hidden = null;
            facetOption.Data = null;

            FacetOption updatedFacetOption = await constructorio.Catalog.PartiallyUpdateFacetOption(facetOption, this.facetGroupName);

            Assert.AreEqual(facetOption.DisplayName, updatedFacetOption.DisplayName, "Display Name should have been updated");
            Assert.AreEqual(2, updatedFacetOption.Position, "Position should not have been updated");
            Assert.AreEqual(false, updatedFacetOption.Hidden, "Hidden field should not have been updated");
            Assert.AreEqual(data, updatedFacetOption.Data, "Data field should not have been updated");
        }
    }
}