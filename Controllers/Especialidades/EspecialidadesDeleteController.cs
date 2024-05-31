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
        public async Task<IActionResult> Delete(int id)
        {
            //buscamos primero y verificar que este en la base de datos
            var result = await _repository.GetById(id);
            if (result.especialidad == null)
                return NotFound($"No se encontro la especialidad con ese id {id}");
            
            await _repository.Delete(id);
            Response.Headers.Add("Message", "Especialidad eliminada correctamente");
            return StatusCode(StatusCodes.Status204NoContent); // Devolver un 204 No Content para "delete"
        }
    }
}