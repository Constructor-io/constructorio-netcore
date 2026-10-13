using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;
using Newtonsoft.Json;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Recommendations Request Class.
    /// </summary>
    public class RecommendationsRequest
    {
        /// <summary>
        /// Gets or sets pod id.
        /// </summary>
        public string PodId { get; set; }

        /// <summary>
        /// Gets or sets number of results to return.
        /// </summary>
        public int NumResults { get; set; }

        /// <summary>
        /// Gets or sets item id.
        /// </summary>
        public List<string> ItemIds { get; set; }

        /// <summary>
        /// Gets or sets variation ids.
        /// </summary>
        public List<string> VariationIds { get; set; }

        /// <summary>
        /// Gets or sets term.
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// Gets or sets filters used to refine results.
        /// </summary>
        public Dictionary<string, List<string>> Filters { get; set; }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Gets or sets user test cells.
        /// </summary>
        public Dictionary<string, string> TestCells { get; set; }

        /// <summary>
        /// Gets or sets collection of user related data.
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationsRequest"/> class.
        /// </summary>
        /// <param name="podId">Pod id to use for the request.</param>
        public RecommendationsRequest(string podId)
        {
            if (podId == null)
            {
                throw new ArgumentException("PodId is required");
            }

            this.PodId = podId;
        }

        /// <summary>
        /// Gets or sets how to return variation data.
        /// </summary>
        public VariationsMap VariationsMap { get; set; }

        /// <summary>
        /// Gets or sets the filtering expression used to scope recommendation results.
        /// </summary>
        public PreFilterExpression PreFilterExpression { get; set; }

        /// <summary>
        /// Gets or sets format options to control result formatting.
        /// </summary>
        public FmtOptions FmtOptions { get; set; }

        /// <summary>
        /// Get request parameters.
        /// </summary>
        /// <returns>Hashtable of request parameters.</returns>
        public Hashtable GetRequestParameters()
        {
            Hashtable parameters = new Hashtable();

            RecommendationsRequestParameters.AddSharedParameters(parameters, this.UserInfo, this.Section, this.TestCells, this.ItemIds, this.VariationIds, this.Term);

            if (this.NumResults != 0)
            {
                parameters.Add("num_results", this.NumResults);
            }

            if (this.Filters != null)
            {
                parameters.Add(Constants.FILTERS, this.Filters);
            }

            if (this.VariationsMap != null && this.VariationsMap.Values.Count > 0)
            {
                string serializedJson = JsonConvert.SerializeObject(this.VariationsMap);
                parameters.Add(Constants.VARIATIONS_MAP, serializedJson);
            }

            if (this.PreFilterExpression != null)
            {
                string preFilterJson = this.PreFilterExpression.GetExpression();
                parameters.Add(Constants.PRE_FILTER_EXPRESSION, preFilterJson);
            }

            if (this.FmtOptions != null)
            {
                foreach (DictionaryEntry entry in this.FmtOptions.GetQueryParameters())
                {
                    parameters.Add(entry.Key, entry.Value);
                }
            }

            return parameters;
        }

        /// <summary>
        /// Get request headers.
        /// </summary>
        /// <returns>Dictionary of request headers.</returns>
        public Dictionary<string, string> GetRequestHeaders()
        {
            return RecommendationsRequestParameters.GetRequestHeaders(this.UserInfo);
        }
    }
}
