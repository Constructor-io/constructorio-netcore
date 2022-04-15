using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace Constructorio_NET
{
  public class Search
  {
    private Hashtable Options;
    public Search(Hashtable options)
    {
      this.Options = options;
      // this.ApiKey = (string)options["apiKey"];
      // this.ServiceUrl = (string)options["serviceUrl"];
    }
    private string CreateSearchUrl(string query, Hashtable parameters, Hashtable userParameters)
    {
      string[] allowedParams = {
        "page",
        "resultsPerPage",
        "filters",
        "sortBy",
        "sortOrder",
        "section",
        "fmtOptions",
        "hiddenFields"
      };
      Hashtable cleanedParams = Helpers.CleanParams(parameters);
      Dictionary<string, string> queryParams = new Dictionary<string, string>()
      {
        // { "key", (string)this.Options["apiKey"] }
      };

      Console.WriteLine(queryParams);

      NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

      // foreach (DictionaryEntry param in queryParams)
      // {
      //   queryString.Add((String)param.Key, (String)param.Value);
      // }
      List<string> paths = new List<string> { "search", HttpUtility.UrlEncode(query) };
      // string url = $"{this.ServiceUrl}/search/{query}?{queryString}";
      // string url = $"{serviceUrl}/search/{query}?{HttpUtility.UrlPathEncode(queryString)}";

      return Helpers.makeUrl(this.Options, paths, queryParams);
    }

    /// <summary>
    /// Retrieve search results from API
    /// </summary>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="userParameters"></param>
    /// <returns></returns>
    public string GetSearchResults(string query, Hashtable parameters, Hashtable userParameters)
    {

      string url = CreateSearchUrl(query, parameters, userParameters);
      // return new SearchResponse('dfddf', SearchResponseInner, ['sdads']);
      return url;
    }
  }
}