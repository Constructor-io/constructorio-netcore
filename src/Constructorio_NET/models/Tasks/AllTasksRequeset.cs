using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;
using Newtonsoft.Json;

namespace Constructorio_NET.Models
{
    /**
     * Constructor.io All Tasks Request
     */
    public class AllTasksRequest
    {
        public int ResultsPerPage { get; set; }

        public int Page { get; set; }

        /// <summary>
        /// Creates a all tasks request
        /// </summary>
        /// <param name="TaskId"></param>
        public AllTasksRequest()
        {
            this.ResultsPerPage = 20;
            this.Page = 1;
        }

        public AllTasksRequest(int ResultsPerPage, int Page)
        {
            this.ResultsPerPage = ResultsPerPage;
            this.Page = Page;
        }

        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();

            if (this.Page != 0)
            {
                parameters.Add(Constants.PAGE, this.Page);
            }

            if (this.ResultsPerPage != 0)
            {
                parameters.Add(Constants.RESULTS_PER_PAGE, this.ResultsPerPage);
            }

            return parameters;
        }

        public Dictionary<string, string> GetRequestHeaders()
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            return requestHeaders;
        }
    }
}
