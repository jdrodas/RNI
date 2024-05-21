using System.Text.Json.Serialization;

namespace RNI_CS_SQL_REST_API.Models
{
    public class Ubicacion
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("pais")]
        public string? Pais { get; set; } = string.Empty;

        [JsonPropertyName("ciudad")]
        public string? Ciudad { get; set; } = string.Empty;
    }
}