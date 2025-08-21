using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;

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
            List<string> paths = new List<string>(capacity: 2) { "v1", "tasks" };
            Hashtable queryParams = req.GetRequestParameters();
            Dictionary<string, bool> omittedQueryParams = new Dictionary<string, bool>(capacity: 2)
            {
                { "_dt", true },
                { "c", true },
            };
            string url = MakeUrl(this.Options, paths, queryParams, omittedQueryParams);
            return url;
        }

        public async Task<AllTasksResponse> GetAllTasks(AllTasksRequest allTasksRequest)
        {
            string url;
            AllTasksResponse result;

            url = CreateAllTasksUrl(allTasksRequest);
            Dictionary<string, string> requestHeaders = allTasksRequest.GetRequestHeaders();
            AddAuthHeaders(this.Options, requestHeaders);
            result = await MakeHttpRequest<AllTasksResponse>(Options, HttpMethod.Get, url, requestHeaders);

            return result ?? throw new ConstructorException("All Tasks response data is malformed");
        }

        public string CreateTaskUrl(TaskRequest req)
        {
            List<string> paths = new List<string>(capacity: 3) { "v1", "tasks", $"{req.TaskId}" };
            Hashtable queryParams = req.GetRequestParameters();
            Dictionary<string, bool> omittedQueryParams = new Dictionary<string, bool>(capacity: 1)
            {
                { "c", true },
            };
            string url = MakeUrl(this.Options, paths, queryParams, omittedQueryParams);

            return url;
        }

        public async Task<TaskResponse> GetTask(TaskRequest taskRequest)
        {
            string url;
            TaskResponse result;

            url = CreateTaskUrl(taskRequest);
            Dictionary<string, string> requestHeaders = taskRequest.GetRequestHeaders();
            AddAuthHeaders(this.Options, requestHeaders);
            result = await MakeHttpRequest<TaskResponse>(Options, HttpMethod.Get, url, requestHeaders);

            return result ?? throw new ConstructorException("Task response data is malformed");
        }
    }
}