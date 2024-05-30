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

    }
}