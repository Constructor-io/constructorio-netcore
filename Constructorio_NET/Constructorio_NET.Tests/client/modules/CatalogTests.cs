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
            var myJObject = JObject.Parse(File.ReadAllText("./../../../../../.config/local.json"));
            string testApiToken = myJObject.SelectToken("TEST_API_TOKEN").Value<string>();

            this.Options = new Hashtable()
            {
                { Constants.API_KEY, this.ApiKey },
                { "apiToken", testApiToken },
            };
            this.UserParameters = new Hashtable()
            {
                { "clientId", ClientId },
                { "sessionId", SessionId }
            };
        }

        [Test]
        public void ReplaceCatalog()
        {
            StreamContent stream = new StreamContent(File.OpenRead("./../../../resources/csv/items.csv"));
            stream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "items", stream }
            };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            CatalogRequest req = new CatalogRequest(files);
            constructorio.Catalog.ReplaceCatalog(req);
        }

        [Test]
        public void UpdateCatalog()
        {
            StreamContent stream = new StreamContent(File.OpenRead("./../../../resources/csv/items.csv"));
            stream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "items", stream }
            };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            CatalogRequest req = new CatalogRequest(files);
            constructorio.Catalog.UpdateCatalog(req);
        }

        [Test]
        public void PatchCatalog()
        {
            StreamContent stream = new StreamContent(File.OpenRead("./../../../resources/csv/items.csv"));
            stream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
            {
                { "items", stream }
            };
            ConstructorIO constructorio = new ConstructorIO(this.Options);
            CatalogRequest req = new CatalogRequest(files);
            constructorio.Catalog.PatchCatalog(req);
        }
    }
}