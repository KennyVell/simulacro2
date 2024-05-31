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
        public async Task<IActionResult> Delete(int id)
        {
            //buscamos primero y verificar que este en la base de datos
            var result = await _repository.GetById(id);
            if (result.medico == null)
                return NotFound($"No se encontro el medico con ese id {id}");
            
            await _repository.Delete(id);
            Response.Headers.Add("Message", "Medico eliminado correctamente");
            return StatusCode(StatusCodes.Status204NoContent); // Devolver un 204 No Content para "delete"
        }
    }
}