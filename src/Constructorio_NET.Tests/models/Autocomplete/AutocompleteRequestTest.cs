using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
  [TestFixture]
  public class AutocompleteRequestTest
  {
    private readonly string ApiKey = "ZqXaOfXuBWD4s3XzCI1q";
    private readonly string ClientId = "r4nd-cl1ent-1d";
    private readonly int SessionId = 4;
    private ConstructorioConfig Config;
    private UserInfo UserInfo;

    [SetUp]
    public void Setup()
    {
      this.Config = new ConstructorioConfig(this.ApiKey);
      this.UserInfo = new UserInfo(ClientId, SessionId);
    }

    [Test]
    public void GetAutocompleteResultsWithInvalidApiKeyShouldError()
    {
      AutocompleteRequest req = new AutocompleteRequest("item")
      {
        UserInfo = UserInfo
      };
      ConstructorIO constructorio = new ConstructorIO(new ConstructorioConfig("invalidKey"));
      var ex = Assert.ThrowsAsync<ConstructorException>(() => constructorio.Autocomplete.GetAutocompleteResults(req));
      Assert.IsTrue(ex.Message == "Http[400]: We have no record of this key. You can find your key at app.constructor.io/dashboard.", "Correct Error is Returned");
    }

    [Test]
    public async Task GetAutocompleteResults()
    {
      AutocompleteRequest req = new AutocompleteRequest("item")
      {
        UserInfo = UserInfo
      };
      ConstructorIO constructorio = new ConstructorIO(this.Config);
      AutocompleteResponse res = await constructorio.Autocomplete.GetAutocompleteResults(req);

      Assert.NotNull(res.ResultId, "Result id exists");
    }
  }
}
