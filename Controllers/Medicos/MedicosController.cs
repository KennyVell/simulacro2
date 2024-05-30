using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Medicos;

namespace simulacro2.Controllers.Medicos
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class MedicosController : ControllerBase
    {
        private readonly IMedicosRepository _repository;
        public MedicosController(IMedicosRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/medicos")]
        public async Task<IActionResult> GetAll()
        {
            var (medico, mensaje, statusCode) = await _repository.GetAll();
            if (statusCode == HttpStatusCode.OK)
                return Ok(medico);
            else
                return BadRequest(mensaje);
        }

        [HttpGet]
        [Route("api/medicos/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var (medico, mensaje, statusCode) = await _repository.GetById(id);
            if (statusCode == HttpStatusCode.OK)
                return Ok(medico);
            else
                return BadRequest(mensaje);
        }
    }
}