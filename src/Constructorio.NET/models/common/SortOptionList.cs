using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Constructorio_NET.Models
{
    /// <summary>
    /// Constructor.io List of Sort Options
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SortOptionList
    {
        /// <summary>
        /// List of Sort Options
        /// </summary>
        [JsonProperty("sort_options")]
        public List<SortOption> SortOptions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortOptionList"/> class.
        /// </summary>
        /// <param name="sortOptions">List of <see cref="SortOption"/></param>
        public SortOptionList(List<SortOption> sortOptions)
        {
            this.SortOptions = sortOptions;
        }
    }
}