using System;
using System.Collections;

namespace Constructorio_NET
{
  public class Search
  {
    private Hashtable options; 
    public Search(Hashtable options)
    {
      this.options = options;
    }
    private string CreateSearchUrl(string query, Hashtable parameters, Hashtable userParameters)
    {
      Hashtable cleanedParams = Helpers.CleanParams(parameters);
      string url = "url";

      return url;
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
      return query;
    }
  }
}