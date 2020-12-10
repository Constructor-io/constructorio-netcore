using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/**
 * Constructor.io Server Error
 **/
namespace Constructorio_NET
{
    public class ServerError {

        [JsonPropertyName("message")]
        public String message { get; set; }

    }
}