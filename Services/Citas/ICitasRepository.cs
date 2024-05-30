using System.Net;
using simulacro2.DTOs.Citas;
using simulacro2.Models;

namespace simulacro2.Services.Citas
{
    public interface ICitasRepository
    {
        Task<(Cita cita, string mensaje, HttpStatusCode statusCode)> Add(CitaDTO citaDTO);
        Task<(IEnumerable<Cita> citas, string mensaje, HttpStatusCode statusCode)> GetAll();
        Task<(IEnumerable<Cita> citas, string mensaje, HttpStatusCode statusCode)> GetCanceladas();
        Task<(Cita cita, string mensaje, HttpStatusCode statusCode)> GetById(int id);
        Task<(Cita cita, string mensaje, HttpStatusCode statusCode)> Update(CitaDTO citaDTO);
        void Delete(int id);
    }
}