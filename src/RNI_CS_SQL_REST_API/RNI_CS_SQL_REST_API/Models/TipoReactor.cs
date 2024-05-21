using System.Text.Json.Serialization;

namespace RNI_CS_SQL_REST_API.Models
{
    public class TipoReactor
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; } = string.Empty;
    }
}
