# Constructor.io Netcore Client

[Constructor.io](http://constructor.io/) provides search as a service that optimizes results using artificial intelligence (including natural language processing, re-ranking to optimize for conversions, and user personalization).

# Documentation

Full API documentation is available on [Github Pages](https://constructor-io.github.io/constructorio-netcore/)

# Requirements

Requesting results from your .NET based back-end can be useful in order to control result rendering logic on your server, or augment/hydrate results with data from another system. However, a back-end integration has additional requirements compared to a front-end integration. Please review the [Additional Information For Backend Integrations](https://github.com/Constructor-io/constructorio-netcore/wiki/Additional-Information-for-Backend-Integrations) article within the wiki for more detail.

# Installation

1. Follow the directions at [Nuget](https://www.nuget.org/packages/constructor.io/) to add the package to your project.
2. Retrieve your API token and key.  You can find this at your [Constructor.io dashboard](https://constructor.io/dashboard).
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
CatalogResponse response = await constructorio.Catalog.ReplaceCatalog(request);
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
variationsMap.AddGroupByRule("url", "data.url");
variationsMap.AddValueRule("variation_id", AggregationTypes.First, "data.variation_id");
variationsMap.AddValueRule("deactivated", AggregationTypes.First, "data.deactivated");
request.VariationsMap = variationsMap;

// Request results as an object
AutocompleteResponse response = await constructorio.Autocomplete.GetAutocompleteResults(request);
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

// Add the following paramaters to request for hidden fields
request.HiddenFields = new List<string>
{
    "hidden_price_field",
    "hidden_brand_field",
};

// Add the following paramaters to request for hidden facets
request.HiddenFacets = new List<string>
{
    "hidden_price_facet",
    "hidden_brand_facet",
};

// Create a UserInfo object with the unique device identifier and session
UserInfo userInfo = new UserInfo("device-id-1123123", 5);
request.UserInfo = userInfo;

// Add a variations map to request specific variation attributes as an array or object (optional)
VariationsMap variationsMap = new VariationsMap();
variationsMap.AddGroupByRule("url", "data.url");
variationsMap.AddValueRule("variation_id", AggregationTypes.First, "data.variation_id");
variationsMap.AddValueRule("deactivated", AggregationTypes.First, "data.deactivated");
request.VariationsMap = variationsMap;

// Faceting expression to scope results, it is applied before other filters and doesn't affect facet counts
string preFilterExpressionJObject = @"{
    or: [
        {
        and:
            [
            { name: 'group_id', value: 'BrandXY' },
            { name: 'Color', value: 'red' },
        ],
        },
        {
        and:
            [
            { name: 'Color', value: 'blue' },
            { name: 'Brand', value: 'XYZ' },
        ],
        },
    ],
}";
JsonPrefilterExpression preFilterExpression = new JsonPrefilterExpression(preFilterExpressionJObject);
request.PreFilterExpression = preFilterExpression;

// Request results as an object
SearchResponse response = await constructorio.Search.GetSearchResults(request);
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

// Add the following paramaters to request for hidden fields
request.HiddenFields = new List<string>
{
    "hidden_price_field",
    "hidden_brand_field",
};

// Add the following paramaters to request for hidden facets
request.HiddenFacets = new List<string>
{
    "hidden_price_facet",
    "hidden_brand_facet",
};

// Create a UserInfo object with the unique device identifier and session
UserInfo userInfo = new UserInfo("device-id-1123123", 5);
request.UserInfo = userInfo;

// Add a variations map to request specific variation attributes as an array or object (optional)
VariationsMap variationsMap = new VariationsMap();
variationsMap.AddGroupByRule("url", "data.url");
variationsMap.AddValueRule("variation_id", AggregationTypes.First, "data.variation_id");
variationsMap.AddValueRule("deactivated", AggregationTypes.First, "data.deactivated");
request.VariationsMap = variationsMap;

// Faceting expression to scope results, it is applied before other filters and doesn't affect facet counts
string preFilterExpressionJObject = @"{
    or: [
        {
        and:
            [
            { name: 'group_id', value: 'BrandXY' },
            { name: 'Color', value: 'red' },
        ],
        },
        {
        and:
            [
            { name: 'Color', value: 'blue' },
            { name: 'Brand', value: 'XYZ' },
        ],
        },
    ],
}";
JsonPrefilterExpression preFilterExpression = new JsonPrefilterExpression(preFilterExpressionJObject);
request.PreFilterExpression = preFilterExpression;

// Request results as an object
BrowseResponse response = await constructorio.Browse.GetBrowseResults(request);
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

// Add the following paramaters to request for hidden fields
request.HiddenFields = new List<string>
{
    "hidden_price_field",
    "hidden_brand_field",
};

// Add the following paramaters to request for hidden facets
request.HiddenFacets = new List<string>
{
    "hidden_price_facet",
    "hidden_brand_facet",
};

// Create a UserInfo object with the unique device identifier and session
UserInfo userInfo = new UserInfo("device-id-1123123", 5);
request.UserInfo = userInfo;

// Request results as an object
BrowseResponse response = await constructorio.Browse.GetBrowseItemsResults(request);
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
variationsMap.AddGroupByRule("url", "data.url");
variationsMap.AddValueRule("variation_id", AggregationTypes.First, "data.variation_id");
variationsMap.AddValueRule("deactivated", AggregationTypes.First, "data.deactivated");
request.VariationsMap = variationsMap;

// Request results as an object
RecommendationsResponse response = await constructorio.Recommendations.GetRecommendationsResults(request);
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
AllTasksResponse response = await constructorio.Tasks.GetAllTasks(request);
```

# Retrieving Task with Task ID

To retrieve a specific task with a task_id, you will need to create a `TaskRequest`.

```csharp
// Create a TaskRequest with the task_id to retrieve
TaskRequest request = new TaskRequest("12345");

//Request task as an object
Task response = await constructorio.Tasks.GetTask(request);
```

# Development
## Using VS Code
- Download ".NET Core Test Explorer" Extension
- In settings => .NET Core Test Explorer => Test Project Path: Constructorio_NET/Constructorio_NET.Tests

## Building
The project includes a build script that handles restore, build, and packaging:

```bash
# Build in Release mode (default)
./build.sh

# Build in Debug mode
./build.sh Debug

# Build with tests
./build.sh --test

# Build with clean
./build.sh --clean

# Combine options
./build.sh Release --test --clean
```

The build script will create a NuGet package in the `./artifacts` directory.

## Testing
- Make sure you have .NET SDK V8 installed
- Open your terminal and navigate to the test directory:
  ```bash
  cd src/Constructorio_NET.Tests/
  ```
- Run the following command to execute tests:
  ```bash
  dotnet test
  ```

## For code coverage:
- if initial setup:
  - dotnet husky install
- dotnet husky run -g coverage

## Publishing
The project includes a publish script to push packages to NuGet:

```bash
# Set your NuGet API key
export NUGET_API_KEY="your-api-key"

# Dry run to verify package details
./publish.sh --dry-run

# Publish to NuGet (requires confirmation)
./publish.sh
```

The publish script will:
1. Find the package in `./artifacts`
2. Show package details and prompt for confirmation
3. Push to NuGet.org
4. Clean up the artifacts folder on success

## Documentation
- Documentation generated by [Doxygen](https://doxygen.nl/download.html) for Mac OS X 10.14 and later
