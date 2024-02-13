using System.Collections.Generic;

namespace Constructorio_NET.Models
{
    public interface IFilterable
    {
        /// <summary>
        /// Gets or sets filters used to refine results.
        /// </summary>
        Dictionary<string, List<string>> Filters { get; set; }
    }
}
