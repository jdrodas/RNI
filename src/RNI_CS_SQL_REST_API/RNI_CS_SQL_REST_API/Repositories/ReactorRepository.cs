using Dapper;
using Npgsql;
using RNI_CS_SQL_REST_API.DBContexts;
using RNI_CS_SQL_REST_API.Exceptions;
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
                .QueryAsync<Reactor>(sentenciaSQL, parametrosSentencia);

            if (resultado.Any())
                unReactor = resultado.First();

            return unReactor;
        }

        public async Task<Reactor> GetByNameAndLocation(Reactor unReactor)
        {
            Reactor reactorResultado = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@reactor_nombre", unReactor.Nombre,
                                    DbType.String, ParameterDirection.Input);
            parametrosSentencia.Add("@reactor_pais", unReactor.UbicacionPais,
                                    DbType.String, ParameterDirection.Input);
            parametrosSentencia.Add("@reactor_ciudad", unReactor.UbicacionCiudad,
                                    DbType.String, ParameterDirection.Input);


            string sentenciaSQL = "SELECT v.reactor_id id, v.reactor_nombre nombre, v.ubicacion_pais ubicacionPais, " +
                "v.ubicacion_ciudad ubicacionCiudad, v.reactor_tipo tipoReactor, v.reactor_estado estadoReactor, " +
                "v.potencia_termica potenciaTermica, v.fecha_primera_reaccion fechaPrimeraReaccion " +
                "FROM v_info_reactores v " +
                "WHERE LOWER(v.reactor_nombre) = LOWER(@reactor_nombre) " +
                "AND LOWER(v.ubicacion_pais) = LOWER(@reactor_pais) " +
                "AND LOWER(v.ubicacion_ciudad) = LOWER(@reactor_ciudad)";

            var resultado = await conexion
                .QueryAsync<Reactor>(sentenciaSQL, parametrosSentencia);

            if (resultado.Any())
                reactorResultado = resultado.First();

            return reactorResultado;
        }

        public async Task<bool> CreateAsync(Reactor unReactor, int ubicacion_id)
        {
            bool resultadoAccion = false;

            var estado_reactor_id = await GetReactorStateIdByNameAsync(unReactor.EstadoReactor!);
            var tipo_reactor_id = await GetReactorTypeIdByNameAsync(unReactor.TipoReactor!);

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "core.p_inserta_reactor";

                var parametros = new
                {
                    p_nombre = unReactor.Nombre,
                    p_ubicacion_id = ubicacion_id,
                    p_estado_id = estado_reactor_id,
                    p_tipo_id = tipo_reactor_id,
                    p_potencia_termica = unReactor.PotenciaTermica,
                    p_fecha_primera_reaccion = unReactor.FechaPrimeraReaccion
                };

                var cantidadFilas = await conexion
                    .ExecuteAsync(
                        procedimiento,
                        parametros,
                        commandType: CommandType.StoredProcedure);

                if (cantidadFilas != 0)
                    resultadoAccion = true;
            }
            catch (NpgsqlException error)
            {
                throw new DbOperationException(error.Message);
            }

            return resultadoAccion;
        }

        public async Task<int> GetReactorStateIdByNameAsync(string unEstadoReactor)
        {
            int estado_reactor_id = 0;

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@estado_reactor", unEstadoReactor,
                                    DbType.String, ParameterDirection.Input);

            string sentenciaSQL = "SELECT id " +
                "FROM core.estados_reactores " +
                "WHERE LOWER(nombre) = LOWER(@estado_reactor)";

            var resultado = await conexion
                .QueryAsync<int>(sentenciaSQL, parametrosSentencia);

            if (resultado.Any())
                estado_reactor_id = resultado.First();

            return estado_reactor_id;
        }

        public async Task<int> GetReactorTypeIdByNameAsync(string unTipoReactor)
        {
            int tipo_reactor_id = 0;

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@tipo_reactor", unTipoReactor,
                                    DbType.String, ParameterDirection.Input);

            string sentenciaSQL = "SELECT id " +
                "FROM core.tipos_reactores " +
                "WHERE LOWER(nombre) = LOWER(@tipo_reactor)";

            var resultado = await conexion
                .QueryAsync<int>(sentenciaSQL, parametrosSentencia);

            if (resultado.Any())
                tipo_reactor_id = resultado.First();

            return tipo_reactor_id;
        }
    }
}
