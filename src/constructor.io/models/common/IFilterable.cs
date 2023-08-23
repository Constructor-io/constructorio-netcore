using System.Collections.Generic;

namespace Constructorio_NET.Models
{
    public interface IFilterable
    {
        /// <summary>
        /// Gets or sets filters used to refine results.
        /// </summary>
        Dictionary<string, List<string>> Filters { get; set; }

        /// <summary>
        /// Gets or sets user test cells.
        /// </summary>
        Dictionary<string, string> TestCells { get; set; }

        /// <summary>
        /// Gets or sets collection of user related data.
        /// </summary>
        UserInfo UserInfo { get; set; }

        /// <summary>
        /// Gets or sets how to return variation data.
        /// </summary>
        VariationsMap VariationsMap { get; set; }
    }
}
