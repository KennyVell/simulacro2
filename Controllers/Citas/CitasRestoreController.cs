using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Citas;

namespace simulacro2.Controllers.Citas
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class CitasRestoreController : ControllerBase
    {
        private readonly ICitasRepository _repository;
        public CitasRestoreController(ICitasRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("api/citas/restore/{id}")]
        public async Task<IActionResult> RestoreCita(int id)
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