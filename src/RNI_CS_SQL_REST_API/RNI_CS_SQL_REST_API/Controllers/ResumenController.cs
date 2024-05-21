using Microsoft.AspNetCore.Mvc;
using RNI_CS_SQL_REST_API.Services;

namespace RNI_CS_SQL_REST_API.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class ResumenController(ResumenService resumenService) : Controller
    {
        private readonly ResumenService _resumenService = resumenService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var elResumen = await _resumenService
                .GetAllAsync();

            return Ok(elResumen);
        }
    }
}