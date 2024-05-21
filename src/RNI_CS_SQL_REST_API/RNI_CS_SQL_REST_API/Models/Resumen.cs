using System.Text.Json.Serialization;

namespace RNI_CS_SQL_REST_API.Models
{
    public class Resumen
    {
        [JsonPropertyName("reactores")]
        public int Reactores { get; set; } = 0;

        [JsonPropertyName("tipos_reactores")]
        public int TiposReactores { get; set; } = 0;

        [JsonPropertyName("estados_reactores")]
        public int EstadosReactores { get; set; } = 0;

        [JsonPropertyName("ubicaciones")]
        public int Ubicaciones { get; set; } = 0;
    }
}