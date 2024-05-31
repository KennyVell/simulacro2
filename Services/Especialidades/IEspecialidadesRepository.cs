using System.Net;
using simulacro2.DTOs.Especialidades;
using simulacro2.Models;

namespace simulacro2.Services.Especialidades
{
    public interface IEspecialidadesRepository
    {
        Task<(Especialidad especialidad, string mensaje, HttpStatusCode statusCode)> Add(EspecialidadCreateDTO especialidadDTO);
        Task<(IEnumerable<Especialidad> especialidades, string mensaje, HttpStatusCode statusCode)> GetAll();
        Task<(IEnumerable<Especialidad> especialidades, string mensaje, HttpStatusCode statusCode)> GetAllDeleted();
        Task<(Especialidad especialidad, string mensaje, HttpStatusCode statusCode)> GetById(int id);
        Task<(Especialidad especialidad, string mensaje, HttpStatusCode statusCode)> Update(int id, EspecialidadDTO especialidadDTO);
        void Delete(int id);
        Task<(Especialidad especialidad, string mensaje, HttpStatusCode statusCode)> Restore(int id);
    }
}