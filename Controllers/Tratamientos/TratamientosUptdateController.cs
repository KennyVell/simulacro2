using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Tratamientos;
using simulacro2.DTOs.Tratamientos;

namespace simulacro2.Controllers.Tratamientos
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class TratamientosUpdateController : ControllerBase
    {
        private readonly ITratamientosRepository _repository;
        public TratamientosUpdateController(ITratamientosRepository repository)
        {
            _repository = repository;
        }

        [HttpPut]
        [Route("api/tratamientos/update/{id}")]
        public async Task<IActionResult> UpdateTratamiento(int id, [FromBody] TratamientoDTO tratamientoDTO)
        {
            try
            {
                if (tratamientoDTO == null)
                {
                    return BadRequest("Los datos de la tratamiento son inválidos.");
                }

                var (tratamiento, mensaje, statusCode) = await _repository.Update(id, tratamientoDTO);
                if (statusCode == HttpStatusCode.OK)
                {
                    return Ok(tratamiento);
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
                    "Error actualizando el registro de tratamiento");
            }
        }
    }
}