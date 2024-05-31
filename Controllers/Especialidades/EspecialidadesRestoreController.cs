using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Especialidades;

namespace simulacro2.Controllers.Especialidades
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class EspecialidadesRestoreController : ControllerBase
    {
        private readonly IEspecialidadesRepository _repository;
        public EspecialidadesRestoreController(IEspecialidadesRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("api/especialidades/restore/{id}")]
        public async Task<IActionResult> RestoreEspecialidad(int id)
        {
            var (result, mensaje, statusCode) = await _repository.Restore(id);

            if (statusCode == HttpStatusCode.OK)
            {
                return Ok(result); // devolver el objeto restaurado
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

    }
}