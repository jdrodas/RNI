using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Interfaces
{
    public interface IReactorRepository
    {
        public Task<List<Reactor>> GetAllAsync();

        public Task<List<Reactor>> GetByReactorTypeIdAsync(int tipo_reactor_id);

        public Task<Reactor> GetByIdAsync(int reactor_id);
    }
}
