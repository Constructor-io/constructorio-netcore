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
    protected static Hashtable CleanParams(Hashtable parameters)
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
    protected static string MakeUrl(Hashtable options, List<String> paths, Hashtable queryParams)
    {
      string queryParamsString = "";
      string url = (string)options["serviceUrl"];

      foreach (var path in paths)
      {
        url += "/" + path;
      }

      url += "?key=" + options["apiKey"] + "&c=" + options["version"];

      // Generate url params query string
      if (queryParams != null && queryParams.Count != 0)
      {
        // Add filters to query string
        if (queryParams.Contains("filters"))
        {
          Dictionary<string, List<string>> filters = (Dictionary<string, List<string>>)queryParams["filters"]; 
          queryParams.Remove("filters");

          foreach (var filter in filters)
          {
            string filterGroup = (string)filter.Key;

            foreach (string filterOption in (List<string>)filter.Value)
            {
              queryParamsString += $"&filters{Uri.EscapeDataString("[" + filterGroup + "]")}={Uri.EscapeDataString(filterOption)}";
            }
          }
        }
        // Add test cells to query string
        if (queryParams.Contains("testCells"))
        {
          Hashtable testCells = (Hashtable)queryParams["testCells"]; 
          queryParams.Remove("testCells");

          foreach (DictionaryEntry testCell in testCells)
          {
            string testCellName = (string)testCell.Key;

            foreach (string testCellValue in (List<string>)testCell.Value)
            {
              queryParamsString += $"&ef-{Uri.EscapeDataString(testCellName)}={Uri.EscapeDataString(testCellValue)}";
            }
          }
        }
        // Add segments to query string
        if (queryParams.Contains("segments"))
        {
          List<string> segments = (List<string>)queryParams["segments"]; 
          queryParams.Remove("segments");

          foreach (string segment in segments)
          {
            queryParamsString += $"&us={Uri.EscapeDataString(segment)}";
          }
        }
        // Add hidden fields to query string
        if (queryParams.Contains("hiddenFields"))
        {
          List<string> hiddenFields = (List<string>)queryParams["hiddenFields"]; 
          queryParams.Remove("hiddenFields");

          foreach (string hiddenField in hiddenFields)
          {
            queryParamsString += $"&hidden_field={Uri.EscapeDataString(hiddenField)}";
          }
        }
        // Add format options to query string
        if (queryParams.Contains("fmtOptions"))
        {
          Hashtable fmtOptions = (Hashtable)queryParams["fmtOptions"];
          queryParams.Remove("fmtOptions");

          foreach (DictionaryEntry fmtOption in fmtOptions)
          {
            string fmtOptionName = (string)fmtOption.Key;

            foreach (string fmtOptionValue in (List<string>)fmtOption.Value)
            {
              queryParamsString += $"&fmt_options{Uri.EscapeDataString("[" + fmtOptionName + "]")}={Uri.EscapeDataString(fmtOptionValue)}";
            }
          }
        }

        // Add remaining query params to query string
        foreach (DictionaryEntry queryParam in queryParams)
        {
          if (queryParam.Value.GetType() == typeof(string))
          {
            queryParamsString += $"&{Uri.EscapeDataString((string)queryParam.Key)}={Uri.EscapeDataString((string)queryParam.Value)}";
          }
        }

        long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        queryParamsString += $"&_dt={time}";
        url += queryParamsString;
      }

      return url;
    }

    /// <summary>
    /// Make a get request
    /// </summary>
    /// <param name="url"></param>
    /// <returns>Task</returns>
    internal static async Task<string> MakeGetRequest(string url)
    {
      var response = await client.GetAsync(new Uri(url));
      var content = response.Content;
      var result = await content.ReadAsStringAsync();
      return result;
    }
  }
}