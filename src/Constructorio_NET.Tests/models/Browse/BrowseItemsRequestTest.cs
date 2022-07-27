using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
  [TestFixture]
  public class BrowseItemsRequestTest
  {
    private readonly string ClientId = "r4nd-cl1ent-1d";
    private readonly int SessionId = 4;
    private readonly string UserId = "user1";
    private readonly int Page = 2;
    private readonly string Section = "Search Suggestions";
    private readonly string SortBy = "Price";
    private readonly string SortOrder = "Ascending";
    private readonly List<string> UserSegments = new List<string>() { "us", "desktop" };
    private readonly List<string> ItemIds = new List<string>() { "10001", "10002" };
    private UserInfo UserInfo;

    [OneTimeSetUp]
    public void Setup()
    {
      this.UserInfo = new UserInfo(ClientId, SessionId);
      this.UserInfo.SetUserId(this.UserId);
      this.UserInfo.SetUserSegments(this.UserSegments);
    }

    [Test]
    public void GetRequestParameters()
    {
      BrowseItemsRequest req = new BrowseItemsRequest(this.ItemIds)
      {
        UserInfo = this.UserInfo,
        Page = this.Page,
        Section = this.Section,
        SortBy = this.SortBy,
        SortOrder = SortOrder,
      };

      Hashtable requestParameters = req.GetRequestParameters();
      Assert.AreEqual(this.ClientId, requestParameters[Constants.CLIENT_ID]);
      Assert.AreEqual(this.SessionId, requestParameters[Constants.SESSION_ID]);
      Assert.AreEqual(this.UserId, requestParameters[Constants.USER_ID]);
      Assert.AreEqual(this.UserSegments, requestParameters[Constants.USER_SEGMENTS]);
      Assert.AreEqual(this.Page, requestParameters[Constants.PAGE]);
      Assert.AreEqual(this.Section, requestParameters[Constants.SECTION]);
      Assert.AreEqual(this.SortBy, requestParameters[Constants.SORT_BY]);
      Assert.AreEqual(this.SortOrder, requestParameters[Constants.SORT_ORDER]);
    }

    [Test]
    public void GetBrowseItemsResultsWithInvalidItemId()
    {
      Assert.Throws<ArgumentException>(() => new BrowseItemsRequest(null));
    }
  }
}
