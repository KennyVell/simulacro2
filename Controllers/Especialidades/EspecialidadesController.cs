using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Especialidades;

namespace simulacro2.Controllers.Especialidades
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class EspecialidadesController : ControllerBase
    {
        private readonly IEspecialidadesRepository _repository;
        public EspecialidadesController(IEspecialidadesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/especialidades")]
        public async Task<IActionResult> GetAll()
        {
            var (especialidades, mensaje, statusCode) = await _repository.GetAll();
            if (statusCode == HttpStatusCode.OK)
                return Ok(especialidades);
            else
                return BadRequest(mensaje);
        }

        [HttpGet]
        [Route("api/especialidades/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var (especialidad, mensaje, statusCode) = await _repository.GetById(id);
            if (statusCode == HttpStatusCode.OK)
                return Ok(especialidad);
            else
                return BadRequest(mensaje);
        }
    }
}