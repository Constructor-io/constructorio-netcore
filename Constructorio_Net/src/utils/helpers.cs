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
      string url = (string)options["serviceUrl"];
      Dictionary<string, string> urlParamsMap = new Dictionary<string, string>()
      {
        { Constants.CLIENT_ID, "i" },
        { Constants.SESSION_ID, "s" },
        { Constants.USER_ID, "ui" },
        { Constants.SEGMENTS, "us" },
        { Constants.FMT_OPTIONS, "fmt_options" },
        { Constants.SECTION, "section" },
        { Constants.FILTERS, "filters" },
        { Constants.HIDDEN_FIELDS, "hidden_fields" },
        { Constants.PAGE, "page" },
        { Constants.RESULTS_PER_PAGE, "num_results_per_page" },
        { Constants.SORT_BY, "sort_by" },
        { Constants.SORT_ORDER, "sort_order" },
      };

      foreach (var path in paths)
      {
        url += "/" + HttpUtility.UrlEncode(path);
      }

      url += $"?key={options[Constants.API_KEY]}&c={options[Constants.VERSION]}";

      // Generate url params query string
      if (queryParams != null && queryParams.Count != 0)
      {
        if (queryParams.Contains(Constants.CLIENT_ID))
        {
          url += $"&{urlParamsMap[Constants.CLIENT_ID]}={Uri.EscapeDataString((string)queryParams[Constants.CLIENT_ID])}";
          queryParams.Remove(Constants.CLIENT_ID);
        }
        if (queryParams.Contains(Constants.SESSION_ID))
        {
          url += $"&{urlParamsMap[Constants.SESSION_ID]}={Uri.EscapeDataString(queryParams[Constants.SESSION_ID].ToString())}";
          queryParams.Remove(Constants.SESSION_ID);
        }
        // Add filters to query string
        if (queryParams.Contains(Constants.FILTERS))
        {
          Dictionary<string, List<string>> filters = (Dictionary<string, List<string>>)queryParams[Constants.FILTERS]; 
          queryParams.Remove(Constants.FILTERS);

          foreach (var filter in filters)
          {
            string filterGroup = (string)filter.Key;

            foreach (string filterOption in (List<string>)filter.Value)
            {
              url += $"&{urlParamsMap[Constants.FILTERS]}{Uri.EscapeDataString("[" + filterGroup + "]")}={Uri.EscapeDataString(filterOption)}";
            }
          }
        }
        // Add test cells to query string
        if (queryParams.Contains(Constants.TEST_CELLS))
        {
          Dictionary<string, string> testCells = (Dictionary<string, string>)queryParams[Constants.TEST_CELLS]; 
          queryParams.Remove(Constants.TEST_CELLS);

          foreach (var testCell in testCells)
          {
            url += $"&ef-{Uri.EscapeDataString(testCell.Key)}={Uri.EscapeDataString(testCell.Value)}";
          }
        }
        // Add format options to query string
        if (queryParams.Contains(Constants.FMT_OPTIONS))
        {
          Dictionary<string, string> fmtOptions = (Dictionary<string, string>)queryParams[Constants.FMT_OPTIONS];
          queryParams.Remove(Constants.FMT_OPTIONS);

          foreach (var fmtOption in fmtOptions)
          {
            url += $"&{urlParamsMap[Constants.FMT_OPTIONS]}{Uri.EscapeDataString("[" + fmtOption.Key + "]")}={Uri.EscapeDataString(fmtOption.Value)}";
          }
        }

        // Add remaining query params to query string
        foreach (DictionaryEntry queryParam in queryParams)
        {
          string paramKey = (string)queryParam.Key;
          Type valueDataType = queryParam.Value.GetType();

          if (valueDataType == typeof(string) && urlParamsMap.ContainsKey(paramKey))
          {
            url += $"&{Uri.EscapeDataString(urlParamsMap[paramKey])}={Uri.EscapeDataString((string)queryParam.Value)}";
          }
          else if (valueDataType == typeof(int))
          {
            url += $"&{Uri.EscapeDataString(urlParamsMap[paramKey])}={Uri.EscapeDataString(queryParam.Value.ToString())}";
          }
          else if (valueDataType == typeof(List<string>))
          {
            foreach (string listValue in (List<string>)queryParam.Value)
            {
              url += $"&{urlParamsMap[paramKey]}={Uri.EscapeDataString(listValue)}";
            }
          }
        }

        long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        url += $"&_dt={time}";
      }

      Console.WriteLine(url);
      return url;
    }

    /// <summary>
    /// Make a http get request
    /// </summary>
    /// <param name="url"></param>
    /// <param name="requestHeaders"></param>
    /// <returns>Task</returns>
    internal static async Task<string> MakeGetRequest(string url, Dictionary<string, string> requestHeaders)
    {
      HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

      foreach (var header in requestHeaders)
      {
        httpRequest.Headers.Add((string)header.Key, (string)header.Value);
      }

      var response = await client.SendAsync(httpRequest);
      HttpContent content = response.Content;
      string result = await content.ReadAsStringAsync();

      return result;
    }
  }
}