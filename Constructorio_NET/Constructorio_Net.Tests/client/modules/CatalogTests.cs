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
        private string url = "https://raw.githubusercontent.com/Constructor-io/integration-examples/main/catalog/";

        [SetUp]
        public void Setup()
        {
            this.Options = new Hashtable()
            {
                { "apiKey", this.ApiKey }
            };
            this.UserParameters = new Hashtable()
            {
                { "clientId", ClientId },
                { "sessionId", SessionId }
            };
        }

        // [Test]
        // public void GetSearchResults()
        // {
        //     StreamContent stream = new StreamContent(File.OpenRead("../../../items.csv"));
        //     stream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");

        //     Dictionary<string, string> files = new Dictionary<string, string>() {
        //         { "items", "../../../items.csv" }
        //     };
        //     ConstructorIO constructorio = new ConstructorIO(this.Options);
        //     CatalogRequest req = new CatalogRequest(files);
        //     string res = constructorio.Catalog.CreateCatalogUrl(req);
        // }
    }
}