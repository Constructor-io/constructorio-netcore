using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Values that can be set per pod on a recommendation page request.
    /// Each value replaces (is not merged with) the page-wide value for that pod.
    /// </summary>
    public class RecommendationPagePodOverride
    {
        /// <summary>
        /// Gets or sets number of results to return.
        /// </summary>
        public int? NumResults { get; set; }

        /// <summary>
        /// Gets or sets filters used to refine results.
        /// </summary>
        public Dictionary<string, List<string>> Filters { get; set; }

        /// <summary>
        /// Gets or sets whether results must match "all", "any" or "none" of each filter's values, keyed by filter name.
        /// </summary>
        public Dictionary<string, string> FilterMatchTypes { get; set; }

        /// <summary>
        /// Gets or sets the filtering expression used to scope recommendation results.
        /// </summary>
        public PreFilterExpression PreFilterExpression { get; set; }

        /// <summary>
        /// Gets or sets how to return variation data.
        /// </summary>
        public VariationsMap VariationsMap { get; set; }

        /// <summary>
        /// Gets or sets format options to control result formatting.
        /// </summary>
        public FmtOptions FmtOptions { get; set; }

        /// <summary>
        /// Adds the overridable parameters to a parameter table, in the same wire format as
        /// <see cref="RecommendationsRequest.GetRequestParameters"/>, nested under a bracket prefix
        /// (empty for page-wide values).
        /// </summary>
        /// <param name="parameters">Hashtable to add parameters to.</param>
        /// <param name="prefix">Bracket prefix, such as "pod_overrides[similar_items]".</param>
        internal void AddOverridableParameters(Hashtable parameters, string prefix)
        {
            if (this.NumResults.HasValue)
            {
                parameters.Add(BracketName(prefix, "num_results"), this.NumResults.Value);
            }

            if (this.Filters != null)
            {
                foreach (var filter in this.Filters)
                {
                    parameters.Add($"{BracketName(prefix, "filters")}[{filter.Key}]", filter.Value);
                }
            }

            if (this.FilterMatchTypes != null)
            {
                foreach (var matchType in this.FilterMatchTypes)
                {
                    parameters.Add($"{BracketName(prefix, "filter_match_types")}[{matchType.Key}]", matchType.Value);
                }
            }

            if (this.VariationsMap != null && this.VariationsMap.Values.Count > 0)
            {
                parameters.Add(BracketName(prefix, "variations_map"), JsonConvert.SerializeObject(this.VariationsMap));
            }

            if (this.PreFilterExpression != null)
            {
                parameters.Add(BracketName(prefix, "pre_filter_expression"), this.PreFilterExpression.GetExpression());
            }

            if (this.FmtOptions != null)
            {
                foreach (DictionaryEntry entry in this.FmtOptions.GetQueryParameters())
                {
                    parameters.Add(NestName(prefix, (string)entry.Key), entry.Value);
                }
            }
        }

        private static string BracketName(string prefix, string name)
        {
            return string.IsNullOrEmpty(prefix) ? name : $"{prefix}[{name}]";
        }

        // Nests a parameter name under a prefix: "fmt_options[x]" becomes "{prefix}[fmt_options][x]"
        private static string NestName(string prefix, string name)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return name;
            }

            int bracketIndex = name.IndexOf('[');

            return bracketIndex == -1
                ? $"{prefix}[{name}]"
                : $"{prefix}[{name.Substring(0, bracketIndex)}]{name.Substring(bracketIndex)}";
        }
    }
}
