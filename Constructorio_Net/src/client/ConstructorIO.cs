using System;

using System.Collections.Generic;

using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

/**
 * Constructor.io Client
 */
public class ConstructorIO
{

    /**
     * the HTTP client used by all instances
     */
    //private static readonly HttpClient client = new HttpClient();

    private static HttpClient client = new HttpClient();
        //.build();

    /**
     * the HTTP client used by all instances (with retry, only for idempotent requests like GET)
     */
    //private static HttpClient clientWithRetry = new HttpClient.Builder()
    //    .addInterceptor(new ConstructorInterceptor())
    //    .retryOnConnectionFailure(true)
    //    .build();

    /**
     * @return the HTTP client used by all instances
     */
    protected static HttpClient getClient()
    {
        return client;
    }

    private string credentials;
    public string apiToken;
    public string apiKey;
    public string protocol;
    public string host;
    public int port;
    public string version;
    public string constructorToken;

    /**
     * Creates a constructor.io Client.
     *
     * @param apiToken API Token, gotten from your <a href="https://constructor.io/dashboard">Constructor.io Dashboard</a>, and kept secret.
     * @param apiKey API Key, used publically in your in-site javascript client.
     * @param isHTTPS true to use HTTPS, false to use HTTP. It is highly recommended that you use HTTPS.
     * @param host The host of the autocomplete service that you are using. It is recommended that you let this value be null, in which case the host defaults to the Constructor.io autocomplete servic at ac.cnstrc.com.
     * @param constructorToken The token provided by Constructor to identify your company's traffic if proxying requests for results
     */
    public ConstructorIO(string apiToken, string apiKey, Boolean isHTTPS, string host, string constructorToken)
    {
        this.apiToken = apiToken;
        this.apiKey = apiKey;
        this.host = host;
        this.version = this.getVersion();
        if (host == null)
        {
            this.host = "ac.cnstrc.com";
        }
        if (isHTTPS)
        {
            this.protocol = "https";
        }
        else
        {
            this.protocol = "http";
        }
        this.constructorToken = constructorToken;
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(this.apiToken + ":");
        this.credentials = "Basic " + System.Convert.ToBase64String(plainTextBytes);
    }

    /**
     * Sets apiKey
     */
    public void setApiKey(string apiKey)
    {
        this.apiKey = apiKey;
    }

    /**
     * Makes a Http GET request
     */
    private async Task<string> makeGetRequest(Uri url)
    {
        var response = await client.GetAsync(url);
        var content = response.Content;
        var result = await content.ReadAsStringAsync();
        return result;
    }

    /**
     * Makes a Http POST request
     */
    private async Task<string> makePostRequest(Uri url, Dictionary<String, Object> dataObject)
    {
        var jsonData = JsonConvert.SerializeObject(dataObject);
        var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, data);
        var content = response.Content;
        var result = await content.ReadAsStringAsync();

