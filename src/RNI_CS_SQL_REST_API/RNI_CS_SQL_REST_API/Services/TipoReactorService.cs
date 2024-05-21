using RNI_CS_SQL_REST_API.Exceptions;
using RNI_CS_SQL_REST_API.Interfaces;
using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Services
{
    public class TipoReactorService(ITipoReactorRepository tipoReactorRepository,
                                    IReactorRepository reactorRepository)
    {
        private readonly ITipoReactorRepository _tipoReactorRepository = tipoReactorRepository;
        private readonly IReactorRepository _reactorRepository = reactorRepository;

        public async Task<List<TipoReactor>> GetAllAsync()
        {
            return await _tipoReactorRepository
                .GetAllAsync();
        }

        public async Task<TipoReactorDetallado> GetByIdAsync(int tipo_reactor_id)
        {
            TipoReactor unTipoReactor = await _tipoReactorRepository
                .GetByIdAsync(tipo_reactor_id);

            if (unTipoReactor.Id == 0)
                throw new AppValidationException($"Tipo de Reactor no encontrado con el id {tipo_reactor_id}");

            TipoReactorDetallado unTipoReactorDetallado = new()
            {
                Id = unTipoReactor.Id,
                Nombre = unTipoReactor.Nombre,
                Reactores = await _reactorRepository
                                    .GetByReactorTypeIdAsync(tipo_reactor_id)
            };

            return unTipoReactorDetallado;
        }
    }
}
