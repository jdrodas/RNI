using RNI_CS_SQL_REST_API.Interfaces;
using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Services
{
    public class ReactorService(IReactorRepository reactorRepository)
    {
        private readonly IReactorRepository _reactorRepository = reactorRepository;

        public async Task<List<Reactor>> GetAllAsync()
        {
            return await _reactorRepository
                .GetAllAsync();
        }
    }
}
