namespace Constructorio_NET
{
  using System;
  using System.Collections;
  using System.Net.Http;
  using System.Text.RegularExpressions;
  using System.Threading.Tasks;

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
    /// Make a get request
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private async Task<string> makeGetRequest(Uri url)
    {
      var response = await client.GetAsync(url);
      var content = response.Content;
      var result = await content.ReadAsStringAsync();
      return result;
    }
  }
}