using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Especialidades;
using simulacro2.DTOs.Especialidades;

namespace simulacro2.Controllers.Especialidades
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class EspecialidadesCreateController : ControllerBase
    {
        private readonly IEspecialidadesRepository _repository;
        public EspecialidadesCreateController(IEspecialidadesRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("api/especialidades/create")]
        public async Task<IActionResult> AddEspecialidad([FromBody] EspecialidadCreateDTO especialidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("La especialidad no puede ser nula.");
            }

            var (result, mensaje, statusCode) = await _repository.Add(especialidad);
            if (statusCode == HttpStatusCode.Created)
                return CreatedAtAction(nameof(EspecialidadesController.GetById), "Especialidades", new { id = result.Id}, result);
            else
                return BadRequest(mensaje);
        }

    }
}