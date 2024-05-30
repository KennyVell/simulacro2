using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Citas;

namespace simulacro2.Controllers.Citas
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class CitasGetCanceladasController : ControllerBase
    {
        private readonly ICitasRepository _repository;
        public CitasGetCanceladasController(ICitasRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/citas/canceladas")]
        public async Task<IActionResult> GetCanceladas()
        {
            var (cita, mensaje, statusCode) = await _repository.GetCanceladas();
            if (statusCode == HttpStatusCode.OK)
                return Ok(cita);
            else
                return BadRequest(mensaje);
        }
    }
}