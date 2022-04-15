namespace Constructorio_NET
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.Net.Http;
  using System.Text.RegularExpressions;
  using System.Threading.Tasks;
  using System.Web;

  public class Helpers
  {
    private static HttpClient client = new HttpClient();

    /// <summary>
    /// Cleans params before applying them to a request url
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static Hashtable CleanParams(Hashtable parameters)
    {
      Hashtable cleanedParams = new Hashtable();

      foreach (DictionaryEntry param in parameters)
      {
        if (param.Value.GetType() == typeof(string))
        {
          string encodedParam = Uri.UnescapeDataString(Uri.EscapeDataString(param.Value.ToString()));
          encodedParam = Regex.Replace(encodedParam, @"\s", " ");
          cleanedParams.Add(param.Key, encodedParam);
        }
        else
        {
          cleanedParams.Add(param.Key, param.Value);
        }
      }

      return cleanedParams;
    }

    /// <summary>
    /// Makes a URL to issue the requests with.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="paths"></param>
    /// <param name="queryParams"></param>
    /// <returns>string</returns>
    internal static string MakeUrl(Hashtable options, List<String> paths, Dictionary<String, String> queryParams)
    {
      NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
      string url = (string)options["serviceUrl"];

      foreach (var path in paths)
      {
        url += "/" + path;
      }

      url += "?key=" + options["apiKey"] + "&c=" + options["version"];

      if (queryParams != null && queryParams.Count != 0)
      {
        foreach (var queryParam in queryParams)
        {
          queryString.Add((string)queryParam.Key, (string)queryParam.Value);
        }

        url += "&" + queryString.ToString();
      }

      return url;
    }

    /// <summary>
    /// Make a get request
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    // internal static async Task<string> MakeGetRequest(string url)
    // {
    //   var response = await client.GetAsync(new Uri(url));
    //   var content = response.Content;
    //   var result = await content.ReadAsStringAsync();
    //   return result;
    // }
    internal static async Task<string> MakeGetRequest(string url)
    {
      var response = await client.GetAsync(new Uri(url));
      var content = response.Content;
      var result = await content.ReadAsStringAsync();
      return result;
    }
  }
}