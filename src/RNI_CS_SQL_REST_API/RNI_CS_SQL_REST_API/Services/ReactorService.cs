using RNI_CS_SQL_REST_API.Exceptions;
using RNI_CS_SQL_REST_API.Interfaces;
using RNI_CS_SQL_REST_API.Models;
using RNI_CS_SQL_REST_API.Repositories;

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

        public async Task<Reactor> GetByIdAsync(int reactor_id)
        {
            Reactor unReactor = await _reactorRepository
                .GetByIdAsync(reactor_id);

            if (unReactor.Id == 0)
                throw new AppValidationException($"Reactor no encontrado con el id {reactor_id}");

            return unReactor;
        }

    }
}
