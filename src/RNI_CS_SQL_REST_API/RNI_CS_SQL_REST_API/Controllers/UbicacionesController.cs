using Microsoft.AspNetCore.Mvc;
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
    }
}