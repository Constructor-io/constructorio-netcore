using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
    internal string CreateSearchUrl(string query, Hashtable parameters, Hashtable userParameters)
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
      Dictionary<string, string> queryParams = new Dictionary<string, string>();
      List<string> paths = new List<string> { "search", HttpUtility.UrlEncode(query) };

      foreach (DictionaryEntry param in parameters)
      {
        if (allowedParams.Contains(param.Key) )
        {
          queryParams.Add((string)param.Key, (string)param.Value);
        }
      }

      return Helpers.MakeUrl(this.Options, paths, queryParams);
    }

    /// <summary>
    /// Retrieve search results from API
    /// </summary>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="userParameters"></param>
    /// <returns></returns>
    public SearchResponse GetSearchResults(string query, Hashtable parameters, Hashtable userParameters)
    {
      string url = CreateSearchUrl(query, parameters, userParameters);
      Task<string> task = Helpers.MakeGetRequest(url);

      return JsonConvert.DeserializeObject<SearchResponse>(task.Result);
    }
  }
}