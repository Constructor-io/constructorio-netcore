using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Constructorio_NET.Models
{
    /**
     * Constructor.io Task Request
     */
    public class TaskRequest
    {
        public int TaskId { get; set; }

        /// <summary>
        /// Creates a task request
        /// </summary>
        /// <param name="TaskId"></param>
        public TaskRequest(int TaskId)
        {
            this.TaskId = TaskId;
        }

        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();
            return parameters;
        }

        public Dictionary<string, string> GetRequestHeaders()
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            return requestHeaders;
        }
    }
}
