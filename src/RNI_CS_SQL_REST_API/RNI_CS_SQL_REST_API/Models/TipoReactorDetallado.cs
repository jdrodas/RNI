using System.Text.Json.Serialization;

namespace RNI_CS_SQL_REST_API.Models
{
    public class TipoReactorDetallado : TipoReactor
    {
        [JsonPropertyName("reactores")]
        public List<Reactor>? Reactores { get; set; } = null;
    }
}