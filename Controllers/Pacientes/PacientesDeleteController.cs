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
        public IActionResult Delete(int id)
        {
            //buscamos primero y verificar que este en la base de datos
            var result = _repository.GetById(id);
            if (result == null)
                return NotFound($"No se encontro el paciente con ese id {id}");

            _repository.Delete(id);
            Response.Headers.Add("X-Message", "Paciente eliminada correctamente");
            return StatusCode(204); // Devolver un 204 No Content para "delete"
        }
    }
}