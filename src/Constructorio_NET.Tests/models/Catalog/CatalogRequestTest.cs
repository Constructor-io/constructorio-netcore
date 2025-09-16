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
        private Dictionary<string, StreamContent> Files;

        private StreamContent CreateItemsStream()
        {
            var stream = new StreamContent(File.OpenRead("./../../../resources/csv/items.csv"));
            stream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            return stream;
        }

        [Test]
        public void GetRequestParameters()
        {
            Files = new Dictionary<string, StreamContent>()
            {
                { "items", CreateItemsStream() },
            };
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
