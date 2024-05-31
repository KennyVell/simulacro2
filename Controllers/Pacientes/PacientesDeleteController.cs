using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Pacientes;

namespace simulacro2.Controllers.Pacientes
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class PacientesDeleteController : ControllerBase
    {
        private readonly IPacientesRepository _repository;
        public PacientesDeleteController(IPacientesRepository repository)
        {
            _repository = repository;
        }

        [HttpDelete]
        [Route("api/pacientes/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //buscamos primero y verificar que este en la base de datos
            var result = await _repository.GetById(id);
            if (result.paciente == null)
                return NotFound($"No se encontro el paciente con ese id {id}");

            await _repository.Delete(id);
            Response.Headers.Add("Message", "Paciente eliminado correctamente");
            return StatusCode(StatusCodes.Status204NoContent); // Devolver un 204 No Content para "delete"
        }
    }
}