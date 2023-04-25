namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io Retrieve Sort Options by sort_by Request Class.
    /// </summary>
    public class RetrieveSortOptionBySortByRequest : SortOptionsRequest
    {
        /// <summary>
        /// Specify a sort_by property to retrieve a specific Sort Option.
        /// </summary>
        public string FilterBySortBy { get; set; }

        public RetrieveSortOptionBySortByRequest(string filterBySortBy, string section = "Products") : base(section)
        {
            this.FilterBySortBy = filterBySortBy;
        }
    }
}
