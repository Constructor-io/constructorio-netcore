using NUnit.Framework;

namespace Constructorio_Net.Tests
{
    [TestFixture]
    public class ConstructorIO_BasicTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NewShouldSetApiToken()
        {
            ConstructorIO constructorIO = new ConstructorIO("boinkaToken", "doinkaKey", true, null, null);
            Assert.AreEqual(constructorIO.apiToken, "boinkaToken", "api token should be set");
        }
    }
}