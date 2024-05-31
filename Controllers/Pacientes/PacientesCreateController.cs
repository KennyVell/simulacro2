using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Pacientes;
using simulacro2.DTOs.Pacientes;

namespace simulacro2.Controllers.Pacientes
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class PacientesCreateController : ControllerBase
    {
        private readonly IPacientesRepository _repository;
        public PacientesCreateController(IPacientesRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("api/pacientes/create")]
        public async Task<IActionResult> AddPaciente([FromBody] PacienteDTO paciente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("La paciente no puede ser nula.");
            }

            var (result, mensaje, statusCode) = await _repository.Add(paciente);
            if (statusCode == HttpStatusCode.Created)
                return CreatedAtAction(nameof(PacientesController.GetById), "Pacientes", new { id = result.Id}, result);
            else
                return BadRequest(mensaje);
        }

    }
}