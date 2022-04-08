using System;
using System.Collections;

namespace Constructorio_NET
{
  public class Browse
  {
    private Hashtable options;
    public Browse(Hashtable options)
    {
      this.options = options;
    }
    private string CreateBrowseUrl(string query, Hashtable parameters, Hashtable userParameters)
    {
      string url = "url";

      return url;
    }

    /// <summary>
    /// Retrieve browse results from API
    /// </summary>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="userParameters"></param>
    /// <returns></returns>
    public string GetBrowseResults(string query, Hashtable parameters, Hashtable userParameters)
    {
      string url = CreateBrowseUrl(query, parameters, userParameters);
      return query;
    }
  }
}