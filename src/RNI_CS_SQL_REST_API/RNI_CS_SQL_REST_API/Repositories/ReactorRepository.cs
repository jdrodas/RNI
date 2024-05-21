using Dapper;
using RNI_CS_SQL_REST_API.DBContexts;
using RNI_CS_SQL_REST_API.Interfaces;
using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Repositories
{
    public class ReactorRepository(PgsqlDbContext unContexto) : IReactorRepository
    {
        private readonly PgsqlDbContext contextoDB = unContexto;

        public async Task<List<Reactor>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL = "SELECT v.reactor_id id, v.reactor_nombre nombre, v.ubicacion_pais ubicacionPais, " +
                "v.ubicacion_ciudad ubicacionCiudad, v.reactor_tipo tipoReactor, v.reactor_estado estadoReactor, " +
                "v.potencia_termica potenciaTermica, v.fecha_primera_reaccion fechaPrimeraReaccion " +
                "FROM v_info_reactores v ";

            var resultadoReactores = await conexion
                .QueryAsync<Reactor>(sentenciaSQL, new DynamicParameters());

            return resultadoReactores.ToList();
        }
    }
}
