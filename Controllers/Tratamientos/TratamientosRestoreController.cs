using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Tratamientos;

namespace simulacro2.Controllers.Tratamientos
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class TratamientosRestoreController : ControllerBase
    {
        private readonly ITratamientosRepository _repository;
        public TratamientosRestoreController(ITratamientosRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("api/tratamientos/restore/{id}")]
        public async Task<IActionResult> RestoreTratamiento(int id)
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