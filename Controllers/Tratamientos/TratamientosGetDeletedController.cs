using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Tratamientos;

namespace simulacro2.Controllers.Tratamientos
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class TratamientosGetDeletedController : ControllerBase
    {
        private readonly ITratamientosRepository _repository;
        public TratamientosGetDeletedController(ITratamientosRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/tratamientos/delete")]
        public async Task<IActionResult> GetDeleted()
        {
            var (tratamiento, mensaje, statusCode) = await _repository.GetAllDeleted();
            if (statusCode == HttpStatusCode.OK)
                return Ok(tratamiento);
            else
                return BadRequest(mensaje);
        }
    }
}