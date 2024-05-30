using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Pacientes;

namespace simulacro2.Controllers.Pacientes
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class PacientesController : ControllerBase
    {
        private readonly IPacientesRepository _repository;
        public PacientesController(IPacientesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/pacientes")]
        public async Task<IActionResult> GetAll()
        {
            var (paciente, mensaje, statusCode) = await _repository.GetAll();
            if (statusCode == HttpStatusCode.OK)
                return Ok(paciente);
            else
                return BadRequest(mensaje);
        }

        [HttpGet]
        [Route("api/pacientes/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var (paciente, mensaje, statusCode) = await _repository.GetById(id);
            if (statusCode == HttpStatusCode.OK)
                return Ok(paciente);
            else
                return BadRequest(mensaje);
        }
    }
}