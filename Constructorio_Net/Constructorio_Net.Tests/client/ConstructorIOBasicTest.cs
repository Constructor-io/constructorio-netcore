using NUnit.Framework;

namespace Constructorio_NET.Tests
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
            //ConstructorIO constructorIO = new ConstructorIO("boinkaToken", "doinkaKey", true, null, null);
            //Assert.AreEqual(constructorIO.apiToken, "boinkaToken", "api token should be set");
        }

        // [Test]
        // public void Search()
        // {
        //     string thing = "thing";
        //     ConstructorIO constructorIO = new ConstructorIO("boinkaToken", "doinkaKey", true, null, null);
        //     string result = constructorIO.search.getSearchResults(thing);
        //     Assert.AreEqual(result, thing, "api token should be set");
        // }
    }
}