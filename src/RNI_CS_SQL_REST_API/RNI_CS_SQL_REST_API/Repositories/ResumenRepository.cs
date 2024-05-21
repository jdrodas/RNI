using Dapper;
using RNI_CS_SQL_REST_API.DBContexts;
using RNI_CS_SQL_REST_API.Interfaces;
using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Repositories
{
    public class ResumenRepository(PgsqlDbContext unContexto) : IResumenRepository
    {
        private readonly PgsqlDbContext contextoDB = unContexto;

        public async Task<Resumen> GetAllAsync()
        {
            Resumen unResumen = new();

            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL = "SELECT COUNT(id) total FROM core.reactores";
            unResumen.Reactores = await conexion
                .QueryFirstAsync<int>(sentenciaSQL, new DynamicParameters());

            sentenciaSQL = "SELECT COUNT(id) total FROM core.tipos_reactores";
            unResumen.TiposReactores = await conexion
                .QueryFirstAsync<int>(sentenciaSQL, new DynamicParameters());

            sentenciaSQL = "SELECT COUNT(id) total FROM core.estados_reactores";
            unResumen.EstadosReactores = await conexion
                .QueryFirstAsync<int>(sentenciaSQL, new DynamicParameters());

            sentenciaSQL = "SELECT COUNT(id) total FROM core.ubicaciones";
            unResumen.Ubicaciones = await conexion
                .QueryFirstAsync<int>(sentenciaSQL, new DynamicParameters());

            return unResumen;
        }
    }
}