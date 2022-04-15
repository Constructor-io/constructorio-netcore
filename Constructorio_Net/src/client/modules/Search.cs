using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Constructorio_NET
{
  public class Search
  {
    private Hashtable Options;
    public Search(Hashtable options)
    {
      this.Options = options;
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

      NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

      // foreach (DictionaryEntry param in queryParams)
      // {
      //   queryString.Add((String)param.Key, (String)param.Value);
      // }
      List<string> paths = new List<string> { "search", HttpUtility.UrlEncode(query) };

      return Helpers.MakeUrl(this.Options, paths, queryParams);
    }

    /// <summary>
    /// Retrieve search results from API
    /// </summary>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="userParameters"></param>
    /// <returns></returns>
    public string GetSearchResults(string query, Hashtable parameters, Hashtable userParameters)
    // / public Task<string> GetSearchResults(string query, Hashtable parameters, Hashtable userParameters)
    {

      string url = CreateSearchUrl(query, parameters, userParameters);
      Task<string> task = Helpers.MakeGetRequest(url);
      SearchResponse response = JsonConvert.DeserializeObject<SearchResponse>(task.Result);
      // Console.WriteLine(response.Response.TotalNumResults);
      // response.Response.Results.ForEach(i => Console.Write("{0}\t", i.Value));
      return url;
    }
  }
}