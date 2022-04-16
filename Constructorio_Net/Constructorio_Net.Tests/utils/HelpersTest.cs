namespace Constructorio_NET.Tests
{
  using System.Collections;
  using NUnit.Framework;
  using System.Collections.Generic;

  [TestFixture]
  public class HelpersTest : Helpers
  {
      private string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
      private string Query = "item";
      private string ServiceUrl = "https://ac.cnstrc.com";
      private string Version = "cionet-5.6.0";

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
        { "filters", filters },
        { "userId", "boink doink yoink" }, // contains non-breaking spaces
        { "section", "Products" },
      };
      Hashtable expectedParameters = new Hashtable()
      {
        { "filters", filters },
        { "userId", "boink doink yoink" }, // contains non-breaking spaces
        { "section", "Products" },
      };

      Hashtable cleanedParams = Helpers.CleanParams(parameters);
      Assert.AreEqual(expectedParameters["userId"], cleanedParams["userId"], "parameters should be cleaned");
    }

    [Test]
    public void MakeUrlSearch()
    {
      Hashtable options = new Hashtable()
      {
        { "serviceUrl", this.ServiceUrl },
        { "apiKey", this.ApiKey },
        { "version", this.Version }
      };
      List<string> paths = new List<string>
      {
        "search", this.Query
      };
      Dictionary<string, string> queryParams = new Dictionary<string, string>()
      {
        { "section", "Search Suggestions" },
      };

      string url = Helpers.MakeUrl(options, paths, queryParams);
      string expectedUrl = $"https://ac.cnstrc.com/search/{this.Query}?key={this.ApiKey}&c={this.Version}&section=Search%20Suggestions";
      Assert.AreEqual(expectedUrl, url, "url should be properly formed");
    }
  }
}