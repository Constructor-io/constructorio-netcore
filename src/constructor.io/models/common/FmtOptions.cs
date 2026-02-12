using System.Collections.Generic;

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
    }
}
