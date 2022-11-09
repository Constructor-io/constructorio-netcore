using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Newtonsoft.Json.Linq;

namespace Constructorio_NET.Tests
{
    internal class TestUtils
    {
        private static List<ConstructorItem> CreatedItems = new List<ConstructorItem>();
        private static List<ConstructorVariation> CreatedVariations = new List<ConstructorVariation>();
        private static JObject Json = JObject.Parse(File.ReadAllText("./../../../../../.config/local.json"));
        private static string TestApiToken = Json.SelectToken("TEST_API_TOKEN").Value<string>();
        private static string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private static ConstructorioConfig Config = new ConstructorioConfig(ApiKey)
        {
            ApiToken = TestApiToken
        };

        public static async Task Cleanup()
        {
            var constructorio = new ConstructorIO(Config);
            await constructorio.Items.DeleteItems(CreatedItems, "Products");
            await constructorio.Items.DeleteVariations(CreatedVariations, "Products");
        }

        public static ConstructorItem PeekItem()
        {
            if (CreatedItems.Count > 0)
            {
                return CreatedItems[0];
            }

            return null;
        }

        public static ConstructorVariation PeekVariation()
        {
            if (CreatedVariations.Count > 0)
            {
                return CreatedVariations[0];
            }

            return null;
        }

        public static ConstructorItem CreateRandomItem()
        {
            string id = Guid.NewGuid().ToString();
            string name = id;

            ConstructorItem item = new ConstructorItem();
            item.Name = name;
            item.Id = id;

            Dictionary<string, List<string>> facets = new Dictionary<string, List<string>>();
            facets.Add("color", new List<string> { "blue", "red" });
            facets.Add("size", new List<string> { "large" });

            Dictionary<string, object> metadata = new Dictionary<string, object>();
            metadata.Add("brand", "abc");
            Dictionary<string, string> complexMetadata = new Dictionary<string, string>();
            complexMetadata.Add("test", "test");
            complexMetadata.Add("test2", "test");
            metadata.Add("complex", complexMetadata);

            item.Facets = facets;
            item.Metadata = metadata;
            item.Url = "test";

            CreatedItems.Add(item);

            return item;
        }

        public static ConstructorVariation CreateRandomVariation(string itemId)
        {
            string id = Guid.NewGuid().ToString();
            string name = id;
            ConstructorVariation item = new ConstructorVariation(id, itemId, name);
            item.Name = name;
            item.Id = id;
            Dictionary<string, List<string>> facets = new Dictionary<string, List<string>>();
            facets.Add("color", new List<string> { "blue", "red" });
            facets.Add("size", new List<string> { "large" });

            Dictionary<string, object> metadata = new Dictionary<string, object>();
            metadata.Add("brand", "abc");
            Dictionary<string, string> complexMetadata = new Dictionary<string, string>();
            complexMetadata.Add("test", "test");
            complexMetadata.Add("test2", "test");
            metadata.Add("complex", complexMetadata);

            item.Facets = facets;
            item.Metadata = metadata;
            item.Url = "test";

            CreatedVariations.Add(item);

            return item;
        }

        protected TestUtils()
        {
        }
    }
}
