using System.Text.Json.Serialization;

namespace CodingTestApi.Models
{
    public class Artist
    {
        [JsonPropertyName("artist_id")]
        public string Id { get; set; }

        [JsonPropertyName("artist_name")]
        public string Name { get; set; }
    }
}