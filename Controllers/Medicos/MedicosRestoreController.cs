using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Medicos;

namespace simulacro2.Controllers.Medicos
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class MedicosRestoreController : ControllerBase
    {
        private readonly IMedicosRepository _repository;
        public MedicosRestoreController(IMedicosRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("api/medicos/restore/{id}")]
        public async Task<IActionResult> RestoreMedico(int id)
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