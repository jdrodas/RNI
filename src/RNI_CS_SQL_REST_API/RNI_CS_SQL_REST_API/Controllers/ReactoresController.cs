using Microsoft.AspNetCore.Mvc;
using RNI_CS_SQL_REST_API.Services;

namespace RNI_CS_SQL_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactoresController(ReactorService reactorService) : Controller
    {
        private readonly ReactorService _reactorService = reactorService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var losReactores = await _reactorService
                .GetAllAsync();

            return Ok(losReactores);
        }
    }
}