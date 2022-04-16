namespace Constructorio_NET.Tests
{
  using NUnit.Framework;
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Text.RegularExpressions;

  [TestFixture]
  public class HelpersTest : Helpers
  {
      private string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
      private Hashtable Options;
      private string Query = "item";
      private string ServiceUrl = "https://ac.cnstrc.com";
      private string Version = "cionet-5.6.0";

    [SetUp]
    public void Setup()
    {
      this.Options = new Hashtable()
      {
        { "serviceUrl", this.ServiceUrl },
        { "apiKey", this.ApiKey },
        { "version", this.Version }
      };
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
      List<string> paths = new List<string> { "search", this.Query };
      Hashtable queryParams = new Hashtable()
      {
        { "section", "Search Suggestions" },
      };

      string url = Helpers.MakeUrl(this.Options, paths, queryParams);
      string expectedUrl = $@"https:\/\/ac.cnstrc.com\/search\/{this.Query}\?key={this.ApiKey}&c={this.Version}&section=Search%20Suggestions&_dt=";
      bool regexMatched = Regex.Match(url, expectedUrl).Success;
      Assert.That(regexMatched, "url should be properly formed");
    }

    [Test]
    public void MakeUrlSearchWithFilters()
    {
      List<string> paths = new List<string> { "search", this.Query };
      Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>()
      {
        { "Color", new List<string>() { "green" } }
      };
      Hashtable queryParams = new Hashtable()
      {
        { "section", "Search Suggestions" },
        { "filters", filters }
      };

      string url = Helpers.MakeUrl(this.Options, paths, queryParams);
      string expectedUrl = $@"https:\/\/ac.cnstrc.com\/search\/{this.Query}\?key={this.ApiKey}&c={this.Version}&filters%5BColor%5D=green&section=Search%20Suggestions&_dt=";
      bool regexMatched = Regex.Match(url, expectedUrl).Success;
      Assert.That(regexMatched, "url should be properly formed");
    }
  }
}