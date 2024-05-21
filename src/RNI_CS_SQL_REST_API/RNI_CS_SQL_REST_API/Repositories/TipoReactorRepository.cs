using Dapper;
using RNI_CS_SQL_REST_API.DBContexts;
using RNI_CS_SQL_REST_API.Interfaces;
using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Repositories
{
    public class TipoReactorRepository(PgsqlDbContext unContexto) : ITipoReactorRepository
    {
        private readonly PgsqlDbContext contextoDB = unContexto;

        public async Task<List<TipoReactor>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL = "SELECT tr.id, tr.nombre " +
                "FROM core.tipos_reactores tr " +
                "ORDER BY tr.nombre";

            var resultadoTipoReactores = await conexion
                .QueryAsync<TipoReactor>(sentenciaSQL, new DynamicParameters());

            return resultadoTipoReactores.ToList();
        }
    }
}
