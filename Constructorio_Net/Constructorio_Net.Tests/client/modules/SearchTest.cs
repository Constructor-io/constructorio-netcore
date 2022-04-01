using NUnit.Framework;
using System;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class SearchTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Search()
        {
            string thing = "thing";
            Console.WriteLine("dfsdf");
            ConstructorIO constructorIO = new ConstructorIO("boinkaToken", "doinkaKey", true, null, null);
            string result = constructorIO.search.getSearchResults();
            Assert.AreEqual(result, thing, "api token should be set");
        }
    }
}