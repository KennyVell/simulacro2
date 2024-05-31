using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Especialidades;

namespace simulacro2.Controllers.Especialidades
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class EspecialidadesGetDeletedController : ControllerBase
    {
        private readonly IEspecialidadesRepository _repository;
        public EspecialidadesGetDeletedController(IEspecialidadesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/especialidades/delete")]
        public async Task<IActionResult> GetDeleted()
        {
            var (especialidad, mensaje, statusCode) = await _repository.GetAllDeleted();
            if (statusCode == HttpStatusCode.OK)
                return Ok(especialidad);
            else
                return BadRequest(mensaje);
        }
    }
}