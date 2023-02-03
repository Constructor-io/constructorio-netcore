using System.Collections;
using Constructorio_NET.Utils;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io PaginationOptions Class
    /// </summary>
    public class PaginationOptions
    {
        public int? Page { get; set; }
        public int? Offset { get; set; }
        public int? NumResultsPerPage { get; set; }

        public Hashtable GetQueryParameters()
        {
            Hashtable queryParams = new Hashtable();
            if (this.Page != null)
            {
                queryParams.Add(Constants.PAGE, this.Page);
            }

            if (this.Offset != null)
            {
                queryParams.Add(Constants.OFFSET, this.Offset);
            }

            if (this.NumResultsPerPage != null)
            {
                queryParams.Add(Constants.RESULTS_PER_PAGE, this.NumResultsPerPage);
            }

            return queryParams;
        }
    }
}