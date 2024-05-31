using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Citas;

namespace simulacro2.Controllers.Citas
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class CitasGetDeletedController : ControllerBase
    {
        private readonly ICitasRepository _repository;
        public CitasGetDeletedController(ICitasRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/citas/delete")]
        public async Task<IActionResult> GetDeleted()
        {
            var (cita, mensaje, statusCode) = await _repository.GetAllDeleted();
            if (statusCode == HttpStatusCode.OK)
                return Ok(cita);
            else
                return BadRequest(mensaje);
        }
    }
}