using System;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class ConstructorioConfigTest : Helpers
    {
        private readonly string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
        private readonly string ApiToken = "API TOKEN";
        private readonly string ConstructorToken = "WAF TOKEN";
        private readonly string ServiceUrl = "https://constructor.net";

        [Test]
        public void ConstructorioConfigWithValidParams()
        {
            ConstructorioConfig config = new ConstructorioConfig(this.ApiKey, this.ApiToken)
            {
                ServiceUrl = this.ServiceUrl,
                ConstructorToken = this.ConstructorToken,
            };

            Assert.AreEqual(this.ApiKey, config.ApiKey);
            Assert.AreEqual(this.ApiToken, config.ApiToken);
            Assert.AreEqual(this.ServiceUrl, config.ServiceUrl);
            Assert.AreEqual(this.ConstructorToken, config.ConstructorToken);
        }

        [Test]
        public void ConstructorioConfigWithInvalidApiKeyAndToken()
        {
            Assert.Throws<ArgumentException>(() => new ConstructorioConfig(null, "apiToken"));
        }

        [Test]
        public void ConstructorioConfigWithInvalidApiKey()
        {
            Assert.Throws<ArgumentException>(() => new ConstructorioConfig(null));
        }
    }
}
