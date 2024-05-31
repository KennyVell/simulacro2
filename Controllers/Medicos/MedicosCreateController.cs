using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Medicos;
using simulacro2.DTOs.Medicos;

namespace simulacro2.Controllers.Medicos
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class MedicosCreateController : ControllerBase
    {
        private readonly IMedicosRepository _repository;
        public MedicosCreateController(IMedicosRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("api/medicos/create")]
        public async Task<IActionResult> AddMedico([FromBody] MedicoDTO medico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("La medico no puede ser nula.");
            }

            var (result, mensaje, statusCode) = await _repository.Add(medico);
            if (statusCode == HttpStatusCode.Created)
                return CreatedAtAction(nameof(MedicosController.GetById), "Medicos", new { id = result.Id}, result);
            else
                return BadRequest(mensaje);
        }

    }
}