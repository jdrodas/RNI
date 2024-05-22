﻿using Microsoft.AspNetCore.Mvc;
using RNI_CS_SQL_REST_API.Exceptions;
using RNI_CS_SQL_REST_API.Models;
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

        [HttpGet("{reactor_id:int}")]
        public async Task<IActionResult> GetByIdAsync(int reactor_id)
        {
            try
            {
                var unReactor = await _reactorService
                    .GetByIdAsync(reactor_id);

                return Ok(unReactor);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Reactor unReactor)
        {
            try
            {
                var reactorCreado = await _reactorService
                    .CreateAsync(unReactor);

                return Ok(reactorCreado);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error en la validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error en la operación de la DB {error.Message}");
            }
        }
    }
}