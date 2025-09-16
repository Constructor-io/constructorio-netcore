namespace Constructorio_NET.Models
{
    public interface ISortable
    {
        /// <summary>
        /// Gets or sets the sort method for results.
        /// </summary>
        string SortBy { get; set; }

        /// <summary>
        /// Gets or sets the sort order for results.
        /// </summary>
        string SortOrder { get; set; }
    }
}
