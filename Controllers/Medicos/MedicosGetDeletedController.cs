using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Medicos;

namespace simulacro2.Controllers.Medicos
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class MedicosGetDeletedController : ControllerBase
    {
        private readonly IMedicosRepository _repository;
        public MedicosGetDeletedController(IMedicosRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/medicos/delete")]
        public async Task<IActionResult> GetDeleted()
        {
            var (medico, mensaje, statusCode) = await _repository.GetAllDeleted();
            if (statusCode == HttpStatusCode.OK)
                return Ok(medico);
            else
                return BadRequest(mensaje);
        }
    }
}