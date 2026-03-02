using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Format options used to refine result groups and control response fields.
    /// </summary>
    public class FmtOptions
    {
        /// <summary>
        /// Gets or sets the maximum depth of result groups to return.
        /// </summary>
        public int? GroupsMaxDepth { get; set; }

        /// <summary>
        /// Gets or sets the starting point for groups.
        /// Valid values: "current", "top", or "group_id:{id}".
        /// </summary>
        public string? GroupsStart { get; set; }

        /// <summary>
        /// Gets or sets the list of fields to return in results.
        /// </summary>
        public List<string>? Fields { get; set; }

        /// <summary>
        /// Gets or sets hidden metadata fields to return.
        /// </summary>
        public List<string>? HiddenFields { get; set; }

        /// <summary>
        /// Gets or sets hidden facet fields to return.
        /// </summary>
        public List<string>? HiddenFacets { get; set; }

        /// <summary>
        /// Gets or sets whether to show hidden fields.
        /// </summary>
        public bool? ShowHiddenFields { get; set; }

        /// <summary>
        /// Gets or sets the variations return type.
        /// Valid values: "default", "all", "matched".
        /// </summary>
        public string? VariationsReturnType { get; set; }

        /// <summary>
        /// Gets or sets whether to show hidden facets.
        /// </summary>
        public bool? ShowHiddenFacets { get; set; }

        /// <summary>
        /// Gets or sets whether to show protected facets.
        /// </summary>
        public bool? ShowProtectedFacets { get; set; }

        /// <summary>
        /// Returns a Hashtable of pre-formatted query parameters with keys like "fmt_options[property_name]".
        /// Values are either string or List&lt;string&gt;.
        /// </summary>
        /// <returns>Hashtable of query parameters.</returns>
        public Hashtable GetQueryParameters()
        {
            Hashtable parameters = new Hashtable();

            if (this.GroupsMaxDepth.HasValue)
            {
                parameters.Add($"{Constants.FMT_OPTIONS}[{Constants.GROUPS_MAX_DEPTH}]", this.GroupsMaxDepth.Value.ToString());
            }

            if (!string.IsNullOrEmpty(this.GroupsStart))
            {
                parameters.Add($"{Constants.FMT_OPTIONS}[{Constants.GROUPS_START}]", this.GroupsStart);
            }

            if (this.ShowHiddenFields.HasValue)
            {
                parameters.Add($"{Constants.FMT_OPTIONS}[{Constants.SHOW_HIDDEN_FIELDS}]", this.ShowHiddenFields.Value.ToString().ToLower());
            }

            if (!string.IsNullOrEmpty(this.VariationsReturnType))
            {
                parameters.Add($"{Constants.FMT_OPTIONS}[{Constants.VARIATIONS_RETURN_TYPE}]", this.VariationsReturnType);
            }

            if (this.ShowHiddenFacets.HasValue)
            {
                parameters.Add($"{Constants.FMT_OPTIONS}[{Constants.SHOW_HIDDEN_FACETS}]", this.ShowHiddenFacets.Value.ToString().ToLower());
            }

            if (this.ShowProtectedFacets.HasValue)
            {
                parameters.Add($"{Constants.FMT_OPTIONS}[{Constants.SHOW_PROTECTED_FACETS}]", this.ShowProtectedFacets.Value.ToString().ToLower());
            }

            if (this.Fields != null)
            {
                parameters.Add($"{Constants.FMT_OPTIONS}[{Constants.FIELDS}]", this.Fields);
            }

            if (this.HiddenFields != null)
            {
                parameters.Add($"{Constants.FMT_OPTIONS}[{Constants.HIDDEN_FIELDS}]", this.HiddenFields);
            }

            if (this.HiddenFacets != null)
            {
                parameters.Add($"{Constants.FMT_OPTIONS}[{Constants.HIDDEN_FACETS}]", this.HiddenFacets);
            }

            return parameters;
        }
    }
}
