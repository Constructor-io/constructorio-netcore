using System.Collections.Generic;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Sort Options Request Class.
    /// </summary>
    public class SortOptionsSingleRequest : SortOptionsRequest
    {
        /// <summary>
        /// Gets, sets or updates a specific Sort Option.
        /// </summary>
        public SortOption SortOption { get; set; }

        /// <summary>
        /// Returns the <see cref="SortOptionForModifyRequest" /> object for requests that updates a single Sort Option.
        /// </summary>
        public SortOptionForModifyRequest GetSortOptionDelta()
        {
            return new SortOptionForModifyRequest
            {
                DisplayName = this.SortOption.DisplayName,
                PathInMetadata = this.SortOption.PathInMetadata,
                Position = this.SortOption.Position
            };
        }

        public SortOptionsSingleRequest(SortOption sortOption) : base()
        {
            this.SortOption = sortOption;
        }
    }
}
