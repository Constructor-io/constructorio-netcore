using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Constructorio_NET
{
  public class Search : Helpers
  {
    private Hashtable Options;
    public Search(Hashtable options)
    {
      this.Options = options;
    }
    internal string CreateSearchUrl(SearchRequest req)
    {
      // Hashtable cleanedParams = Helpers.CleanParams(parameters);
      Hashtable queryParams = req.getParameters();
      List<string> paths = new List<string> { "search", req.Query };

      return Helpers.MakeUrl(this.Options, paths, queryParams);
    }

    /// <summary>
    /// Retrieve search results from API
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public SearchResponse GetSearchResults(SearchRequest req)
    {
      string url = CreateSearchUrl(req);
      Task<string> task = Helpers.MakeGetRequest(url);
      // needs http error handling

      return JsonConvert.DeserializeObject<SearchResponse>(task.Result);
    }
  }
}