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
    }
}
