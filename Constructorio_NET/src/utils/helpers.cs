namespace Constructorio_NET
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Net.Http;
  using System.Text;
  using System.Text.RegularExpressions;
  using System.Threading.Tasks;
  using System.Web;
  using Newtonsoft.Json;
  using System.IO;

  public class Helpers
  {
    private static HttpClient client = new HttpClient();

    /// <summary>
    /// Method in order to modify a string to ensure proper url encoding
    /// </summary>
    /// <param name="str"></param>
    /// <returns>Url encoded string</returns>
    protected static string OurEscapeDataString(string str)
    {
      string encodedString = Regex.Replace(str, @"\s", " ");
      encodedString = Uri.EscapeDataString(encodedString);

      return encodedString;
    }

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
            string encodedParam = OurEscapeDataString(param.Value.ToString());
            string cleanedParam = Uri.UnescapeDataString(encodedParam);

            cleanedParams.Add(param.Key, cleanedParam);
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
    protected static string MakeUrl(Hashtable options, List<String> paths, Hashtable queryParams, Dictionary<string, bool> omittedQueryParams = null)
    {
        string url = (string)options[Constants.SERVICE_URL];

        foreach (var path in paths)
        {
            url += "/" + HttpUtility.UrlEncode(path);
        }

        url += $"?key={options[Constants.API_KEY]}";

        if (omittedQueryParams == null || omittedQueryParams.ContainsKey("c"))
        {
          url += $"&c={options[Constants.VERSION]}";
        }

        // Generate url params query string
        if (queryParams != null && queryParams.Count != 0)
        {
            if (queryParams.Contains(Constants.CLIENT_ID))
            {
                url += $"&{Constants.CLIENT_ID}={OurEscapeDataString((string)queryParams[Constants.CLIENT_ID])}";
                queryParams.Remove(Constants.CLIENT_ID);
            }
            if (queryParams.Contains(Constants.SESSION_ID))
            {
                url += $"&{Constants.SESSION_ID}={OurEscapeDataString(queryParams[Constants.SESSION_ID].ToString())}";
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
                        url += $"&{Constants.FILTERS}{OurEscapeDataString("[" + filterGroup + "]")}={OurEscapeDataString(filterOption)}";
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
                    url += $"&ef-{OurEscapeDataString(testCell.Key)}={OurEscapeDataString(testCell.Value)}";
                }
            }
            // Add format options to query string
            if (queryParams.Contains(Constants.FMT_OPTIONS))
            {
                Dictionary<string, string> fmtOptions = (Dictionary<string, string>)queryParams[Constants.FMT_OPTIONS];
                queryParams.Remove(Constants.FMT_OPTIONS);

                foreach (var fmtOption in fmtOptions)
                {
                    url += $"&{Constants.FMT_OPTIONS}{OurEscapeDataString("[" + fmtOption.Key + "]")}={OurEscapeDataString(fmtOption.Value)}";
                }
            }

            // Add hidden fields as fmt_options
            if (queryParams.Contains(Constants.HIDDEN_FIELDS))
            {
                List<string> hiddenFields = (List<string>)queryParams[Constants.HIDDEN_FIELDS];
                queryParams.Remove(Constants.HIDDEN_FIELDS);

                foreach (var hiddenField in hiddenFields)
                {
                    url += $"&{Constants.FMT_OPTIONS}{OurEscapeDataString("[" + Constants.HIDDEN_FIELDS + "]")}={OurEscapeDataString(hiddenField)}";
                }
            }

            // Add remaining query params to query string
            foreach (DictionaryEntry queryParam in queryParams)
            {
              string paramKey = (string)queryParam.Key;
              Type valueDataType = queryParam.Value.GetType();

              if (valueDataType == typeof(string))
              {
                  url += $"&{OurEscapeDataString(paramKey)}={OurEscapeDataString((string)queryParam.Value)}";
              }
              else if (valueDataType == typeof(int))
              {
                  url += $"&{OurEscapeDataString(paramKey)}={OurEscapeDataString(queryParam.Value.ToString())}";
              }
              else if (valueDataType == typeof(List<string>))
              {
                foreach (string listValue in (List<string>)queryParam.Value)
                {
                    url += $"&{paramKey}={OurEscapeDataString(listValue)}";
                }
              }
            }

          long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();

          if (omittedQueryParams == null || !omittedQueryParams.ContainsKey("_dt"))
          {
            url += $"&_dt={time}";
          }
        }

        return url;
      }

    /// <summary>
    /// Make a http request
    /// </summary>
    /// <param name="httpMethod">HTTP request method</param>
    /// <param name="url">Url for the request</param>
    /// <param name="requestHeaders">Additional headers to send with the request</param>
    /// <param name="requestBody">Key values pairs used for the POST body</param>
    /// <param name="stream">Key values pairs used for the POST body</param>
    /// <returns>Task</returns>
    public static async Task<string> MakeHttpRequest(HttpMethod httpMethod, string url, Dictionary<string, string> requestHeaders, Hashtable requestBody = null, Dictionary<string, StreamContent> files = null)
    {
      HttpRequestMessage httpRequest = new HttpRequestMessage(httpMethod, url);

      foreach (var header in requestHeaders)
      {
        httpRequest.Headers.Add((string)header.Key, (string)header.Value);
      }

      if (files != null)
      {
        var formData = new MultipartFormDataContent();

        if (files.ContainsKey("items")) {
          formData.Add(files["items"], "items", "items.csv");
        }
        if (files.ContainsKey("variations")) {
          formData.Add(files["variations"], "variations", "variations.csv");
        }
        if (files.ContainsKey("item_groups")) {
          formData.Add(files["item_groups"], "item_groups", "item_groups.csv");
        }

        httpRequest.Content = formData;
      }
      else if (requestBody != null)
      {
        StringContent reqContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
        httpRequest.Content = reqContent;
      }

      HttpResponseMessage response = await client.SendAsync(httpRequest);
      HttpContent resContent = response.Content;
      string result = await resContent.ReadAsStringAsync();

      return result;
    }

    /// <summary>
    /// Create and add authorization headers
    /// </summary>
    /// <param name="options">Main options object</param>
    /// <param name="requestHeaders">Headers to send with the request</param>
    /// <returns></returns>
    internal static void AddAuthHeaders(Hashtable options, Dictionary<string, string> requestHeaders)
    {
      if (!options.ContainsKey(Constants.API_TOKEN))
      {
        throw new ConstructorException("apiToken was not found");
      }

      string encodedToken = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{options["apiToken"]}:"));
      requestHeaders.Add("Authorization", $"Basic {encodedToken}");
    }
  }
}