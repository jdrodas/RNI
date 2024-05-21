using Dapper;
using RNI_CS_SQL_REST_API.DBContexts;
using RNI_CS_SQL_REST_API.Interfaces;
using RNI_CS_SQL_REST_API.Models;
using System.Data;

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

        public async Task<List<Reactor>> GetByReactorTypeIdAsync(int tipo_reactor_id)
        {
            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@tipo_reactor_id", tipo_reactor_id,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT v.reactor_id id, v.reactor_nombre nombre, v.ubicacion_pais ubicacionPais, " +
                "v.ubicacion_ciudad ubicacionCiudad, v.reactor_tipo tipoReactor, v.reactor_estado estadoReactor, " +
                "v.potencia_termica potenciaTermica, v.fecha_primera_reaccion fechaPrimeraReaccion " +
                "FROM v_info_reactores v " +
                "WHERE v.tipo_id = @tipo_reactor_id";

            var resultadoReactores = await conexion
                .QueryAsync<Reactor>(sentenciaSQL, parametrosSentencia);

            return resultadoReactores.ToList();
        }

        public async Task<Reactor> GetByIdAsync(int reactor_id)
        {
            Reactor unReactor = new();
            
            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@reactor_id", reactor_id,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT v.reactor_id id, v.reactor_nombre nombre, v.ubicacion_pais ubicacionPais, " +
                "v.ubicacion_ciudad ubicacionCiudad, v.reactor_tipo tipoReactor, v.reactor_estado estadoReactor, " +
                "v.potencia_termica potenciaTermica, v.fecha_primera_reaccion fechaPrimeraReaccion " +
                "FROM v_info_reactores v " +
                "WHERE v.reactor_id = @reactor_id";

            var resultado = await conexion
                .QueryAsync<Reactor>(sentenciaSQL,parametrosSentencia);

            if (resultado.Any())
                unReactor = resultado.First();

            return unReactor;
        }
    }
}
