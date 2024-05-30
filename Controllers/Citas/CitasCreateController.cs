using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Citas;
using simulacro2.DTOs.Citas;

namespace simulacro2.Controllers.Citas
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class CitasCreateController : ControllerBase
    {
        private readonly ICitasRepository _repository;
        public CitasCreateController(ICitasRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("api/citas/create")]
        public async Task<IActionResult> AddCita([FromBody] CitaDTO citaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("La cita no puede ser nula.");
            }

            var (result, mensaje, statusCode) = await _repository.Add(citaDTO);
            if (statusCode == HttpStatusCode.OK)            
                //return CreatedAtAction("GetById", new { id = result.Id}, result);
                //return CreatedAtAction(nameof(CitasController.GetById), new { id = result.Id}, result);
                //return CreatedAtAction("GetById", "CitasController", new { id = result.Id}, result);
                return CreatedAtAction(nameof(CitasController.GetById), new { id = result.Id}, result);
            else
                return BadRequest(mensaje);        
        }
    }
}