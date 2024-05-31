using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Especialidades;
using simulacro2.DTOs.Especialidades;

namespace simulacro2.Controllers.Especialidades
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class EspecialidadesUpdateController : ControllerBase
    {
        private readonly IEspecialidadesRepository _repository;
        public EspecialidadesUpdateController(IEspecialidadesRepository repository)
        {
            _repository = repository;
        }

        [HttpPut]
        [Route("api/especialidades/update/{id}")]
        public async Task<IActionResult> UpdateEspecialidad(int id, [FromBody] EspecialidadDTO especialidadDTO)
        {
            try
            {
                if (especialidadDTO == null)
                {
                    return BadRequest("Los datos de la especialidad son inválidos.");
                }

                var (especialidad, mensaje, statusCode) = await _repository.Update(id, especialidadDTO);
                if (statusCode == HttpStatusCode.OK)
                {
                    return Ok(especialidad);
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
                    "Error actualizando el registro de especialidad");
            }
        }
    }
}