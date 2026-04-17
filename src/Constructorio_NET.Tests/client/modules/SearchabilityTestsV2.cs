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
    public class SearchabilityTestsV2
    {
        private readonly string ApiKey = Environment.GetEnvironmentVariable("TEST_CATALOG_FACETS_V2_API_KEY");
        private ConstructorioConfig Config;
        private static List<string> CreatedSearchabilities = new List<string>();

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
            if (CreatedSearchabilities.Count > 0)
            {
                try
                {
                    var deleteRequest = new DeleteSearchabilitiesV2Request(CreatedSearchabilities);
                    await constructorio.Catalog.DeleteSearchabilitiesV2(deleteRequest);
                }
                catch (Exception e)
                {
                    // Do Nothing
                    Console.WriteLine(e);
                }
            }
        }

        internal static SearchabilityV2 CreateRandomSearchability()
        {
            string name = $"test_field_{Guid.NewGuid().ToString().Substring(0, 8)}";

            SearchabilityV2 searchability = new SearchabilityV2(name);
            searchability.FuzzySearchable = true;
            searchability.ExactSearchable = false;
            searchability.Displayable = true;
            searchability.Hidden = false;

            CreatedSearchabilities.Add(name);

            return searchability;
        }

        [Test]
        public async Task RetrieveSearchabilitiesV2()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var request = new RetrieveSearchabilitiesV2Request();
            SearchabilitiesV2Response response = await constructorio.Catalog.RetrieveSearchabilitiesV2(request);

            Assert.IsNotNull(response, "Response should not be null");
            Assert.IsNotNull(response.Searchabilities, "Searchabilities list should not be null");
            Assert.IsTrue(response.TotalCount >= 0, "TotalCount should be non-negative");
        }

        [Test]
        public async Task RetrieveSearchabilitiesV2WithSection()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var request = new RetrieveSearchabilitiesV2Request();
            request.Section = "Products";
            SearchabilitiesV2Response response = await constructorio.Catalog.RetrieveSearchabilitiesV2(request);

            Assert.IsNotNull(response, "Response should not be null");
        }

        [Test]
        public async Task RetrieveSearchabilitiesV2WithPagination()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var request = new RetrieveSearchabilitiesV2Request();
            request.NumResultsPerPage = 5;
            request.Page = 1;
            SearchabilitiesV2Response response = await constructorio.Catalog.RetrieveSearchabilitiesV2(request);

            Assert.IsNotNull(response, "Response should not be null");
        }

        [Test]
        public async Task RetrieveSearchabilitiesV2WithFilters()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var request = new RetrieveSearchabilitiesV2Request();
            request.FuzzySearchable = true;
            SearchabilitiesV2Response response = await constructorio.Catalog.RetrieveSearchabilitiesV2(request);

            Assert.IsNotNull(response, "Response should not be null");
        }

        [Test]
        public async Task RetrieveSearchabilitiesV2WithDisplayableFilter()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var request = new RetrieveSearchabilitiesV2Request();
            request.Displayable = true;
            SearchabilitiesV2Response response = await constructorio.Catalog.RetrieveSearchabilitiesV2(request);

            Assert.IsNotNull(response, "Response should not be null");
        }

        [Test]
        public async Task RetrieveSearchabilitiesV2WithSorting()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var request = new RetrieveSearchabilitiesV2Request();
            request.SortBy = "name";
            request.SortOrder = "ascending";
            SearchabilitiesV2Response response = await constructorio.Catalog.RetrieveSearchabilitiesV2(request);

            Assert.IsNotNull(response, "Response should not be null");
        }

        [Test]
        public async Task RetrieveSearchabilitiesV2WithNameFilter()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var request = new RetrieveSearchabilitiesV2Request();
            request.Name = "keywords";
            SearchabilitiesV2Response response = await constructorio.Catalog.RetrieveSearchabilitiesV2(request);

            Assert.IsNotNull(response, "Response should not be null");
            Assert.IsNotNull(response.Searchabilities, "Searchabilities list should not be null");
        }

        [Test]
        public void RetrieveSearchabilitiesV2_ThrowsWhenRequestNull()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await constructorio.Catalog.RetrieveSearchabilitiesV2(null));
        }

        [Test]
        public async Task PatchSearchabilitiesV2()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var searchability = CreateRandomSearchability();
            var searchabilities = new List<SearchabilityV2> { searchability };
            var request = new PatchSearchabilitiesV2Request(searchabilities);

            SearchabilitiesV2Response response = await constructorio.Catalog.PatchSearchabilitiesV2(request);

            Assert.IsNotNull(response, "Response should not be null");
            Assert.IsTrue(response.Searchabilities.Count > 0, "Should have created at least one searchability");
            Assert.IsTrue(response.TotalCount >= 0, "TotalCount should be present");

            var created = response.Searchabilities.Find(s => s.Name == searchability.Name);
            Assert.IsNotNull(created, "Created searchability should be in response");
            Assert.IsNotNull(created.CreatedAt, "CreatedAt should be set");
        }

        [Test]
        public async Task PatchSearchabilitiesV2WithSkipRebuild()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var searchability = CreateRandomSearchability();
            var searchabilities = new List<SearchabilityV2> { searchability };
            var request = new PatchSearchabilitiesV2Request(searchabilities);
            request.SkipRebuild = true;

            SearchabilitiesV2Response response = await constructorio.Catalog.PatchSearchabilitiesV2(request);

            Assert.IsNotNull(response, "Response should not be null");
            Assert.IsTrue(response.Searchabilities.Count > 0, "Should have created at least one searchability");
        }

        [Test]
        public void PatchSearchabilitiesV2_ThrowsWhenRequestNull()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await constructorio.Catalog.PatchSearchabilitiesV2(null));
        }

        [Test]
        public void PatchSearchabilitiesV2_ThrowsWhenSearchabilitiesNull()
        {
            Assert.Throws<ArgumentException>(() =>
                new PatchSearchabilitiesV2Request(null));
        }

        [Test]
        public void PatchSearchabilitiesV2_ThrowsWhenSearchabilitiesEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                new PatchSearchabilitiesV2Request(new List<SearchabilityV2>()));
        }

        [Test]
        public async Task GetSearchabilityV2()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            // First create a searchability
            var searchability = CreateRandomSearchability();
            var searchabilities = new List<SearchabilityV2> { searchability };
            var createRequest = new PatchSearchabilitiesV2Request(searchabilities);
            await constructorio.Catalog.PatchSearchabilitiesV2(createRequest);

            // Then get it by name
            SearchabilityV2 result = await constructorio.Catalog.GetSearchabilityV2(searchability.Name);

            Assert.IsNotNull(result, "Response should not be null");
            Assert.AreEqual(searchability.Name, result.Name, "Name should match");
            Assert.IsNotNull(result.CreatedAt, "CreatedAt should be set");
        }

        [Test]
        public void GetSearchabilityV2_ThrowsWhenNameEmpty()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await constructorio.Catalog.GetSearchabilityV2(string.Empty));
        }

        [Test]
        public void GetSearchabilityV2_ErrorWhenNotFound()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ConstructorException>(async () =>
                await constructorio.Catalog.GetSearchabilityV2("truly_non_existent_searchability_" + Guid.NewGuid()));
        }

        [Test]
        public async Task PatchSearchabilityV2Single()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            // First create a searchability
            var searchability = CreateRandomSearchability();
            var searchabilities = new List<SearchabilityV2> { searchability };
            var createRequest = new PatchSearchabilitiesV2Request(searchabilities);
            await constructorio.Catalog.PatchSearchabilitiesV2(createRequest);

            // Update it
            searchability.Displayable = false;
            SearchabilityV2 result = await constructorio.Catalog.PatchSearchabilityV2(searchability);

            Assert.IsNotNull(result, "Response should not be null");
            Assert.AreEqual(searchability.Name, result.Name, "Name should match");
            Assert.IsTrue(result.Displayable.HasValue, "Displayable should be set on response");
            Assert.IsFalse(result.Displayable.Value, "Displayable should be updated");
        }

        [Test]
        public async Task PatchSearchabilityV2WithSkipRebuild()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            // First create a searchability
            var searchability = CreateRandomSearchability();
            var searchabilities = new List<SearchabilityV2> { searchability };
            var createRequest = new PatchSearchabilitiesV2Request(searchabilities);
            await constructorio.Catalog.PatchSearchabilitiesV2(createRequest);

            // Update it with skipRebuild
            searchability.Hidden = true;
            SearchabilityV2 result = await constructorio.Catalog.PatchSearchabilityV2(searchability, skipRebuild: true);

            Assert.IsNotNull(result, "Response should not be null");
            Assert.AreEqual(searchability.Name, result.Name, "Name should match");
        }

        [Test]
        public void PatchSearchabilityV2_ThrowsWhenNull()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await constructorio.Catalog.PatchSearchabilityV2(null));
        }

        [Test]
        public void PatchSearchabilityV2_ThrowsWhenNameEmpty()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);
            var searchability = new SearchabilityV2();

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await constructorio.Catalog.PatchSearchabilityV2(searchability));
        }

        [Test]
        public async Task DeleteSearchabilitiesV2()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            // First create a searchability
            var searchability = CreateRandomSearchability();
            var searchabilities = new List<SearchabilityV2> { searchability };
            var createRequest = new PatchSearchabilitiesV2Request(searchabilities);
            await constructorio.Catalog.PatchSearchabilitiesV2(createRequest);

            // Delete it
            var deleteRequest = new DeleteSearchabilitiesV2Request(new List<string> { searchability.Name });
            SearchabilitiesV2Response response = await constructorio.Catalog.DeleteSearchabilitiesV2(deleteRequest);

            Assert.IsNotNull(response, "Response should not be null");

            // Remove from cleanup list since we already deleted it
            CreatedSearchabilities.Remove(searchability.Name);
        }

        [Test]
        public async Task DeleteSearchabilitiesV2WithSkipRebuild()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            // First create a searchability
            var searchability = CreateRandomSearchability();
            var searchabilities = new List<SearchabilityV2> { searchability };
            var createRequest = new PatchSearchabilitiesV2Request(searchabilities);
            await constructorio.Catalog.PatchSearchabilitiesV2(createRequest);

            // Delete it with skipRebuild
            var deleteRequest = new DeleteSearchabilitiesV2Request(new List<string> { searchability.Name });
            deleteRequest.SkipRebuild = true;
            SearchabilitiesV2Response response = await constructorio.Catalog.DeleteSearchabilitiesV2(deleteRequest);

            Assert.IsNotNull(response, "Response should not be null");

            // Remove from cleanup list since we already deleted it
            CreatedSearchabilities.Remove(searchability.Name);
        }

        [Test]
        public void DeleteSearchabilitiesV2_ThrowsWhenRequestNull()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await constructorio.Catalog.DeleteSearchabilitiesV2(null));
        }

        [Test]
        public void DeleteSearchabilitiesV2_ThrowsWhenNamesNull()
        {
            Assert.Throws<ArgumentException>(() =>
                new DeleteSearchabilitiesV2Request(null));
        }

        [Test]
        public void DeleteSearchabilitiesV2_ThrowsWhenNamesEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                new DeleteSearchabilitiesV2Request(new List<string>()));
        }

        [Test]
        public async Task DeleteSearchabilityV2Single()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            // First create a searchability
            var searchability = CreateRandomSearchability();
            var searchabilities = new List<SearchabilityV2> { searchability };
            var createRequest = new PatchSearchabilitiesV2Request(searchabilities);
            await constructorio.Catalog.PatchSearchabilitiesV2(createRequest);

            // Delete it by name
            SearchabilityV2 result = await constructorio.Catalog.DeleteSearchabilityV2(searchability.Name);

            Assert.IsNotNull(result, "Response should not be null");
            Assert.AreEqual(searchability.Name, result.Name, "Name should match");

            // Remove from cleanup list since we already deleted it
            CreatedSearchabilities.Remove(searchability.Name);
        }

        [Test]
        public async Task DeleteSearchabilityV2WithSkipRebuild()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            // First create a searchability
            var searchability = CreateRandomSearchability();
            var searchabilities = new List<SearchabilityV2> { searchability };
            var createRequest = new PatchSearchabilitiesV2Request(searchabilities);
            await constructorio.Catalog.PatchSearchabilitiesV2(createRequest);

            // Delete it by name with skipRebuild
            SearchabilityV2 result = await constructorio.Catalog.DeleteSearchabilityV2(searchability.Name, skipRebuild: true);

            Assert.IsNotNull(result, "Response should not be null");
            Assert.AreEqual(searchability.Name, result.Name, "Name should match");

            // Remove from cleanup list since we already deleted it
            CreatedSearchabilities.Remove(searchability.Name);
        }

        [Test]
        public void DeleteSearchabilityV2_ThrowsWhenNameEmpty()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await constructorio.Catalog.DeleteSearchabilityV2(string.Empty));
        }

        [Test]
        public void DeleteSearchabilityV2_ErrorWhenNotFound()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            Assert.ThrowsAsync<ConstructorException>(async () =>
                await constructorio.Catalog.DeleteSearchabilityV2("truly_non_existent_searchability_" + Guid.NewGuid()));
        }

        [Test]
        public async Task SearchabilityV2HasNewFields()
        {
            ConstructorIO constructorio = new ConstructorIO(this.Config);

            // Create a searchability with the new v2 fields
            var searchability = CreateRandomSearchability();
            searchability.Displayable = true;
            searchability.Hidden = true;

            var searchabilities = new List<SearchabilityV2> { searchability };
            var request = new PatchSearchabilitiesV2Request(searchabilities);
            SearchabilitiesV2Response response = await constructorio.Catalog.PatchSearchabilitiesV2(request);

            Assert.IsNotNull(response, "Response should not be null");
            Assert.IsTrue(response.Searchabilities.Count > 0, "Should have created at least one searchability");

            // Verify the created searchability has the new fields
            var created = response.Searchabilities.Find(s => s.Name == searchability.Name);
            Assert.IsNotNull(created, "Created searchability should be in response");
            Assert.IsNotNull(created.CreatedAt, "CreatedAt should be set");
            Assert.IsTrue(created.Displayable.HasValue && created.Displayable.Value, "Displayable should reflect the value set on creation");
            Assert.IsTrue(created.Hidden.HasValue && created.Hidden.Value, "Hidden should reflect the value set on creation");
        }
    }
}
