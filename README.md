# Constructor-IO Netcore Client

[Constructor.io](http://constructor.io/) provides search as a service that optimizes results using artificial intelligence (including natural language processing, re-ranking to optimize for conversions, and user personalization).

Full API documentation is available on [Github Pages](https://constructor-io.github.io/constructorio-netcore/)

# Installation

1. Follow the directions at [nuget](https://www.nuget.org/packages/constructor.io/) to add the client to your project.
2. Retrieve your autocomplete token and key.  You can find this at your [Constructor.io dashboard](https://constructor.io/dashboard).
3. Create a new instance of the client.
```csharp
ConstructorioConfig config = new ConstructorioConfig("apiKey", "apiToken");
ConstructorIO constructorio = new ConstructorIO(config);
```

# Uploading a Catalog

To upload your product catalog, you will need to create a `CatalogRequest`.  In this request, you can specify the files you want to upload (items, variations, and item groups) and the section you want to send the upload.  You can also set a notification e-mail to be alerted when a file ingestion fails.

```csharp
// Create a files dictionary and add the relevant files
StreamContent itemsStream = new StreamContent(File.OpenRead("./catalog/items.csv"));
itemsStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
StreamContent variationsStream = new StreamContent(File.OpenRead("./catalog/variations.csv"));
variationsStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
StreamContent itemGroupsStream = new StreamContent(File.OpenRead("./catalog/item_groups.csv"));
itemGroupsStream.Headers.ContentType = new MediaTypeHeaderValue("text/csv");

Dictionary<string, StreamContent> files = new Dictionary<string, StreamContent>()
{
    { "items", itemsStream },
    { "variations", variationsStream },
    { "item_groups", itemGroupsStream },
};

// Create a CatalogRequest with files to upload and the section to upload to
CatalogRequest request = new CatalogRequest(files);
request.Section = "Products";

// Set a notification e-mail
request.NotificationEmail = "integration@company.com";

// Set the force flag if the catalog should be processed even if it will invalidate a large number of existing items
request.Force = true;

// Send a request to replace the catalog (sync)
CatalogResponse response = constructorio.Catalog.ReplaceCatalog(request);
```

# Retrieving Autocomplete Results

To retrieve autocomplete results, you will need to create an `AutocompleteRequest`. In this request you can specify the number of results you want per autocomplete section.  If the results are for a specific user, you can also create a `UserInfo` object, which will allow you to retrieve personalized results.

```csharp
// Create an AutocompleteRequest with the term to request results for
AutocompleteRequest request = new AutocompleteRequest("rain coat");

// Define the number of results to show per section
request.ResultsPerSection = new Dictionary<string, int>
{
    { "Products", 6 },
    { "Search Suggestions", 8 },
};

// Create a UserInfo object with the unique device identifier and session
UserInfo userInfo = new UserInfo("device-id-1123123", 5);
request.UserInfo = userInfo;

// Add a variations map to request specific variation attributes as an array or object (optional)
VariationsMap variationsMap = new VariationsMap();
variationMap.AddGroupByRule("url", "data.url");
variationMap.AddValueRule("variation_id", AggregationTypes.First, "data.variation_id");
variationMap.AddValueRule("deactivated", AggregationTypes.First, "data.deactivated");
request.VariationMap = variationsMap;

// Request results as an object
AutocompleteResponse response = constructorio.Autocomplete.GetAutocompleteResults(request);
```

# Retrieving Search Results

To retrieve search results, you will need to create a `SearchRequest`. In this request you can specify the number of results you want per page, the page you want, sorting instructions, and also filter the results by category or facets. If the results are for a specific user, you can also create a `UserInfo` object, which will allow you to retrieve personalized results.

```csharp
// Create a SearchRequest with the term to request results for
SearchRequest request = new SearchRequest("peanut butter");

// Add in additional parameters
request.ResultsPerPage = 5;
request.Page = 1;
request.SortBy = "Price";
request.Filters = new Dictionary<string, List<string>>()
{
    { "Brand", new List<string>() { "Jif" } }
};

// Add the following paramaters to request for hidden fields or facets
request.HiddenFields = new List<string>
{
    "hidden_price_field",
    "hidden_brand_facet",
}

// Create a UserInfo object with the unique device identifier and session
UserInfo userInfo = new UserInfo("device-id-1123123", 5);
request.UserInfo = userInfo;

// Add a variations map to request specific variation attributes as an array or object (optional)
VariationsMap variationsMap = new VariationsMap();
variationMap.AddGroupByRule("url", "data.url");
variationMap.AddValueRule("variation_id", AggregationTypes.First, "data.variation_id");
variationMap.AddValueRule("deactivated", AggregationTypes.First, "data.deactivated");
request.VariationMap = variationsMap;

// Request results as an object
SearchResponse res = constructorio.Search.GetSearchResults(request);
```

# Retrieving Browse Results

To retrieve browse results, you will need to create a `BrowseRequest`. When creating the `BrowseRequest` the filter name can be one of `collection_id`, `group_id`, or a facet name (i.e. Brand). In this request, you can also specify the number of results you want per page, the page you want, sorting instructions, and also filter the results by category or facets. If the results are for a specific user, you can also create a `UserInfo` object, which will allow you to retrieve personalized results.

```csharp
// Create a BrowseRequest with the filter name and filter value to request results for
BrowseRequest request = new BrowseRequest("group_id", "8193");

// Add in additional parameters
request.ResultsPerPage = 5;
request.Page = 1;
request.SortBy = "Price";
request.Filters = new Dictionary<string, List<string>>()
{
    { "Brand", new List<string>() { "Jif" } }
};

// Add the following paramaters to request for hidden fields or facets
request.HiddenFields = new List<string>
{
    "hidden_price_field",
    "hidden_brand_facet",
}

// Create a UserInfo object with the unique device identifier and session
UserInfo userInfo = new UserInfo("device-id-1123123", 5);
request.UserInfo = userInfo;

// Add a variations map to request specific variation attributes as an array or object (optional)
VariationsMap variationsMap = new VariationsMap();
variationMap.AddGroupByRule("url", "data.url");
variationMap.AddValueRule("variation_id", AggregationTypes.First, "data.variation_id");
variationMap.AddValueRule("deactivated", AggregationTypes.First, "data.deactivated");
request.VariationMap = variationsMap;

// Request results as an object
BrowseResponse response = constructor.browse(request);
```

# Retrieving Browse Results for Item ID's

To retrieve browse results for a supplied list of item ID's, you will need to create a `BrowseItemsRequest`. When creating the `BrowseItemsRequest` the `itemIds` parameter will be a list of item ID's. In this request, you can also specify the number of results you want per page, the page you want, sorting instructions, and also filter the results by category or facets. If the results are for a specific user, you can also create a `UserInfo` object, which will allow you to retrieve personalized results.

```csharp
// Create a BrowseItemsRequest with the filter name and filter value to request results for
BrowseItemsRequest request = new BrowseItemsRequest(Arrays.asList("t-shirt-xxl"));

// Add in additional parameters
request.ResultsPerPage = 5;
request.Page = 1;
request.SortBy = "Price";
request.Filters = new Dictionary<string, List<string>>()
{
    { "Brand", new List<string>() { "Jif" } }
};

// Add the following paramaters to request for hidden fields or facets
request.HiddenFields = new List<string>
{
    "hidden_price_field",
    "hidden_brand_facet",
}

// Create a UserInfo object with the unique device identifier and session
UserInfo userInfo = new UserInfo("device-id-1123123", 5);
request.UserInfo = userInfo;

// Request results as an object
BrowseResponse response = constructor.browseItems(request, userInfo);
```

# Retrieving Recommendation Results

To retrieve recommendation results, you will need to create a `RecommendationsRequest`. In this request, you can also specify the number of results you want and the items (given the ids) that you want to retrieve recommendations for. If the results are for a specific user, you can also create a `UserInfo` object, which will allow you to retrieve personalized results.

```csharp
// Create a RecommendationsRequest with the pod id to request results for
RecommendationsRequest request = new RecommendationsRequest("pdp_complementary_items");

// Add in additional parameters
request.NumResults = 5;
request.ItemIds = new List<string> { "9838172" };

// Create a UserInfo object with the unique device identifier and session
UserInfo userInfo = new UserInfo("device-id-1123123", 5);
request.UserInfo = userInfo;

// Add a variations map to request specific variation attributes as an array or object (optional)
VariationsMap variationsMap = new VariationsMap();
variationMap.AddGroupByRule("url", "data.url");
variationMap.AddValueRule("variation_id", AggregationTypes.First, "data.variation_id");
variationMap.AddValueRule("deactivated", AggregationTypes.First, "data.deactivated");
request.VariationMap = variationsMap;

// Request results as an object
RecommendationsResponse response = constructor.recommendations(request, userInfo);
```

# Retrieving All Tasks

To retrieve all tasks, you will need to create a `AllTasksRequest`. In this request you can also specify the page and number of results per page. The page and number of results per page will default to 1 and 20 respectively.

```csharp
// Create a AllTasksRequest to request results for
AllTasksRequest request = new AllTasksRequest();

// Add in additional parameters
request.Page = 2;
request.ResultsPerPage = 10;

//Request all tasks as an object
AllTasksResponse response = constructor.Tasks.GetAllTasks(request);
```

# Retrieving Task with task_id

To retrieve a specific task with a task_id, you will need to create a `TaskRequest`.

```csharp
// Create a TaskRequest with the task_id to retrieve
TaskRequest request = new TaskRequest("12345");

//Request task as an object
Task response = constructor.Task.GetTask(request);
```

# Development
## Using VS Code
- Download ".NET Core Test Explorer" Extension
- In settings => .NET Core Test Explorer => Test Project Path: Constructorio_NET/Constructorio_NET.Tests

## For code coverage:
- if initial setup:
  - dotnet husky install
- dotnet husky run -g coverage

## Documentation
- Documentation generated by [Doxygen](https://doxygen.nl/download.html) for Mac OS X 10.14 and later