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
      Dictionary<string, bool> omittedQueryParams = new Dictionary<string, bool>()
      {
        { "_dt", true },
        { "c", true },
      };
      string url = Helpers.MakeUrl(this.Options, paths, queryParams, omittedQueryParams);
      return url;
    }

    public CatalogResponse ReplaceCatalog(CatalogRequest catalogRequest)
    {
      string url;
      Task<string> task;
      Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

      try
      {
        url = CreateCatalogUrl(catalogRequest);
        requestHeaders = catalogRequest.GetRequestHeaders();
        Helpers.AddAuthHeaders(this.Options, requestHeaders);
        task = Helpers.MakeHttpRequest(new HttpMethod("PUT"), url, requestHeaders, null, catalogRequest.Files);
      }
      catch (Exception e)
      {
        throw new ConstructorException(e);
      }

      if (task.Result != null)
      {
        return JsonConvert.DeserializeObject<CatalogResponse>(task.Result);
      }

      throw new ConstructorException("ReplaceCatalog response data is malformed");
    }

    public CatalogResponse UpdateCatalog(CatalogRequest catalogRequest)
    {
      string url;
      Task<string> task;
      Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

      try
      {
        url = CreateCatalogUrl(catalogRequest);
        requestHeaders = catalogRequest.GetRequestHeaders();
        Helpers.AddAuthHeaders(this.Options, requestHeaders);
        task = Helpers.MakeHttpRequest(new HttpMethod("PATCH"), url, requestHeaders, null, catalogRequest.Files);
      }
      catch (Exception e)
      {
        throw new ConstructorException(e);
      }

      if (task.Result != null)
      {
        return JsonConvert.DeserializeObject<CatalogResponse>(task.Result);
      }

      throw new ConstructorException("UpdateCatalog response data is malformed");
    }

    public CatalogResponse PatchCatalog(CatalogRequest catalogRequest)
    {
      string url;
      Task<string> task;
      Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

      try
      {
        url = CreateCatalogUrl(catalogRequest);
        url += "&patch_delta=true";
        requestHeaders = catalogRequest.GetRequestHeaders();
        Helpers.AddAuthHeaders(this.Options, requestHeaders);
        task = Helpers.MakeHttpRequest(new HttpMethod("PATCH"), url, requestHeaders, null, catalogRequest.Files);
      }
      catch (Exception e)
      {
        throw new ConstructorException(e);
      }

      if (task.Result != null)
      {
        return JsonConvert.DeserializeObject<CatalogResponse>(task.Result);
      }

      throw new ConstructorException("PatchCatalog response data is malformed");
    }
  }
}