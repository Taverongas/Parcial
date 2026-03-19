using Microsoft.AspNetCore.Mvc;
using Parcial.Interfaces;
using Parcial.Models;

namespace Parcial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreguntasController : ControllerBase
    {
        private readonly IPreguntaService _service;

        public PreguntasController(IPreguntaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPregunta([FromBody] Pregunta pregunta)
        {
            var result = await _service.CrearPregunta(pregunta);
            return Ok(result);
        }

        [HttpGet("{estado}")]
        public async Task<IActionResult> ObtenerPorEstado(string estado)
        {
            var result = await _service.ObtenerPorEstado(estado);
            return Ok(result);
        }
    }
}
