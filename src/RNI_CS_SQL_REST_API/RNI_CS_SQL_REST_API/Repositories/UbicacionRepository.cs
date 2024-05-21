﻿using Dapper;
using RNI_CS_SQL_REST_API.DBContexts;
using RNI_CS_SQL_REST_API.Interfaces;
using RNI_CS_SQL_REST_API.Models;
using System.Data;

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

            var resultadoUbicaciones = await conexion
                .QueryAsync<Ubicacion>(sentenciaSQL, new DynamicParameters());

            return resultadoUbicaciones.ToList();
        }

        public async Task<Ubicacion> GetByIdAsync(int ubicacion_id)
        {
            Ubicacion unaUbicacion = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@ubicacion_id", ubicacion_id,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT u.id, u.pais, u.ciudad " +
                "FROM core.ubicaciones u " +
                "WHERE u.id = @ubicacion_id";

            var resultado = await conexion.QueryAsync<Ubicacion>(sentenciaSQL,
                parametrosSentencia);

            if (resultado.Any())
                unaUbicacion = resultado.First();

            return unaUbicacion;
        }

    }
}