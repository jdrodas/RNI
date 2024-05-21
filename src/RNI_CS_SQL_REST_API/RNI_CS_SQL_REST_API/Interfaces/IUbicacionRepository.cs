using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Interfaces
{
    public interface IUbicacionRepository
    {
        public Task<List<Ubicacion>> GetAllAsync();

        public Task<Ubicacion> GetByIdAsync(int departamento_id);
    }
}