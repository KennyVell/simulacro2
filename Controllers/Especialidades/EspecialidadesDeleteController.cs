using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Especialidades;

namespace simulacro2.Controllers.Especialidades
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class EspecialidadesDeleteController : ControllerBase
    {
        private readonly IEspecialidadesRepository _repository;
        public EspecialidadesDeleteController(IEspecialidadesRepository repository)
        {
            _repository = repository;
        }

        [HttpDelete]
        [Route("api/especialidades/delete/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            Response.Headers.Add("X-Message", "Especialidad eliminada correctamente");
            return StatusCode(204); // Devolver un 204 No Content para "delete"
        }
    }
}