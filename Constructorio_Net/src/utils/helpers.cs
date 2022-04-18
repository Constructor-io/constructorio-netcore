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
      // string constants
      string APIKEY= "apiKey";
      string CLIENTID= "clientId";
      string FILTERS = "filters";
      string FMTOPTIONS = "fmtOptions";
      string HIDDENFIELDS = "hiddenFields";
      string SEGMENTS = "segments";
      string SESSIONID = "sessionId";
      string SECTION = "section";
      string TESTCELLS = "testCells";
      string USERID = "userId";
      string VERSION= "version";
      string PAGE = "page";
      string RESULTSPERPAGE = "resultsPerPage";
      string SORTBY = "sort_by";
      string SORTORDER = "sort_order";

      Dictionary<string, string> urlParamsMap = new Dictionary<string, string>()
      {
        { CLIENTID, "i" },
        { SESSIONID, "s" },
        { USERID, "ui" },
        { SEGMENTS, "us" },
        { FMTOPTIONS, "fmt_options" },
        { SECTION, "section" },
        { FILTERS, "filters" },
        { HIDDENFIELDS, "hidden_fields" },
        { PAGE, "page" },
        { RESULTSPERPAGE, "num_results_per_page" },
        { SORTBY, "sort_by" },
        { SORTORDER, "sort_order" },
      };

      foreach (var path in paths)
      {
        url += "/" + HttpUtility.UrlEncode(path);
      }

      url += $"?key={options[APIKEY]}&c={options[VERSION]}";


      // Generate url params query string
      if (queryParams != null && queryParams.Count != 0)
      {
        if (queryParams.Contains(CLIENTID))
        {
          url += $"&{urlParamsMap[CLIENTID]}={Uri.EscapeDataString((string)queryParams[CLIENTID])}";
          queryParams.Remove(CLIENTID);
        }
        if (queryParams.Contains(SESSIONID))
        {
          url += $"&{urlParamsMap[SESSIONID]}={Uri.EscapeDataString(queryParams[SESSIONID].ToString())}";
          queryParams.Remove(SESSIONID);
        }
        // Add filters to query string
        if (queryParams.Contains(FILTERS))
        {
          Dictionary<string, List<string>> filters = (Dictionary<string, List<string>>)queryParams[FILTERS]; 
          queryParams.Remove(FILTERS);

          foreach (var filter in filters)
          {
            string filterGroup = (string)filter.Key;

            foreach (string filterOption in (List<string>)filter.Value)
            {
              url += $"&{urlParamsMap[FILTERS]}{Uri.EscapeDataString("[" + filterGroup + "]")}={Uri.EscapeDataString(filterOption)}";
            }
          }
        }
        // Add test cells to query string
        if (queryParams.Contains(TESTCELLS))
        {
          Dictionary<string, string> testCells = (Dictionary<string, string>)queryParams[TESTCELLS]; 
          queryParams.Remove(TESTCELLS);

          foreach (var testCell in testCells)
          {
            url += $"&ef-{Uri.EscapeDataString(testCell.Key)}={Uri.EscapeDataString(testCell.Value)}";
          }
        }
        // Add format options to query string
        if (queryParams.Contains(FMTOPTIONS))
        {
          Dictionary<string, string> fmtOptions = (Dictionary<string, string>)queryParams[FMTOPTIONS];
          queryParams.Remove(FMTOPTIONS);

          foreach (var fmtOption in fmtOptions)
          {
            url += $"&{urlParamsMap[FMTOPTIONS]}{Uri.EscapeDataString("[" + fmtOption.Key + "]")}={Uri.EscapeDataString(fmtOption.Value)}";
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