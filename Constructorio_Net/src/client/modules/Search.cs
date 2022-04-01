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
    // private string CreateSearchUrl(string term, parameters, userParameters, options)
    // {
    //   string url;
    //   return url;
    // }

    // public SearchResponse getSearchResults(string query)
    public string GetSearchResults(string query)
    {
      // return new SearchResponse('dfddf', SearchResponseInner, ['sdads']);
      return query;
    }
  }
}