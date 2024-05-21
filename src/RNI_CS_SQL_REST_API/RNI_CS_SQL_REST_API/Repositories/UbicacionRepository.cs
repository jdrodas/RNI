using Dapper;
using RNI_CS_SQL_REST_API.DBContexts;
using RNI_CS_SQL_REST_API.Interfaces;
using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Repositories
{
    public class UbicacionRepository(PgsqlDbContext unContexto) : IUbicacionRepository
    {
        private readonly PgsqlDbContext contextoDB = unContexto;

        public async Task<List<Ubicacion>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL = "SELECT u.id, u.pais, u.ciudad " +
                "FROM core.ubicaciones u " +
                "ORDER BY u.pais, u.ciudad";

            var resultadoMunicipios = await conexion
                .QueryAsync<Ubicacion>(sentenciaSQL, new DynamicParameters());

            return resultadoMunicipios.ToList();
        }
    }
}