        return result;
    }

    /**
     * Makes a Http PUT request
     */
    private async Task<string> makePutRequest(Uri url, Dictionary<String, Object> dataObject)
    {
        var jsonData = JsonConvert.SerializeObject(dataObject);
        var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await client.PutAsync(url, data);
        var content = response.Content;
        var result = await content.ReadAsStringAsync();

        return result;
    }

    /**
     * Makes a Http DELETE request
     */
    private async Task<string> makeDeleteRequest(Uri url, Dictionary<String, Object> dataObject)
    {
        var jsonData = JsonConvert.SerializeObject(dataObject);
        var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await client.DeleteAsync(url);
        var content = response.Content;
        var result = await content.ReadAsStringAsync();

        return result;
    }

    /**
      * Verifies that an autocomplete service is working.
      *
      * @return true if working.
      * @throws ConstructorException if the service is not working
      */
    public Boolean verify()
    {
        try {
            var url = this.makeUrl(new List<string> { "v1", "verify" }, null);
            var response = this.makeGetRequest(url);

            getResponseBody(response);
            return true;
        } catch (Exception exception) {
            throw new ConstructorException(exception);
        }
    }

    /**
     * Adds an item to your autocomplete.
     *
     * @param item the item that you're adding.
     * @param autocompleteSection the section of the autocomplete that you're adding the item to.
     * @return true if working
     * @throws ConstructorException if the request is invalid.
     */
    public Boolean addItem(ConstructorItem item, string autocompleteSection)
    {
        try {
            var url = this.makeUrl(new List<string> { "v1", "item" }, null);
            var data = item.toMap();

            data.Add("autocomplete_section", autocompleteSection);

            var response = this.makePostRequest(url, data);

            getResponseBody(response);

            return true;
        }
        catch (Exception exception) {
            throw new ConstructorException(exception);
        }
    }

    /**
     * Adds an item to your autocomplete or updates it if it already exists.
     *
     * @param item the item that you're adding.
     * @param autocompleteSection the section of the autocomplete that you're adding the item to.
     * @return true if working
     * @throws ConstructorException if the request is invalid.
     */
    public Boolean addOrUpdateItem(ConstructorItem item, string autocompleteSection)
    {
        try
        {
            var additionalQueryParams = new Dictionary<string, string>() { { "force", "1" } };
            var url = this.makeUrl(new List<string> { "v1", "item" }, additionalQueryParams);
            var data = item.toMap();

            data.Add("autocomplete_section", autocompleteSection);

            var response = this.makePutRequest(url, data);

            getResponseBody(response);

            return true;
        }
        catch (Exception exception)
        {
            throw new ConstructorException(exception);
        }
    }

    /**
     * Adds multiple items to your autocomplete (limit of 1000 items)
     *
     * @param items the items you want to add.
     * @param autocompleteSection the section of the autocomplete that you're adding the items to.
     * @return true if working
     * @throws ConstructorException if the request is invalid.
     */
    public Boolean addItemBatch(ConstructorItem[] items, string autocompleteSection)
    {
        try
        {
            var url = this.makeUrl(new List<string> { "v1", "batch_items" }, null);
            var data = new Dictionary<string, Object>();
            var itemsList = new List<Object>();

            foreach (var item in items)
            {
                itemsList.Add(item.toMap());
            }
            data.Add("items", itemsList);
            data.Add("section", autocompleteSection);

            var response = this.makePostRequest(url, data);

            getResponseBody(response);

            return true;
        }
        catch (Exception exception)
        {
            throw new ConstructorException(exception);
        }
    }

    /**
     * Adds multiple items to your autocomplete whilst updating existing ones (limit of 1000 items)
     *
     * @param items the items you want to add.
     * @param autocompleteSection the section of the autocomplete that you're adding the items to.
     * @return true if working
     * @throws ConstructorException if the request is invalid.
     */
    public Boolean addOrUpdateItemBatch(ConstructorItem[] items, string autocompleteSection)
    {
        try
        {
            var additionalQueryParams = new Dictionary<string, string>() { { "force", "1" } };
            var url = this.makeUrl(new List<string> { "v1", "batch_items" }, additionalQueryParams);
            var data = new Dictionary<string, Object>();
            var itemsList = new List<Object>();

            foreach (var item in items)
            {
                itemsList.Add(item.toMap());
            }
            data.Add("items", itemsList);
            data.Add("section", autocompleteSection);

            var response = this.makePutRequest(url, data);

            getResponseBody(response);

            return true;
        }
        catch (Exception exception)
        {
            throw new ConstructorException(exception);
        }
    }

    /**
     * Removes an item from your autocomplete.
     *
     * @param item the item that you're removing.
     * @param autocompleteSection the section of the autocomplete that you're removing the item from.
     * @return true if successfully removed
     * @throws ConstructorException if the request is invalid.
     */
    public Boolean removeItem(ConstructorItem item, string autocompleteSection)
    {
        try
        {
            HttpUrl url = this.makeUrl(Arrays.asList("v1", "item"));
            Map<string, Object> data = new HashMap<string, Object>();
            data.put("item_name", item.getItemName());
            data.put("autocomplete_section", autocompleteSection);
            string parameter = new Gson().toJson(data);
            RequestBody body = RequestBody.create(MediaType.parse("application/json; charset=utf-8"), parameter);
            Request request = this.makeAuthorizedRequestBuilder()
                .url(url)
                .delete(body)
                .build();

            Response response = client.newCall(request).execute();
            getResponseBody(response);
            return true;
        }
        catch (Exception exception)
        {
            throw new ConstructorException(exception);
        }
    }

    /**
     * Removes multiple items from your autocomplete (limit of 1000 items)
     *
     * @param items the items that you are removing
     * @param autocompleteSection the section of the autocomplete that you're removing the items from.
     * @return true if successfully removed
     * @throws ConstructorException if the request is invalid
     */
    public Boolean removeItemBatch(ConstructorItem[] items, string autocompleteSection)
    {
        try
        {
            HttpUrl url = this.makeUrl(Arrays.asList("v1", "batch_items"));
            Map<string, Object> data = new HashMap<string, Object>();
            List<Object> itemsAsJSON = new ArrayList<Object>();
            for (ConstructorItem item : items)
            {
                itemsAsJSON.add(item.toMap());
            }
            data.put("items", itemsAsJSON);
            data.put("autocomplete_section", autocompleteSection);
            string parameter = new Gson().toJson(data);
            RequestBody body = RequestBody.create(MediaType.parse("application/json; charset=utf-8"), parameter);
            Request request = this.makeAuthorizedRequestBuilder()
                .url(url)
                .delete(body)
                .build();

            Response response = client.newCall(request).execute();
            getResponseBody(response);
            return true;
        }
        catch (Exception exception)
        {
            throw new ConstructorException(exception);
        }
    }

    /**
     * Modifies an item from your autocomplete.
     *
     * @param item the item that you're modifying.
     * @param autocompleteSection the section of the autocomplete that you're modifying the item for.
     * @param previousItemName the previous name of the item.
     * @return true if successfully modified
     * @throws ConstructorException if the request is invalid.
     */
    public Boolean modifyItem(ConstructorItem item, string autocompleteSection, string previousItemName)
    {
        try
        {
            var url = this.makeUrl(new List<string> { "v1", "item" }, null);
            var data = item.toMap();

            data.Add("new_item_name", item.getItemName());
            data.Add("section", autocompleteSection);
            data.Add("item_name", previousItemName);

            var response = this.makePutRequest(url, data);

            getResponseBody(response);

            return true;
        }
        catch (Exception exception)
        {
            throw new ConstructorException(exception);
        }

        //try
        //{
        //    var additionalQueryParams = new Dictionary<string, string>() { { "force", "1" } };
        //    var url = this.makeUrl(new List<string> { "v1", "item" }, additionalQueryParams);
        //    var data = item.toMap();

        //    data.Add("autocomplete_section", autocompleteSection);

        //    var response = this.makePutRequest(url, data);

        //    getResponseBody(response);

        //    return true;
        //}
        //catch (Exception exception)
        //{
        //    throw new ConstructorException(exception);
        //}
    }

    /**
     * Queries the autocomplete service.
     *
     * Note that if you're making an autocomplete request for a website, you should definitely use our javascript client instead of doing it server-side!
     * That's important. That will be a solid latency difference.
     *
     * @param req the autocomplete request
     * @param userInfo optional information about the user
     * @return an autocomplete response
     * @throws ConstructorException if the request is invalid.
     */
    //public AutocompleteResponse autocomplete(AutocompleteRequest req, UserInfo userInfo)
    //{
    //    try {
    //        string json = autocompleteAsJSON(req, userInfo);
    //        return createAutocompleteResponse(json);
    //    } catch (Exception exception) {
    //        throw new ConstructorException(exception);
    //    }
    //}

    /**
     * Queries the autocomplete service.
     *
     * Note that if you're making an autocomplete request for a website, you should definitely use our javascript client instead of doing it server-side!
     * That's important. That will be a solid latency difference.
     *
     * @param req the autocomplete request
     * @param userInfo optional information about the user
     * @return a string of JSON
     * @throws ConstructorException if the request is invalid.
     */
    //public string autocompleteAsJSON(AutocompleteRequest req, UserInfo userInfo)
    //{
    //    try {
    //        List<string> paths = Arrays.asList("autocomplete", req.getQuery());
    //        HttpUrl url = (userInfo == null) ? this.makeUrl(paths) : this.makeUrl(paths, userInfo);

    //        for (Map.Entry<string, int> entry : req.getResultsPerSection().entrySet())
    //        {
    //            string section = entry.getKey();
    //            string count = string.valueOf(entry.getValue());
    //            url = url.newBuilder()
    //                .addQueryParameter("num_results_" + section, count)
    //                .build();
    //        }

    //        Request request = this.makeUserRequestBuilder(userInfo)
    //            .url(url)
    //            .get()
    //            .build();

    //        Response response = clientWithRetry.newCall(request).execute();
    //        return getResponseBody(response);
    //    } catch (Exception exception) {
    //        throw new ConstructorException(exception);
    //    }
    //}

    /**
     * Creates a search OkHttp request
     * 
     * @param req the search request
     * @param userInfo optional information about the user
     * @return a search OkHttp request
     * @throws ConstructorException
     */
    //protected Request createSearchRequest(SearchRequest req, UserInfo userInfo)
    //{
    //    try {
    //        List<string> paths = Arrays.asList("search", req.getQuery());
    //        HttpUrl url = (userInfo == null) ? this.makeUrl(paths) : this.makeUrl(paths, userInfo);
    //        url = url.newBuilder()
    //            .addQueryParameter("section", req.getSection())
    //            .addQueryParameter("page", string.valueOf(req.getPage()))
    //            .addQueryParameter("num_results_per_page", string.valueOf(req.getResultsPerPage()))
    //            .build();

    //        if (req.getGroupId() != null)
    //        {
    //            url = url.newBuilder()
    //                .addQueryParameter("filters[group_id]", req.getGroupId())
    //                .build();
    //        }

    //        for (string facetName : req.getFacets().keySet())
    //        {
    //            for (string facetValue : req.getFacets().get(facetName))
    //            {
    //                url = url.newBuilder()
    //                    .addQueryParameter("filters[" + facetName + "]", facetValue)
    //                    .build();
    //            }
    //        }

    //        if (stringUtils.isNotBlank(req.getSortBy()))
    //        {
    //            url = url.newBuilder()
    //                .addQueryParameter("sort_by", req.getSortBy())
    //                .addQueryParameter("sort_order", req.getSortAscending() ? "ascending" : "descending")
    //                .build();
    //        }

    //        if (req.getCollectionId() != null)
    //        {
    //            url = url.newBuilder()
    //            .addQueryParameter("collection_id", req.getCollectionId())
    //            .build();
    //        }

    //        Request request = this.makeUserRequestBuilder(userInfo)
    //            .url(url)
    //            .get()
    //            .build();

    //        return request;
    //    } catch (Exception exception) {
    //        throw new ConstructorException(exception);
    //    }
    //}

    /**
     * Queries the search service.
     *
     * Note that if you're making a search request for a website, you should definitely use our javascript client instead of doing it server-side!
     * That's important. That will be a solid latency difference.
     *
     * @param req the search request
     * @param userInfo optional information about the user
     * @return a search response
     * @throws ConstructorException if the request is invalid.
     */
    //public SearchResponse search(SearchRequest req, UserInfo userInfo)
    //{
    //    try {
    //        Request request = createSearchRequest(req, userInfo);
    //        Response response = clientWithRetry.newCall(request).execute();
    //        string json = getResponseBody(response);
    //        return createSearchResponse(json);
    //    } catch (Exception exception) {
    //        throw new ConstructorException(exception);
    //    }
    //}

    /**
     * Queries the search service.
     *
     * Note that if you're making a search request for a website, you should definitely use our javascript client instead of doing it server-side!
     * That's important. That will be a solid latency difference.
     *
     * @param req the search request
     * @param userInfo optional information about the user
     * @param c a callback with success and failure conditions
     * @throws ConstructorException if the request is invalid.
     */
    //public void search(SearchRequest req, UserInfo userInfo, SearchCallback c)
    //{
    //    try {
    //        Request request = createSearchRequest(req, userInfo);
    //        clientWithRetry.newCall(request).enqueue(new Callback() {
    //                @Override
    //                public void onFailure(Call call, IOException e)
    //            {
    //                c.onFailure(new ConstructorException(e));
    //            }

    //        @Override
    //                public void onResponse(Call call, Response response)
    //                {
    //                    try {
    //                        string json = getResponseBody(response);
    //                        SearchResponse res = createSearchResponse(json);
    //                        c.onResponse(res);
    //                    }
    //                    catch (Exception e) {
    //                        c.onFailure(new ConstructorException(e));
    //                    }
    //                }
    //        });
    //    } catch (Exception exception) {
    //        throw new ConstructorException(exception);
    //    }
    //}

    /**
     * Queries the search service.
     *
     * Note that if you're making a search request for a website, you should definitely use our javascript client instead of doing it server-side!
     * That's important. That will be a solid latency difference.
     *
     * @param req the search request
     * @param userInfo optional information about the user
     * @return a string of JSON
     * @throws ConstructorException if the request is invalid.
     */
    //public string searchAsJSON(SearchRequest req, UserInfo userInfo)
    //{
    //    try {
    //        Request request = createSearchRequest(req, userInfo);
    //        Response response = clientWithRetry.newCall(request).execute();
    //        return getResponseBody(response);
    //    } catch (Exception exception) {
    //        throw new ConstructorException(exception);
    //    }
    //}
    /**
     * Creates a browse OkHttp request
     * 
     * @param req the browse request
     * @param userInfo optional information about the user
     * @return a browse OkHttp request
     * @throws ConstructorException
     */
    //protected Request createBrowseRequest(BrowseRequest req, UserInfo userInfo)
    //{
    //        try {
    //        List<string> paths = Arrays.asList("browse", req.getFilterName(), req.getFilterValue());
    //        HttpUrl url = (userInfo == null) ? this.makeUrl(paths) : this.makeUrl(paths, userInfo);
    //        url = url.newBuilder()
    //            .addQueryParameter("section", req.getSection())
    //            .addQueryParameter("page", string.valueOf(req.getPage()))
    //            .addQueryParameter("num_results_per_page", string.valueOf(req.getResultsPerPage()))
    //            .build();

    //        if (req.getGroupId() != null)
    //        {
    //            url = url.newBuilder()
    //                .addQueryParameter("filters[group_id]", req.getGroupId())
    //                .build();
    //        }

    //        for (string facetName : req.getFacets().keySet())
    //        {
    //            for (string facetValue : req.getFacets().get(facetName))
    //            {
    //                url = url.newBuilder()
    //                    .addQueryParameter("filters[" + facetName + "]", facetValue)
    //                    .build();
    //            }
    //        }

    //        if (stringUtils.isNotBlank(req.getSortBy()))
    //        {
    //            url = url.newBuilder()
    //                .addQueryParameter("sort_by", req.getSortBy())
    //                .addQueryParameter("sort_order", req.getSortAscending() ? "ascending" : "descending")
    //                .build();
    //        }

    //        Request request = this.makeUserRequestBuilder(userInfo)
    //            .url(url)
    //            .get()
    //            .build();

    //        return request;
    //    } catch (Exception exception) {
    //        throw new ConstructorException(exception);
    //    }
    //}

    /**
     * Queries the browse service.
     *
     * Note that if you're making a browse request for a website, you should definitely use our javascript client instead of doing it server-side!
     * That's important. That will be a solid latency difference.
     *
     * @param req the browse request
     * @param userInfo optional information about the user
     * @return a browse response
     * @throws ConstructorException if the request is invalid.
     */
    //public BrowseResponse browse(BrowseRequest req, UserInfo userInfo)
    //{
    //    try {
    //        Request request = createBrowseRequest(req, userInfo);
    //        Response response = clientWithRetry.newCall(request).execute();
    //        string json = getResponseBody(response);
    //        return createBrowseResponse(json);
    //    } catch (Exception exception) {
    //        throw new ConstructorException(exception);
    //    }
    //}

    /**
     * Queries the browse service.
     *
     * Note that if you're making a browse request for a website, you should definitely use our javascript client instead of doing it server-side!
     * That's important. That will be a solid latency difference.
     *
     * @param req the browse request
     * @param userInfo optional information about the user
     * @param c a callback with success and failure conditions
     * @throws ConstructorException if the request is invalid.
     */
    //public void browse(BrowseRequest req, UserInfo userInfo, final BrowseCallback c)
    //{
    //    try {
    //        Request request = createBrowseRequest(req, userInfo);
    //        clientWithRetry.newCall(request).enqueue(new Callback() {
    //                @Override
    //                public void onFailure(Call call, IOException e)
    //        {
    //            c.onFailure(new ConstructorException(e));
    //        }

    //        @Override
    //                public void onResponse(Call call, final Response response) throws IOException {
    //            try
    //            {
    //                string json = getResponseBody(response);
    //                BrowseResponse res = createBrowseResponse(json);
    //                c.onResponse(res);
    //            }
    //            catch (Exception e)
    //            {
    //                c.onFailure(new ConstructorException(e));
    //            }
    //        }
    //    });
    //} catch (Exception exception)
    //{
    //    throw new ConstructorException(exception);
    //}
    //    }

    /**
     * Queries the browse service.
     *
     * Note that if you're making a browse request for a website, you should definitely use our javascript client instead of doing it server-side!
     * That's important. That will be a solid latency difference.
     *
     * @param req the browse request
     * @param userInfo optional information about the user
     * @return a string of JSON
     * @throws ConstructorException if the request is invalid.
     */
    //    public string browseAsJSON(BrowseRequest req, UserInfo userInfo)
    //{
    //      try {
    //        Request request = createBrowseRequest(req, userInfo);
    //        Response response = clientWithRetry.newCall(request).execute();
    //        return getResponseBody(response);
    //    } catch (Exception exception) {
    //        throw new ConstructorException(exception);
    //    }
    //}

    /**
     * Queries the search service with natural language processing.
     *
     * Note that if you're making a search request for a website, you should definitely use our javascript client instead of doing it server-side!
     * That's important. That will be a solid latency difference.
     *
     * @param req the natural language search request
     * @param userInfo optional information about the user
     * @return a search response
     * @throws ConstructorException if the request is invalid.
     */
    //public SearchResponse naturalLanguageSearch(NaturalLanguageSearchRequest req, UserInfo userInfo)
    //{
    //        try {
    //        string json = naturalLanguageSearchAsJSON(req, userInfo);
    //        return createSearchResponse(json);
    //    } catch (Exception exception) {
    //        throw new ConstructorException(exception);
    //    }
    //}

    /**
     * Queries the search service with natural language processing.
     *
     * Note that if you're making a search request for a website, you should definitely use our javascript client instead of doing it server-side!
     * That's important. That will be a solid latency difference.
     *
     * @param req the natural language search request
     * @param userInfo optional information about the user
     * @return a string of JSON
     * @throws ConstructorException if the request is invalid.
     */
    //public string naturalLanguageSearchAsJSON(NaturalLanguageSearchRequest req, UserInfo userInfo)
    //{
    //    try {
    //        List<string> paths = Arrays.asList("search", "natural_language", req.getQuery());
    //        HttpUrl url = (userInfo == null) ? this.makeUrl(paths) : this.makeUrl(paths, userInfo);

    //        url = url.newBuilder()
    //            .addQueryParameter("section", req.getSection())
    //            .addQueryParameter("page", string.valueOf(req.getPage()))
    //            .addQueryParameter("num_results_per_page", string.valueOf(req.getResultsPerPage()))
    //            .build();

    //        Request request = this.makeUserRequestBuilder(userInfo)
    //            .url(url)
    //            .get()
    //            .build();

    //        Response response = clientWithRetry.newCall(request).execute();
    //        return getResponseBody(response);
    //    } catch (Exception exception) {
    //        throw new ConstructorException(exception);
    //    }
    //}

    /**
     * Queries the recommendations service to retrieve results.
     *
     * Note that if you're making a recommendations request for a website, you should definitely use our javascript client instead of doing it server-side!
     * That's important. That will be a solid latency difference.
     *
     * @param req the recommendations request
     * @param userInfo optional information about the user
     * @return a recommendations response
     * @throws ConstructorException if the request is invalid.
     */
    //public RecommendationsResponse recommendations(RecommendationsRequest req, UserInfo userInfo)
    //{
    //        try {
    //        string json = recommendationsAsJSON(req, userInfo);
    //        return createRecommendationsResponse(json);
    //    } catch (Exception exception) {
    //        throw new ConstructorException(exception);
    //    }
    //}

    /**
     * Queries the recommendations service to retrieve results.
     *
     * Note that if you're making an recommendations request for a website, you should definitely use our javascript client instead of doing it server-side!
     * That's important. That will be a solid latency difference.
     *
     * @param req the recommendations request
     * @param userInfo optional information about the user
     * @return a string of JSON
     * @throws ConstructorException if the request is invalid.
     */
    //public string recommendationsAsJSON(RecommendationsRequest req, UserInfo userInfo)
    //{
    //    try {
    //        List<string> paths = Arrays.asList("recommendations", "v1", "pods", req.getPodId());
    //        HttpUrl url = (userInfo == null) ? this.makeUrl(paths) : this.makeUrl(paths, userInfo);

    //        url = url.newBuilder()
    //            .addQueryParameter("num_results", string.valueOf(req.getNumResults()))
    //            .addQueryParameter("section", req.getSection())
    //            .build();

    //        if (req.getItemIds() != null)
    //        {
    //            for (string itemId : req.getItemIds())
    //            {
    //                url = url.newBuilder()
    //                    .addQueryParameter("item_id", itemId)
    //                    .build();
    //            }
    //        }

    //        Request request = this.makeUserRequestBuilder(userInfo)
    //            .url(url)
    //            .get()
    //            .build();

    //        Response response = clientWithRetry.newCall(request).execute();
    //        return getResponseBody(response);
    //    } catch (Exception exception) {
    //        throw new ConstructorException(exception);
    //    }
    //}

    /**
     * Makes a URL to issue the requests to.  Note that the URL will automagically have the apiKey embedded.
     *
     * @param path endpoint of the autocomplete service.
     * @return the created URL. Now you can use it to issue requests and things!
     */
    protected Uri makeUrl(List<String> paths, Dictionary<String, String> queryParams)
    {
        var uriBuilder = new UriBuilder();

        uriBuilder.Scheme = this.protocol;
        uriBuilder.Host = this.host;
        uriBuilder.Query = "key=" + this.apiKey + "&c=" + this.version;

        if (queryParams != null && queryParams.Count != 0)
        {
            foreach (var queryParam in queryParams)
            {
                uriBuilder.Query += "&" + queryParam.Key + "=" + queryParam.Value;
            }
        }

        foreach (var path in paths)
        {
            uriBuilder.Path += "/" + paths;
        }

        if (this.port != null)
        {
            uriBuilder.Port = this.port;
        }

        return uriBuilder.Uri;
    }

    /**
     * Creates a builder for an authorized request
     *
     * @return Request Builder
     */
    protected Builder makeAuthorizedRequestBuilder()
    {
        Builder builder = new Request.Builder();
        builder.addHeader("Authorization", this.credentials);
        return builder;
    }

    /**
     * Creates a builder for an end user request
     *
     * @param info user information if available
     * @return Request Builder
     */
    //protected Builder makeUserRequestBuilder(UserInfo info)
    //{
    //    Builder builder = new Request.Builder();
    //    if (this.constructorToken != null)
    //    {
    //        builder.addHeader("x-cnstrc-token", this.constructorToken);
    //    }
    //    if (info != null && info.getForwardedFor() != null)
    //    {
    //        builder.addHeader("x-forwarded-for", info.getForwardedFor());
    //    }
    //    if (info != null && info.getUserAgent() != null)
    //    {
    //        builder.addHeader("User-Agent", info.getUserAgent());
    //    }
    //    return builder;
    //}

    /**
     * Checks the response from an endpoint and throws an exception if an error occurred
     *
     * @return whether the request was successful
     */
    protected static string getResponseBody(Task<string> response)
    {
        string errorMessage = "Unknown error";
        try {
            var body = response.Result;

            if (response.isSuccessful())
            {
                //return body;
            }
            else
            {
                //var error = new Gson().fromJson(body, ServerError.class);
                //errorMessage = "[HTTP " + response.code() + "] " + error.getMessage();
            }
        } catch (Exception e) {
            //errorMessage = "[HTTP " + response.code() + "]";
            //finally
            //{
            //    response.close();
            //}
            throw new ConstructorException(errorMessage);
        }
    }

   /**
    * Grabs the version number (hard coded ATM)
    *
    * @return version number
    */
    protected string getVersion()
    {
        return "ciojava-5.6.0";
    }

