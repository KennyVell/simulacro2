using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Pacientes;

namespace simulacro2.Controllers.Pacientes
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class PacientesRestoreController : ControllerBase
    {
        private readonly IPacientesRepository _repository;
        public PacientesRestoreController(IPacientesRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("api/pacientes/restore/{id}")]
        public async Task<IActionResult> RestorePaciente(int id)
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