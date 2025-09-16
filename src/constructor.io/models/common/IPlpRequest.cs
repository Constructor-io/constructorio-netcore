using System.Collections.Generic;

namespace Constructorio_NET.Models
{
    public interface IPlpRequest : IFilterable, IPageable, ISortable, IUserDetails
    {
        /// <summary>
        /// Gets or sets the format options used to refine result groups.
        /// </summary>
        Dictionary<string, string> FmtOptions { get; set; }

        /// <summary>
        /// Gets or sets hidden facets fields to return.
        /// </summary>
        List<string> HiddenFacets { get; set; }

        /// <summary>
        /// Gets or sets hidden metadata fields to return.
        /// </summary>
        List<string> HiddenFields { get; set; }

        /// <summary>
        /// Gets or sets the filtering expression used to scope search results.
        /// </summary>
        PreFilterExpression PreFilterExpression { get; set; }

        /// <summary>
        /// Gets or sets how to return variation data.
        /// </summary>
        VariationsMap VariationsMap { get; set; }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        string Section { get; set; }
    }
}
