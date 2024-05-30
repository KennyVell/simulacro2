using System.Net;
using simulacro2.Models;

namespace simulacro2.Services.Citas
{
    public interface ICitasRepository
    {
        Task<(Cita cita, string mensaje, HttpStatusCode statusCode)> Add(Cita cita);
        Task<(IEnumerable<Cita> citas, string mensaje, HttpStatusCode statusCode)> GetAll();
        Task<(Cita cita, string mensaje, HttpStatusCode statusCode)> GetById(int id);
        Task<(Cita cita, string mensaje, HttpStatusCode statusCode)> Update(Cita cita);
    }
}