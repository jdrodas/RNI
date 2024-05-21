using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Interfaces
{
    public interface IResumenRepository
    {
        public Task<Resumen> GetAllAsync();
    }
}