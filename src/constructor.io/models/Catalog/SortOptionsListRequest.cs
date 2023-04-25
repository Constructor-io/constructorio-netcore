using System.Collections.Generic;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Sort Options Request Class for Multiple Sort Options.
    /// </summary>
    public class SortOptionsListRequest : SortOptionsRequest
    {
        /// <summary>
        /// Gets or sets a list of SortOptions.
        /// </summary>
        public List<SortOption> SortOptions { get; set; }

        /// <summary>
        /// Returns the formatted SortOptionList object for the request.
        /// </summary>
        public SortOptionList GetSortOptionsList()
        {
            return new SortOptionList(this.SortOptions);
        }

        /// <summary>
        /// Returns the SortOptionList object to deleting Sort Options for the request.
        /// </summary>
        public SortOptionList GetSortOptionListForDeletion()
        {
            List<SortOption> sortOptionsIds = new List<SortOption>();
            foreach (SortOption sortOption in this.SortOptions)
            {
                sortOptionsIds.Add(new SortOption(sortOption.SortBy, sortOption.SortOrder));
            }

            return new SortOptionList(sortOptionsIds);
        }

        public SortOptionsListRequest(List<SortOption> sortOptions, string section = "Products") : base(section)
        {
            this.SortOptions = sortOptions;
        }
    }
}
