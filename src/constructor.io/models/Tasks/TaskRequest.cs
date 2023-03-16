using System;
using System.Collections;
using System.Collections.Generic;

namespace Constructorio_NET.Models.Tasks
{
    /// <summary>
    /// Constructor.io Task Request Class.
    /// </summary>
    public class TaskRequest
    {
        public int TaskId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRequest"/> class.
        /// </summary>
        /// <param name="taskId">Task id to use for the request.</param>
        public TaskRequest(int taskId)
        {
            this.TaskId = taskId;
        }

        /// <summary>
        /// Get request parameters.
        /// </summary>
        /// <returns>Hashtable of request parameters.</returns>
        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();
            return parameters;
        }

        /// <summary>
        /// Get request headers.
        /// </summary>
        /// <returns>Dictionary of request headers.</returns>
        public Dictionary<string, string> GetRequestHeaders()
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            return requestHeaders;
        }
    }
}
