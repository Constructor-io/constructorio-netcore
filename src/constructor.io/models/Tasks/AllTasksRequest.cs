using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io All Tasks Request Class.
    /// </summary>
    public class AllTasksRequest
    {
        public int ResultsPerPage { get; set; }

        public int Page { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string Type { get ; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllTasksRequest"/> class.
        /// </summary>
        public AllTasksRequest()
        {
            this.ResultsPerPage = 20;
            this.Page = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllTasksRequest"/> class.
        /// </summary>
        /// <param name="ResultsPerPage">Number of results per page to return.</param>
        /// <param name="Page">Page number of the results to return.</param>
        public AllTasksRequest(int ResultsPerPage, int Page)
        {
            this.ResultsPerPage = ResultsPerPage;
            this.Page = Page;
        }

        /// <summary>
        /// Get request parameters.
        /// </summary>
        /// <returns>Hashtable of request parameters.</returns>
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

            if (!string.IsNullOrEmpty(StartDate))
            {
                parameters.Add(Constants.START_DATE, this.StartDate);
            }

            if (!string.IsNullOrEmpty(EndDate))
            {
                parameters.Add(Constants.END_DATE, this.EndDate);
            }

            if (!string.IsNullOrEmpty(Status))
            {
                parameters.Add(Constants.STATUS, this.Status);
            }

            if (!string.IsNullOrEmpty(Type))
            {
                parameters.Add(Constants.TYPE, this.Type);
            }

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
