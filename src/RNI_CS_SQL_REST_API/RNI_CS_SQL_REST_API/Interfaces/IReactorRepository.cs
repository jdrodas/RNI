using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Interfaces
{
    public interface IReactorRepository
    {
        public Task<List<Reactor>> GetAllAsync();

        public Task<List<Reactor>> GetByReactorTypeIdAsync(int tipo_reactor_id);

        public Task<Reactor> GetByIdAsync(int reactor_id);

        public Task<Reactor> GetByNameAndLocation(Reactor unReactor);

        public Task<bool> CreateAsync(Reactor unReactor, int ubicacion_id);

        public Task<bool> UpdateAsync(Reactor unaReactor, int ubicacion_id);

        public Task<bool> RemoveAsync(int reactor_id);

        public Task<int> GetReactorStateIdByNameAsync(string unEstadoReactor);

        public Task<int> GetReactorTypeIdByNameAsync(string unTipoReactor);
    }
}
