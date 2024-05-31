using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Medicos;

namespace simulacro2.Controllers.Medicos
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class MedicosDeleteController : ControllerBase
    {
        private readonly IMedicosRepository _repository;
        public MedicosDeleteController(IMedicosRepository repository)
        {
            _repository = repository;
        }

        [HttpDelete]
        [Route("api/medicos/delete/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            Response.Headers.Add("X-Message", "Medico eliminada correctamente");
            return StatusCode(204); // Devolver un 204 No Content para "delete"
        }
    }
}