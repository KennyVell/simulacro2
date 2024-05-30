using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Tratamientos;

namespace simulacro2.Controllers.Tratamientos
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class TratamientosController : ControllerBase
    {
        private readonly ITratamientosRepository _repository;
        public TratamientosController(ITratamientosRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/tratamientos")]
        public async Task<IActionResult> GetAll()
        {
            var (tratamiento, mensaje, statusCode) = await _repository.GetAll();
            if (statusCode == HttpStatusCode.OK)
                return Ok(tratamiento);
            else
                return BadRequest(mensaje);
        }

        [HttpGet]
        [Route("api/tratamientos/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var (tratamiento, mensaje, statusCode) = await _repository.GetById(id);
            if (statusCode == HttpStatusCode.OK)
                return Ok(tratamiento);
            else
                return BadRequest(mensaje);
        }
    }
}