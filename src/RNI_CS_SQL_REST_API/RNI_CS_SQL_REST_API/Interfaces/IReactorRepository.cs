using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Interfaces
{
    public interface IReactorRepository
    {
        public Task<List<Reactor>> GetAllAsync();
    }
}
