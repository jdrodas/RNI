using RNI_CS_SQL_REST_API.Interfaces;
using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Services
{
    public class TipoReactorService(ITipoReactorRepository tipoReactorRepository)
    {
        private readonly ITipoReactorRepository _tipoReactorRepository = tipoReactorRepository;

        public async Task<List<TipoReactor>> GetAllAsync()
        {
            return await _tipoReactorRepository
                .GetAllAsync();
        }
    }
}
