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

    public static byte[] ReadToEnd(Stream stream)
    {
      long originalPosition = 0;

      if (stream.CanSeek)
      {
        originalPosition = stream.Position;
        stream.Position = 0;
      }

      try
      {
        byte[] readBuffer = new byte[4096];

        int totalBytesRead = 0;
        int bytesRead;

        while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
        {
          totalBytesRead += bytesRead;

          if (totalBytesRead == readBuffer.Length)
          {
            int nextByte = stream.ReadByte();
            if (nextByte != -1)
            {
              byte[] temp = new byte[readBuffer.Length * 2];
              Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
              Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
              readBuffer = temp;
              totalBytesRead++;
            }
          }
        }

        byte[] buffer = readBuffer;
        if (readBuffer.Length != totalBytesRead)
        {
          buffer = new byte[totalBytesRead];
          Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
        }
        return buffer;
      }
      finally
      {
        if (stream.CanSeek)
        {
          stream.Position = originalPosition;
        }
      }
    }

    public string CreateCatalogUrl(CatalogRequest req)
    {
      // Hashtable queryParams = req.GetUrlParameters();
      List<string> paths = new List<string> { "v1", "catalog" };
      // Stream stream = File.OpenRead(url);
      // byte[] buffer = ReadToEnd(stream);
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

    public void UpdateCatalog(CatalogRequest catalogRequest)
    {
      string url;
      Task<string> task;
      Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

      try
      {
        url = CreateCatalogUrl(catalogRequest);
        requestHeaders = catalogRequest.GetRequestHeaders();
        string encodedToken = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{this.Options["apiToken"]}:"));
        requestHeaders.Add("Authorization", $"Basic {encodedToken}");
        task = Helpers.MakeHttpRequest(new HttpMethod("PATCH"), url, requestHeaders, null, catalogRequest.Files);
      }
      catch (Exception e)
      {
        throw new ConstructorException(e);
      }

      if (task.Result != null)
      {
        Console.WriteLine(task.Result);
        // return JsonConvert.DeserializeObject<SearchResponse>(task.Result);
      }

      throw new ConstructorException("UpdateCatalog response data is malformed");
      // catalogRequest.
      // File.Create("url");
      // Helpers.MakeHttpRequest();
    }
  }
}