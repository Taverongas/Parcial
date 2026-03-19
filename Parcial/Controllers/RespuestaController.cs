using Microsoft.AspNetCore.Mvc;
using Parcial.Interfaces;
using Parcial.Models;

namespace Parcial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespuestasController : ControllerBase
    {
        private readonly IPreguntaService _service;

        public RespuestasController(IPreguntaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CrearRespuesta([FromBody] Respuesta respuesta)
        {
            var result = await _service.CrearRespuesta(respuesta);
            return Ok(result);
        }
    }
}
