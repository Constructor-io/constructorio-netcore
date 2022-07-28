using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class CatalogRequestTest
    {
        private readonly string Section = "Search Suggestions";
        private readonly string NotificationEmail = "mail@mail.mail";
        private readonly bool Force = true;
        private StreamContent itemsStream;
        private Dictionary<string, StreamContent> Files;

        [OneTimeSetUp]
        public void Setup()
        {
            itemsStream = new StreamContent(File.OpenRead("./../../../resources/csv/items.csv"));
            itemsStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            Files = new Dictionary<string, StreamContent>()
            {
                { "items", itemsStream },
            };
        }

        [Test]
        public void GetRequestParameters()
        {
            CatalogRequest req = new CatalogRequest(this.Files)
            {
                Section = this.Section,
                Force = this.Force,
                NotificationEmail = this.NotificationEmail,
            };

            Hashtable requestParameters = req.GetRequestParameters();
            Assert.AreEqual(this.Section, requestParameters[Constants.SECTION]);
            Assert.AreEqual(this.Force, requestParameters[Constants.FORCE]);
            Assert.AreEqual(this.NotificationEmail, requestParameters[Constants.NOTIFICATION_EMAIL]);
        }

        [Test]
        public void CatalogRequestWithInvalidFiles()
        {
            Assert.Throws<ArgumentException>(() => new CatalogRequest(null));
        }
    }
}
