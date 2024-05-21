using RNI_CS_SQL_REST_API.Exceptions;
using RNI_CS_SQL_REST_API.Interfaces;
using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Services
{
    public class UbicacionService(IUbicacionRepository ubicacionRepository)
    {
        private readonly IUbicacionRepository _ubicacionRepository = ubicacionRepository;

        public async Task<List<Ubicacion>> GetAllAsync()
        {
            return await _ubicacionRepository
                .GetAllAsync();
        }

        public async Task<Ubicacion> GetByIdAsync(int ubicacion_id)
        {
            Ubicacion unaUbicacion = await _ubicacionRepository
                .GetByIdAsync(ubicacion_id);

            if (unaUbicacion.Id == 0)
                throw new AppValidationException($"Ubicación no encontrada con el id {ubicacion_id}");

            return unaUbicacion;
        }

        public async Task<List<Reactor>> GetAssociatedReactorsAsync(int ubicacion_id)
        {
            Ubicacion unaUbicacion = await _ubicacionRepository
                .GetByIdAsync(ubicacion_id);

            if (unaUbicacion.Id == 0)
                throw new AppValidationException($"Ubicación no encontrada con el id {ubicacion_id}");

            var reactoresAsociados = await _ubicacionRepository
                .GetAssociatedReactorsAsync(ubicacion_id);

            if (reactoresAsociados.Count == 0)
                throw new AppValidationException($"Ubicacion {unaUbicacion.Pais} - {unaUbicacion.Ciudad} no tiene reactores localizados allí");

            return reactoresAsociados;
        }
    }
}
