using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using Newtonsoft.Json;

namespace Constructorio_NET.Modules
{
    public class Tasks : Helpers
    {
        private readonly Hashtable Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasks"/> class.
        /// Interface for tasks related API calls.
        /// </summary>
        /// <param name="options">Hashtable of options from Constructorio instantiation.</param>
        public Tasks(Hashtable options)
        {
            this.Options = options;
        }

        public string CreateAllTasksUrl(AllTasksRequest req)
        {
            List<string> paths = new List<string> { "v1", "tasks" };
            Hashtable queryParams = req.GetRequestParameters();
            string url = MakeUrl(this.Options, paths, queryParams, OmitDtAndCQueryParams);
            return url;
        }

        public async Task<AllTasksResponse> GetAllTasks(AllTasksRequest allTasksRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = CreateAllTasksUrl(allTasksRequest);
                Dictionary<string, string> requestHeaders = allTasksRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                var result = await MakeHttpRequest(this.Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);

                if (result != null)
                {
                    return JsonConvert.DeserializeObject<AllTasksResponse>(result);
                }

                throw new ConstructorException("All Tasks response data is malformed");
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
        }

        public string CreateTaskUrl(TaskRequest req)
        {
            List<string> paths = new List<string> { "v1", "tasks", $"{req.TaskId}" };
            Hashtable queryParams = req.GetRequestParameters();
            string url = MakeUrl(this.Options, paths, queryParams, OmitCQueryParam);

            return url;
        }

        public async Task<TaskResponse> GetTask(TaskRequest taskRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = CreateTaskUrl(taskRequest);
                Dictionary<string, string> requestHeaders = taskRequest.GetRequestHeaders();
                AddAuthHeaders(this.Options, requestHeaders);
                var result = await MakeHttpRequest(this.Options, HttpMethod.Get, url, requestHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);

                if (result != null)
                {
                    return JsonConvert.DeserializeObject<TaskResponse>(result);
                }

                throw new ConstructorException("Task response data is malformed");
            }
            catch (OperationCanceledException)
            {
                // Bubble this up to the caller to determine how to handle canceled operations
                throw;
            }
        }
    }
}
