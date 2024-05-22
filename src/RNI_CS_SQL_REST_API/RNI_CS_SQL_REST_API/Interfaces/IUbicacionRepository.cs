using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Interfaces
{
    public interface IUbicacionRepository
    {
        public Task<List<Ubicacion>> GetAllAsync();

        public Task<Ubicacion> GetByIdAsync(int ubicacion_id);

        public Task<Ubicacion> GetByCountryAndCityAsync(string ubicacion_pais, string ubicacion_ciudad);

        public Task<List<Reactor>> GetAssociatedReactorsAsync(int ubicacion_id);
    }
}