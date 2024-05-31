using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Medicos;
using simulacro2.DTOs.Medicos;

namespace simulacro2.Controllers.Medicos
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class MedicosUpdateController : ControllerBase
    {
        private readonly IMedicosRepository _repository;
        public MedicosUpdateController(IMedicosRepository repository)
        {
            _repository = repository;
        }

        [HttpPut]
        [Route("api/medicos/update/{id}")]
        public async Task<IActionResult> UpdateMedico(int id, [FromBody] MedicoDTO medicoDTO)
        {
            try
            {
                if (medicoDTO == null)
                {
                    return BadRequest("Los datos de la medico son inválidos.");
                }

                var (medico, mensaje, statusCode) = await _repository.Update(id, medicoDTO);
                if (statusCode == HttpStatusCode.OK)
                {
                    return Ok(medico);
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
                    "Error actualizando el registro de medico");
            }
        }
    }
}