using System;
using System.Collections;

namespace Constructorio_NET
{
  public class Recommendations
  {
    private Hashtable options;
    public Recommendations(Hashtable options)
    {
      this.options = options;
    }
    private string CreateRecommendationsUrl(string query, Hashtable parameters, Hashtable userParameters)
    {
      string url = "url";

      return url;
    }

    /// <summary>
    /// Retrieve recommendation results from API
    /// </summary>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="userParameters"></param>
    /// <returns></returns>
    public string GetRecommendationsResults(string podId, Hashtable parameters, Hashtable userParameters)
    {
      string url = CreateRecommendationsUrl(podId, parameters, userParameters);
      return podId;
    }
  }
}