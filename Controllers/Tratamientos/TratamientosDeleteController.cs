using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Tratamientos;

namespace simulacro2.Controllers.Tratamientos
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class TratamientosDeleteController : ControllerBase
    {
        private readonly ITratamientosRepository _repository;
        public TratamientosDeleteController(ITratamientosRepository repository)
        {
            _repository = repository;
        }

        [HttpDelete]
        [Route("api/tratamientos/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //buscamos primero y verificar que este en la base de datos
            var result = await _repository.GetById(id);
            if (result.tratamiento == null)
                return NotFound($"No se encontro el tratamiento con ese id {id}");

            await _repository.Delete(id);
            Response.Headers.Add("Message", "Tratamiento eliminado correctamente");
            return StatusCode(StatusCodes.Status204NoContent); // Devolver un 204 No Content para "delete"
        }
    }
}