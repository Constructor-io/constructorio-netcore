namespace Constructorio_NET.Tests
{
  using System;
  using System.Collections;
  using NUnit.Framework;

  [TestFixture]
  public class HelpersTest
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CleanParams()
    {
      Hashtable filters = new Hashtable()
      {
        { "size", "large" },
        { "color", "green" },
      };

      Hashtable parameters = new Hashtable()
      {
        { "origin_referrer", "https://test.com/search/pizza?a=bread&b=pizza+burrito" },
        { "filters", filters },
        { "userId", "boink doink yoink" }, // contains non-breaking spaces
        { "section", "Products" },
      };

      Hashtable expectedParameters = new Hashtable()
      {
        { "origin_referrer", "https://test.com/search/pizza?a=bread&b=pizza+burrito" },
        { "filters", filters },
        { "userId", "boink doink yoink" }, // contains non-breaking spaces
        { "section", "Products" },
      };

      Hashtable cleanedParams = Helpers.CleanParams(parameters);
      Assert.AreEqual(expectedParameters["userId"], cleanedParams["userId"], "parameters should be cleaned");
    }
  }
}