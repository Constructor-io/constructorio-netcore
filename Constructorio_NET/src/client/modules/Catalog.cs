using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Constructorio_NET
{
  public class Catalog : Helpers
  {
    private Hashtable Options;
    public Catalog(Hashtable options)
    {
      this.Options = options;
    }

    public string CreateCatalogUrl(CatalogRequest req)
    {
      List<string> paths = new List<string> { "v1", "catalog" };
      Hashtable queryParams = req.GetUrlParameters();
      Dictionary<string, string> requestHeaders = req.GetRequestHeaders();
      Hashtable omittedQueryParams = new Hashtable()
      {
        { "dt", true },
        { "c", true },
      };
      string url = Helpers.MakeUrl(this.Options, paths, queryParams, omittedQueryParams);
      Task<string> task = Helpers.MakeHttpRequest(HttpMethod.Post, url, requestHeaders, null, req.Files);
      return url;
    }

    public void ReplaceCatalog(CatalogRequest catalogRequest)
    {
      string url;
      Task<string> task;
      Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

      try
      {
        url = CreateCatalogUrl(catalogRequest);
        requestHeaders = catalogRequest.GetRequestHeaders();
        Helpers.CreateAuthHeaders(this.Options, requestHeaders);
        task = Helpers.MakeHttpRequest(new HttpMethod("PUT"), url, requestHeaders, null, catalogRequest.Files);
      }
      catch (Exception e)
      {
        throw new ConstructorException(e);
      }

      if (task.Result != null)
      {
        // return JsonConvert.DeserializeObject<SearchResponse>(task.Result);
      }

      throw new ConstructorException("ReplaceCatalog response data is malformed");
    }

    public void UpdateCatalog(CatalogRequest catalogRequest)
    {
      string url;
      Task<string> task;
      Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

      try
      {
        url = CreateCatalogUrl(catalogRequest);
        requestHeaders = catalogRequest.GetRequestHeaders();
        Helpers.CreateAuthHeaders(this.Options, requestHeaders);
        task = Helpers.MakeHttpRequest(new HttpMethod("PATCH"), url, requestHeaders, null, catalogRequest.Files);
      }
      catch (Exception e)
      {
        throw new ConstructorException(e);
      }

      if (task.Result != null)
      {
        // return JsonConvert.DeserializeObject<SearchResponse>(task.Result);
      }

      throw new ConstructorException("UpdateCatalog response data is malformed");
    }

    public void PatchCatalog(CatalogRequest catalogRequest)
    {
      string url;
      Task<string> task;
      Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

      try
      {
        url = CreateCatalogUrl(catalogRequest);
        url += "&patch_delta=true";
        requestHeaders = catalogRequest.GetRequestHeaders();
        Helpers.CreateAuthHeaders(this.Options, requestHeaders);
        task = Helpers.MakeHttpRequest(new HttpMethod("PATCH"), url, requestHeaders, null, catalogRequest.Files);
      }
      catch (Exception e)
      {
        throw new ConstructorException(e);
      }

      if (task.Result != null)
      {
        // return JsonConvert.DeserializeObject<SearchResponse>(task.Result);
      }

      throw new ConstructorException("PatchCatalog response data is malformed");
    }
  }
}