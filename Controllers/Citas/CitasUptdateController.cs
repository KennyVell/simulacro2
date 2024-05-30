using Microsoft.AspNetCore.Mvc;
using System.Net;
using simulacro2.Services.Citas;
using simulacro2.DTOs.Citas;

namespace simulacro2.Controllers.Citas
{
    /* [ApiController]
    [Route("api/[controller]")] */
    public class CitasUpdateController : ControllerBase
    {
        private readonly ICitasRepository _repository;
        public CitasUpdateController(ICitasRepository repository)
        {
            _repository = repository;
        }
    }
}