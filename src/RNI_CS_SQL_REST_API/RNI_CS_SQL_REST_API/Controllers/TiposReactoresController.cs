using Microsoft.AspNetCore.Mvc;
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
            var losReactores = await _tipoReactorService
                .GetAllAsync();

            return Ok(losReactores);
        }
    }
}