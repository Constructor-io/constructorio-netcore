using System;
using System.Collections;

namespace Constructorio_NET
{
  public class Autocomplete
  {
    private Hashtable options;
    public Autocomplete(Hashtable options)
    {
      this.options = options;
    }
    private string CreateAutocompleteUrl(string query, Hashtable parameters, Hashtable userParameters)
    {
      string url = "url";

      return url;
    }

    /// <summary>
    /// Retrieve autocomplete results from API
    /// </summary>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="userParameters"></param>
    /// <returns></returns>
    public string GetAutocompleteResults(string podId, Hashtable parameters, Hashtable userParameters)
    {
      string url = CreateAutocompleteUrl(podId, parameters, userParameters);
      return podId;
    }
  }
}