using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
using Newtonsoft.Json;

namespace Constructorio_NET
{
    public class Tasks : Helpers
    {
        private Hashtable Options;
        public Tasks(Hashtable options)
        {
            this.Options = options;
        }

        public string CreateAllTasksUrl(AllTasksRequest req)
        {
            List<string> paths = new List<string> { "v1", "tasks" };
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

        public AllTasksResponse GetAllTasks(AllTasksRequest allTasksRequest)
        {
            string url;
            Task<string> task;
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            try
            {
                url = CreateAllTasksUrl(allTasksRequest);
                requestHeaders = allTasksRequest.GetRequestHeaders();
                Helpers.AddAuthHeaders(this.Options, requestHeaders);
                task = Helpers.MakeHttpRequest(new HttpMethod("GET"), url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (task.Result != null)
            {
                return JsonConvert.DeserializeObject<AllTasksResponse>(task.Result);
            }

            throw new ConstructorException("All Tasks response data is malformed");
        }

        public string CreateTaskUrl(TaskRequest req)
        {
            List<string> paths = new List<string> { "v1", "tasks", $"{req.TaskId}" };
            Hashtable queryParams = req.GetUrlParameters();
            Dictionary<string, string> requestHeaders = req.GetRequestHeaders();
            //      Dictionary<string, bool> omittedQueryParams = new Dictionary<string, bool>()
            //{
            //  { "_dt", true },
            //  { "c", true },
            //};
            string url = Helpers.MakeUrl(this.Options, paths, queryParams);
            return url;
        }

        public TaskResponse GetTask(TaskRequest taskRequest)
        {
            string url;
            Task<string> task;
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

            try
            {
                url = CreateTaskUrl(taskRequest);
                requestHeaders = taskRequest.GetRequestHeaders();
                Helpers.AddAuthHeaders(this.Options, requestHeaders);
                task = Helpers.MakeHttpRequest(new HttpMethod("GET"), url, requestHeaders);
            }
            catch (Exception e)
            {
                throw new ConstructorException(e);
            }

            if (task.Result != null)
            {
                return JsonConvert.DeserializeObject<TaskResponse>(task.Result);
            }

            throw new ConstructorException("Task response data is malformed");
        }
    }
}