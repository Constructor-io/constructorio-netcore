using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * Constructor.io Browse Facet Options Response Inner
 */
namespace Constructorio_NET.Models.Browse
{
    public class BrowseFacetOptionsResponseInner
    {
        [JsonProperty("facets")]
        public List<FilterFacet> Facets { get; set; }
    }
}
