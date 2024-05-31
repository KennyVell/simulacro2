using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Pacientes;
using simulacro2.DTOs.Pacientes;

namespace simulacro2.Controllers.Pacientes
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class PacientesUpdateController : ControllerBase
    {
        private readonly IPacientesRepository _repository;
        public PacientesUpdateController(IPacientesRepository repository)
        {
            _repository = repository;
        }

        [HttpPut]
        [Route("api/pacientes/update/{id}")]
        public async Task<IActionResult> UpdatePaciente(int id, [FromBody] PacienteDTO pacienteDTO)
        {
            try
            {
                if (pacienteDTO == null)
                {
                    return BadRequest("Los datos de la paciente son inválidos.");
                }

                var (paciente, mensaje, statusCode) = await _repository.Update(id, pacienteDTO);
                if (statusCode == HttpStatusCode.OK)
                {
                    return Ok(paciente);
                }
                else if (statusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(mensaje);
                }
                else
                {
                    return BadRequest(mensaje);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error actualizando el registro de paciente");
            }
        }
    }
}