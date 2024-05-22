/*
DbOperationException:
Excepcion creada para enviar mensajes relacionados 
con la ejecución en todas las operaciones CRUD de la aplicación
*/

namespace RNI_CS_SQL_REST_API.Exceptions
{
    public class DbOperationException(string message) : Exception(message)
    {
    }
}
