using System;
using Newtonsoft.Json;

/**
 * Constructor.io Server Error
 */
namespace Constructorio_NET.Models
{
    public class ServerError
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}