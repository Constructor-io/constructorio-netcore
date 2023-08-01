using Newtonsoft.Json;

/**
 * Constructor.io Result Sources
 **/
namespace Constructorio_NET.Models
{
    public class Match
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class ResultSources
    {
        [JsonProperty("token_match")]
        public Match TokenMatch { get; set; }

        [JsonProperty("embeddings_match")]
        public Match EmbeddingsMatch { get; set; }
    }
}
