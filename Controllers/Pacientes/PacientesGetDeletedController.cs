using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Pacientes;

namespace simulacro2.Controllers.Pacientes
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class PacientesGetDeletedController : ControllerBase
    {
        private readonly IPacientesRepository _repository;
        public PacientesGetDeletedController(IPacientesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/pacientes/delete")]
        public async Task<IActionResult> GetDeleted()
        {
            var (paciente, mensaje, statusCode) = await _repository.GetAllDeleted();
            if (statusCode == HttpStatusCode.OK)
                return Ok(paciente);
            else
                return BadRequest(mensaje);
        }
    }
}