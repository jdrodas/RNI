using Microsoft.AspNetCore.Mvc;
using RNI_CS_SQL_REST_API.Exceptions;
using RNI_CS_SQL_REST_API.Services;

namespace RNI_CS_SQL_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionesController(UbicacionService ubicacionService) : Controller
    {
        private readonly UbicacionService _ubicacionService = ubicacionService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var lasUbicaciones = await _ubicacionService
                .GetAllAsync();

            return Ok(lasUbicaciones);
        }

        [HttpGet("{ubicacion_id:int}")]
        public async Task<IActionResult> GetByIdAsync(int ubicacion_id)
        {
            try
            {
                var unaUbicacion = await _ubicacionService
                    .GetByIdAsync(ubicacion_id);

                return Ok(unaUbicacion);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpGet("{ubicacion_id:int}/Reactores")]
        public async Task<IActionResult> GetAssociatedReactorsAsync(int ubicacion_id)
        {
            try
            {
                var losReactoresAsociados = await _ubicacionService
                    .GetAssociatedReactorsAsync(ubicacion_id);

                return Ok(losReactoresAsociados);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }
    }
}