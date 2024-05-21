using Dapper;
using RNI_CS_SQL_REST_API.DBContexts;
using RNI_CS_SQL_REST_API.Interfaces;
using RNI_CS_SQL_REST_API.Models;
using System.Data;

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

        public async Task<TipoReactor> GetByIdAsync(int tipo_reactor_id)
        {
            TipoReactor unTipoReactor = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@tipo_reactor_id", tipo_reactor_id,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT tr.id, tr.nombre " +
                "FROM core.tipos_reactores tr " +
                "WHERE tr.id = @tipo_reactor_id";

            var resultado = await conexion.QueryAsync<TipoReactor>(sentenciaSQL,
                parametrosSentencia);

            if (resultado.Any())
                unTipoReactor = resultado.First();

            return unTipoReactor;
        }
    }
}
