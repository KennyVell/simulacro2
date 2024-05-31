using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Tratamientos;
using simulacro2.DTOs.Tratamientos;

namespace simulacro2.Controllers.Tratamientos
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class TratamientosCreateController : ControllerBase
    {
        private readonly ITratamientosRepository _repository;
        public TratamientosCreateController(ITratamientosRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("api/tratamientos/create")]
        public async Task<IActionResult> AddTratamiento([FromBody] TratamientoDTO tratamiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("La tratamiento no puede ser nula.");
            }

            var (result, mensaje, statusCode) = await _repository.Add(tratamiento);
            if (statusCode == HttpStatusCode.Created)
                return CreatedAtAction(nameof(TratamientosController.GetById), "Tratamientos", new { id = result.Id}, result);
            else
                return BadRequest(mensaje);
        }

    }
}