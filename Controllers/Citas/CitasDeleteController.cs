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
        public async Task<IActionResult> Delete(int id)
        {
            //buscamos primero y verificar que este en la base de datos
            var result = await _repository.GetById(id);
            if (result.cita == null)
                return NotFound($"No se encontro la cita con el id {id}");
            
            await _repository.Delete(id);
            Response.Headers.Add("Message", "Cita eliminada correctamente");
            return StatusCode(StatusCodes.Status204NoContent); // Devolver un 204 No Content para "delete"
        }
    }
}