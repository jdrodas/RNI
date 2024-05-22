using RNI_CS_SQL_REST_API.Exceptions;
using RNI_CS_SQL_REST_API.Interfaces;
using RNI_CS_SQL_REST_API.Models;

namespace RNI_CS_SQL_REST_API.Services
{
    public class ReactorService(IReactorRepository reactorRepository,
                                IUbicacionRepository ubicacionRepository)
    {
        private readonly IReactorRepository _reactorRepository = reactorRepository;
        private readonly IUbicacionRepository _ubicacionRepository = ubicacionRepository;

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

        public async Task<Reactor> CreateAsync(Reactor unReactor)
        {

            if (string.IsNullOrEmpty(unReactor.UbicacionPais))
                throw new AppValidationException("El país donde está ubicado el reactor no puede estar vacío");

            if (string.IsNullOrEmpty(unReactor.UbicacionCiudad))
                throw new AppValidationException("La ciudad donde está ubicado el reactor no puede estar vacío");

            var ubicacionExistente = await _ubicacionRepository
                .GetByCountryAndCityAsync(unReactor.UbicacionPais, unReactor.UbicacionCiudad);

            if (ubicacionExistente.Id == 0)
                throw new AppValidationException($"No hay una ubicación {unReactor.UbicacionPais} - {unReactor.UbicacionCiudad} " +
                    $"previamente registrada para el reactor");

            if (string.IsNullOrEmpty(unReactor.Nombre))
                throw new AppValidationException("El nombre del reactor no puede estar vacío");

            var reactorExistente = await _reactorRepository
                .GetByNameAndLocation(unReactor);

            if (reactorExistente.Id != 0)
                throw new AppValidationException($"Ya existe un reactor con el nombre {unReactor.Nombre} en la ubicación" +
                    $" {unReactor.UbicacionPais} - {unReactor.UbicacionCiudad} previamente registrado");

            if (string.IsNullOrEmpty(unReactor.EstadoReactor))
                throw new AppValidationException("El estado del reactor no puede estar vacío");

            var estadoReactor_id = await _reactorRepository
                .GetReactorStateIdByNameAsync(unReactor.EstadoReactor!);

            if (estadoReactor_id == 0)
                throw new AppValidationException($"El estado del reactor {unReactor.EstadoReactor} no está previamente registrado");

            if (string.IsNullOrEmpty(unReactor.TipoReactor))
                throw new AppValidationException("El tipo del reactor no puede estar vacío");

            var tipoReactor_id = await _reactorRepository
                .GetReactorTypeIdByNameAsync(unReactor.TipoReactor!);

            if (tipoReactor_id == 0)
                throw new AppValidationException($"El tipo de reactor {unReactor.TipoReactor} no está previamente registrado");

            if (unReactor.PotenciaTermica < 0)
                throw new AppValidationException("La potencia del reactor debe ser un valor positivo mayor o igual a cero");

            if (unReactor.FechaPrimeraReaccion.HasValue && unReactor.FechaPrimeraReaccion.Value > DateTime.Now)
                throw new AppValidationException("La fecha de la primera reacción no puede ser en el futuro");

            try
            {
                bool resultado = await _reactorRepository
                    .CreateAsync(unReactor, ubicacionExistente.Id);

                if (!resultado)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios");

                reactorExistente = await _reactorRepository
                    .GetByNameAndLocation(unReactor);
            }
            catch (DbOperationException)
            {
                throw;
            }

            return reactorExistente;
        }
    }
}
