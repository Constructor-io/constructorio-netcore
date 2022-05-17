using System;
using Newtonsoft.Json;

/**
 * Constructor.io Server Error
 */
namespace Constructorio_NET
{
    public class ServerError {

        [JsonProperty("message")]
        public String Message { get; set; }

    }
}