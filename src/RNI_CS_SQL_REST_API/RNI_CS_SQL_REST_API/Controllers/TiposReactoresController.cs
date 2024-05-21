using Microsoft.AspNetCore.Mvc;
using RNI_CS_SQL_REST_API.Exceptions;
using RNI_CS_SQL_REST_API.Services;

namespace RNI_CS_SQL_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposReactoresController(TipoReactorService tipoReactorService) : Controller
    {
        private readonly TipoReactorService _tipoReactorService = tipoReactorService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var losTiposReactores = await _tipoReactorService
                .GetAllAsync();

            return Ok(losTiposReactores);
        }

        [HttpGet("{tipo_reactor_id:int}")]
        public async Task<IActionResult> GetByIdAsync(int tipo_reactor_id)
        {
            try
            {
                var unTipoReactor = await _tipoReactorService
                    .GetByIdAsync(tipo_reactor_id);

                return Ok(unTipoReactor);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }
    }
}