using Npgsql;
using System.Data;

namespace RNI_CS_SQL_REST_API.DBContexts
{
    public class PgsqlDbContext(IConfiguration unaConfiguracion)
    {
        private readonly string cadenaConexion = unaConfiguracion.GetConnectionString("FrutasPL")!;

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(cadenaConexion);
        }
    }
}