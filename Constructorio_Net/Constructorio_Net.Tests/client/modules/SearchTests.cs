using NUnit.Framework;
using System;
using System.Collections;

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
        public void GetSearchResults()
        {
            Hashtable parameters = new Hashtable()
            {
               { "apiKey", "ZqXaOfXuBWD4s3XzCI1q" }
            };
            ConstructorIO constructorio = new ConstructorIO(parameters);
            Console.WriteLine(constructorio.Search.GetSearchResults("item", new Hashtable(), new Hashtable()));
        }
    }
}