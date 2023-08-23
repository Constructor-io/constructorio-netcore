namespace Constructorio_NET.Models
{
    public interface IPagable
    {
        /// <summary>
        /// Gets or sets the number of results to skip from the beginning.
        /// Can't be used together with <see cref="Page"/>.
        /// </summary>
        int Offset { get; set; }

        /// <summary>
        /// Gets or sets the page number of the results to return.
        /// Can't be used together with <see cref="Offset"/>.
        /// </summary>
        int Page { get; set; }

        /// <summary>
        /// Gets or sets the number of results per page to return.
        /// </summary>
        int ResultsPerPage { get; set; }

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