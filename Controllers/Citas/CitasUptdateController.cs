using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Citas;
using simulacro2.DTOs.Citas;

namespace simulacro2.Controllers.Citas
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class CitasUpdateController : ControllerBase
    {
        private readonly ICitasRepository _repository;
        public CitasUpdateController(ICitasRepository repository)
        {
            _repository = repository;
        }

        [HttpPut]
        [Route("api/citas/update/{id}")]
        public async Task<IActionResult> UpdateCita(int id, [FromBody] CitaDTO citaDTO)
        {
            try
            {
                if (citaDTO == null)
                {
                    return BadRequest("Los datos de la cita son inválidos.");
                }

                var (cita, mensaje, statusCode) = await _repository.Update(id, citaDTO);
                if (statusCode == HttpStatusCode.OK)
                {
                    return Ok(cita);
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
                    "Error actualizando el registro de cita");
            }
        }
    }
}