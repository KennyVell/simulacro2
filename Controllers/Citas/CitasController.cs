using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services;
using simulacro2.Services.Citas;

namespace simulacro2.Controllers.Citas
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class CitasController : ControllerBase
    {
        private readonly ICitasRepository _repository;
        public CitasController(ICitasRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/citas")]
        public async Task<IActionResult> GetAll()
        {
            var (citas, mensaje, statusCode) = await _repository.GetAll();
            if (statusCode == HttpStatusCode.OK)
                return Ok(citas);
            else
                return BadRequest(mensaje);
        }

        [HttpGet]
        [Route("api/citas/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var (cita, mensaje, statusCode) = await _repository.GetById(id);
            if (statusCode == HttpStatusCode.OK)
                return Ok(cita);
            else
                return BadRequest(mensaje);
        }
    }
}