/**
 * Transforms a JSON string to a new JSON string for easy Gson parsing into an autocomplete response.
 * Using JSON objects to acheive this is considerably less error prone than attempting to do it in
 * a Gson Type Adapter.
 */
//protected static AutocompleteResponse createAutocompleteResponse(string string)
//{
//    JSONObject json = new JSONObject(string);
//    JSONObject sections = json.getJSONObject("sections");
//    for (Object sectionKey : sections.keySet()) {
//    string sectionName = (string)sectionKey;
//    JSONArray results = sections.getJSONArray(sectionName);
//    moveMetadataOutOfResultData(results);
//}
//string transformed = json.tostring();
//return new Gson().fromJson(transformed, AutocompleteResponse.class);
//    }

    /**
     * Transforms a JSON string to a new JSON string for easy Gson parsing into an search response.
     * Using JSON objects to acheive this is considerably less error prone than attempting to do it in
     * a Gson Type Adapter.
     */
//    protected static SearchResponse createSearchResponse(string string)
//{
//    JSONObject json = new JSONObject(string);
//    JSONObject response = json.getJSONObject("response");
//    JSONArray results;

//    if (!response.isNull("results"))
//    {
//        results = response.getJSONArray("results");
//        moveMetadataOutOfResultData(results);
//    }

