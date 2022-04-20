using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Constructorio_NET
{
  public class Search : Helpers
  {
    private Hashtable Options;

    /// <summary>
    /// Interface to search related API calls
    /// </summary>
    internal Search(Hashtable options)
    {
      this.Options = options;
    }
    internal string CreateSearchUrl(SearchRequest req)
    {
      // Hashtable cleanedParams = Helpers.CleanParams(parameters);
      Hashtable queryParams = req.GetUrlParameters();
      List<string> paths = new List<string> { "search", req.Query };

      return Helpers.MakeUrl(this.Options, paths, queryParams);
    }

    /// <summary>
    /// Retrieve search results from API
    /// </summary>
    /// <param name="searchRequest">Constructorio's request object</param>
    /// <returns>Constructorio's response object</returns>
    public SearchResponse GetSearchResults(SearchRequest searchRequest)
    {
      string url;
      Task<string> task;
      Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

      try
      {
        url = CreateSearchUrl(searchRequest);
        requestHeaders = searchRequest.GetRequestHeaders();
        task = Helpers.MakeGetRequest(url, requestHeaders);
      }
      catch (Exception e)
      {
        throw new ConstructorException(e);
      }

      if (task.Result != null)
      {
        return JsonConvert.DeserializeObject<SearchResponse>(task.Result);
      }

      throw new ConstructorException("GetSearchResults response data is malformed");
    }
  }
}
