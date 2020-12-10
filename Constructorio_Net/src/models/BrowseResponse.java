using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Browse Response
 */
namespace Constructorio_NET
{
    public class BrowseResponse {

    [JsonPropertyName("result_id")]
    private String resultId;

    [JsonPropertyName("response")]
    private BrowseResponseInner response;

    [JsonPropertyName("request")]
    private Map<String, Object> request;

    /**
     * @return the resultId
     */
    public String getResultId() {
      return resultId;
    }

    /**
     * @return the response
     */
    public BrowseResponseInner getResponse() {
      return response;
    }

    /**
     * @return the request as understood by the server
     */
    public Map<String, Object> getRequest() {
      return request;
    }
}