//    string transformed = json.tostring();
//    return new Gson().fromJson(transformed, SearchResponse.class);
//    }

    /**
     * Transforms a JSON string to a new JSON string for easy Gson parsing into an browse response.
     * Using JSON objects to acheive this is considerably less error prone than attempting to do it in
     * a Gson Type Adapter.
     */
//    protected static BrowseResponse createBrowseResponse(string string)
//{
//    JSONObject json = new JSONObject(string);
//    JSONObject response = json.getJSONObject("response");
//    JSONArray results = response.getJSONArray("results");
//    moveMetadataOutOfResultData(results);
//    string transformed = json.tostring();
//    return new Gson().fromJson(transformed, BrowseResponse.class);
//  }

    /**
     * Transforms a JSON string to a new JSON string for easy Gson parsing into an recommendations response.
     * Using JSON objects to acheive this is considerably less error prone than attempting to do it in
     * a Gson Type Adapter.
     */
//    protected static RecommendationsResponse createRecommendationsResponse(string string)
//{
//    JSONObject json = new JSONObject(string);
//    JSONObject response = json.getJSONObject("response");
//    JSONArray results = response.getJSONArray("results");
//    moveMetadataOutOfResultData(results);
//    string transformed = json.tostring();
//    return new Gson().fromJson(transformed, RecommendationsResponse.class);
//    }

    /**
     * Moves metadata out of the result data for an array of results
     * @param results A JSON array of results
     */
//    protected static void moveMetadataOutOfResultData(JSONArray results)
//{
//    for (int i = 0; i < results.length(); i++)
//    {

//        JSONObject result = results.getJSONObject(i);
//        JSONObject resultData = result.getJSONObject("data");
//        JSONObject metadata = new JSONObject();

//        // Recursive call to move unspecified properties in result variations to it's metadata object
//        if (!result.isNull("variations"))
//        {
//            JSONArray variations = result.getJSONArray("variations");
//            moveMetadataOutOfResultData(variations);
//        }

//        // Move unspecified properties in result data object to metadata object
//        for (Object propertyKey : resultData.keySet()) {
//    string propertyName = (string)propertyKey;
//    if (!propertyName.matches("(description|id|url|image_url|groups|facets|variation_id)"))
//    {
//        metadata.put(propertyName, resultData.get(propertyName));
//    }
//}

//// Remove unspecified properties from result data object
//for (Object propertyKey : metadata.keySet())
//{
//    string propertyName = (string)propertyKey;
//    resultData.remove(propertyName);
//}

//// Add metadata to result data object
//resultData.put("metadata", metadata);
//        }
//    }
}
