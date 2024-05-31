using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Citas;

namespace simulacro2.Controllers.Citas
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class CitasDeleteController : ControllerBase
    {
        private readonly ICitasRepository _repository;
        public CitasDeleteController(ICitasRepository repository)
        {
            _repository = repository;
        }

        [HttpDelete]
        [Route("api/citas/delete/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            Response.Headers.Add("X-Message", "Cita eliminada correctamente");
            return StatusCode(204); // Devolver un 204 No Content para "delete"
        }
    }
}