using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Constructorio_NET.Models;
using Newtonsoft.Json;

namespace Constructorio_NET.Utils
{
    public class Helpers
    {
        private static readonly HttpClient Client = new HttpClient();

        protected Helpers()
        {
        }

        /// <summary>
        /// Method in order to modify a string to ensure proper url encoding.
        /// </summary>
        /// <param name="str">String to be encoded.</param>
        /// <returns>Url encoded string.</returns>
        protected static string OurEscapeDataString(string str)
        {
            string encodedString = Regex.Replace(str, @"\s", " ");
            encodedString = Uri.EscapeDataString(encodedString);

            return encodedString;
        }

        /// <summary>
        /// Cleans params before applying them to a request url.
        /// </summary>
        /// <param name="parameters">Parameters to be cleaned.</param>
        /// <returns>Hashtable of cleaned parameters.</returns>
        protected static Hashtable CleanParams(Hashtable parameters)
        {
            Hashtable cleanedParams = new Hashtable();

            foreach (DictionaryEntry param in parameters)
            {
                if (param.Value is string)
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
        /// Makes a URL to issue requests with.
        /// </summary>
        /// <param name="options">Hashtable of options from Constructorio instantiation.</param>
        /// <param name="paths">List of url paths.</param>
        /// <param name="queryParams">Hashtable of query params.</param>
        /// <param name="omittedQueryParams">Dictionary of query params to omit.</param>
        /// <returns>string.</returns>
        protected static string MakeUrl(Hashtable options, List<string> paths, Hashtable queryParams, Dictionary<string, bool> omittedQueryParams = null)
        {
            StringBuilder url = new StringBuilder((string)options[Constants.SERVICE_URL]);

            foreach (var path in paths)
            {
                url.Append($"/{HttpUtility.UrlPathEncode(path)}");
            }

            url.Append($"?{Constants.API_KEY}={options[Constants.API_KEY]}");

            if (omittedQueryParams == null || !omittedQueryParams.ContainsKey("c"))
            {
                url.Append($"&{Constants.VERSION}={options[Constants.VERSION]}");
            }

            // Generate url params query string
            if (queryParams != null && queryParams.Count != 0)
            {
                if (queryParams.Contains(Constants.CLIENT_ID))
                {
                    url.Append($"&{Constants.CLIENT_ID}={OurEscapeDataString((string)queryParams[Constants.CLIENT_ID])}");
                    queryParams.Remove(Constants.CLIENT_ID);
                }

                if (queryParams.Contains(Constants.SESSION_ID))
                {
                    url.Append($"&{Constants.SESSION_ID}={OurEscapeDataString(queryParams[Constants.SESSION_ID].ToString())}");
                    queryParams.Remove(Constants.SESSION_ID);
                }

                // Add filters to query string
                if (queryParams.Contains(Constants.FILTERS))
                {
                    Dictionary<string, List<string>> filters = (Dictionary<string, List<string>>)queryParams[Constants.FILTERS];
                    queryParams.Remove(Constants.FILTERS);

                    foreach (var filter in filters)
                    {
                        string filterGroup = filter.Key;

                        foreach (string filterOption in filter.Value)
                        {
                            url.Append($"&{Constants.FILTERS}{OurEscapeDataString("[" + filterGroup + "]")}={OurEscapeDataString(filterOption)}");
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
                        url.Append($"&ef-{OurEscapeDataString(testCell.Key)}={OurEscapeDataString(testCell.Value)}");
                    }
                }

                // Add format options to query string
                if (queryParams.Contains(Constants.FMT_OPTIONS))
                {
                    Dictionary<string, string> fmtOptions = (Dictionary<string, string>)queryParams[Constants.FMT_OPTIONS];
                    queryParams.Remove(Constants.FMT_OPTIONS);

                    foreach (var fmtOption in fmtOptions)
                    {
                        url.Append($"&{Constants.FMT_OPTIONS}{OurEscapeDataString("[" + fmtOption.Key + "]")}={OurEscapeDataString(fmtOption.Value)}");
                    }
                }

                // Add hidden fields as fmt_options
                if (queryParams.Contains(Constants.HIDDEN_FIELDS))
                {
                    List<string> hiddenFields = (List<string>)queryParams[Constants.HIDDEN_FIELDS];
                    queryParams.Remove(Constants.HIDDEN_FIELDS);

                    foreach (var hiddenField in hiddenFields)
                    {
                        url.Append($"&{Constants.FMT_OPTIONS}{OurEscapeDataString("[" + Constants.HIDDEN_FIELDS + "]")}={OurEscapeDataString(hiddenField)}");
                    }
                }

                // Add hidden facets as fmt_options
                if (queryParams.Contains(Constants.HIDDEN_FACETS))
                {
                    List<string> hiddenFacets = (List<string>)queryParams[Constants.HIDDEN_FACETS];
                    queryParams.Remove(Constants.HIDDEN_FACETS);

                    foreach (var hiddenFacet in hiddenFacets)
                    {
                        url.Append($"&{Constants.FMT_OPTIONS}{OurEscapeDataString("[" + Constants.HIDDEN_FACETS + "]")}={OurEscapeDataString(hiddenFacet)}");
                    }
                }

                // Add quiz answers to query string
                if (queryParams.Contains(Constants.ANSWERS))
                {
                    List<List<string>> answers = (List<List<string>>)queryParams[Constants.ANSWERS];
                    queryParams.Remove(Constants.ANSWERS);

                    foreach (var answer in answers)
                    {
                        url.Append($"&{Constants.ANSWERS}={OurEscapeDataString(string.Join(",", answer))}");
                    }
                }

                // Add remaining query params to query string
                foreach (DictionaryEntry queryParam in queryParams)
                {
                    string paramKey = (string)queryParam.Key;
                    Type valueDataType = queryParam.Value.GetType();

                    if (valueDataType == typeof(string))
                    {
                        url.Append($"&{OurEscapeDataString(paramKey)}={OurEscapeDataString((string)queryParam.Value)}");
                    }
                    else if (valueDataType == typeof(int))
                    {
                        url.Append($"&{OurEscapeDataString(paramKey)}={OurEscapeDataString(queryParam.Value.ToString())}");
                    }
                    else if (valueDataType == typeof(List<string>))
                    {
                        foreach (string listValue in (List<string>)queryParam.Value)
                        {
                            url.Append($"&{paramKey}={OurEscapeDataString(listValue)}");
                        }
                    }
                    else if (valueDataType == typeof(bool))
                    {
                        url.Append($"&{OurEscapeDataString(paramKey)}={OurEscapeDataString(queryParam.Value.ToString())}");
                    }
                }

                if (omittedQueryParams == null || !omittedQueryParams.ContainsKey("_dt"))
                {
                    long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    url.Append($"&_dt={time}");
                }
            }

            return url.ToString();
        }

        /// <summary>
        /// Makes a http request.
        /// </summary>
        /// <param name="options">Hashtable of options from Constructorio instantiation.</param>
        /// <param name="httpMethod">HTTP request method.</param>
        /// <param name="url">Url for the request.</param>
        /// <param name="requestHeaders">Additional headers to send with the request.</param>
        /// <param name="requestBody">Key values pairs used for the POST body.</param>
        /// <param name="files">Dictionary of streamcontent.</param>
        /// <returns>Task.</returns>
        public static async Task<string> MakeHttpRequest(Hashtable options, HttpMethod httpMethod, string url, Dictionary<string, string> requestHeaders, object requestBody = null, Dictionary<string, StreamContent> files = null)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(httpMethod, url);

            foreach (var header in requestHeaders)
            {
                if (header.Key == Constants.USER_AGENT) {
                    httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
                } else {
                    httpRequest.Headers.Add(header.Key, header.Value);
                }
            }

            if (options.ContainsKey(Constants.CONSTRUCTOR_TOKEN))
            {
                httpRequest.Headers.Add(Constants.SECURITY_TOKEN, (string)options[Constants.CONSTRUCTOR_TOKEN]);
            }

            if (files != null)
            {
                var formData = new MultipartFormDataContent();

                if (files.ContainsKey("items"))
                {
                    formData.Add(files["items"], "items", "items.csv");
                }

                if (files.ContainsKey("variations"))
                {
                    formData.Add(files["variations"], "variations", "variations.csv");
                }

                if (files.ContainsKey("item_groups"))
                {
                    formData.Add(files["item_groups"], "item_groups", "item_groups.csv");
                }

                httpRequest.Content = formData;
            }
            else if (requestBody != null)
            {
                StringContent reqContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                httpRequest.Content = reqContent;
            }

            HttpResponseMessage response = await Client.SendAsync(httpRequest);
            HttpContent resContent = response.Content;
            string result = await resContent.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                ServerError error = JsonConvert.DeserializeObject<ServerError>(result);
                throw new ConstructorException($"Http[{(int)response.StatusCode}]: {error.Message}");
            }

            return result;
        }

        /// <summary>
        /// Create and add authorization headers.
        /// </summary>
        /// <param name="options">Main options object.</param>
        /// <param name="requestHeaders">Headers to send with the request.</param>
        internal static void AddAuthHeaders(Hashtable options, Dictionary<string, string> requestHeaders)
        {
            if (!options.ContainsKey(Constants.API_TOKEN))
            {
                throw new ConstructorException("apiToken was not found");
            }

            string encodedToken = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{options[Constants.API_TOKEN]}:"));
            requestHeaders.Add("Authorization", $"Basic {encodedToken}");
        }
    }
}
