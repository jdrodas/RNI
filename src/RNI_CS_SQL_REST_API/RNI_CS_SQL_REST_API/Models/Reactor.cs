using System.Text.Json.Serialization;

namespace RNI_CS_SQL_REST_API.Models
{
    public class Reactor
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; } = string.Empty;

        [JsonPropertyName("ubicacion_pais")]
        public string? UbicacionPais { get; set; } = string.Empty;

        [JsonPropertyName("ubicacion_ciudad")]
        public string? UbicacionCiudad { get; set; } = string.Empty;

        [JsonPropertyName("tipo_reactor")]
        public string? TipoReactor { get; set; } = string.Empty;

        [JsonPropertyName("estado_reactor")]
        public string? EstadoReactor { get; set; } = string.Empty;

        [JsonPropertyName("potencia_termica")]
        public float PotenciaTermica { get; set; } = 0f;

        [JsonPropertyName("fecha_primera_reaccion")]
        public DateTime FechaPrimeraReaccion { get; set; } = new DateTime();
    }
}