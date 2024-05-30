using System.Net;
using simulacro2.DTOs.Especialidades;
using simulacro2.Models;

namespace simulacro2.Services.Especialidades
{
    public interface IEspecialidadesRepository
    {
        Task<(Especialidad especialidad, string mensaje, HttpStatusCode statusCode)> Add(EspecialidadDTO especialidadDTO);
        Task<(IEnumerable<Especialidad> especialidades, string mensaje, HttpStatusCode statusCode)> GetAll();
        Task<(IEnumerable<Especialidad> especialidades, string mensaje, HttpStatusCode statusCode)> GetDelete();
        Task<(Especialidad especialidad, string mensaje, HttpStatusCode statusCode)> GetById(int id);
        Task<(Especialidad especialidad, string mensaje, HttpStatusCode statusCode)> Update(EspecialidadDTO especialidadDTO);
        void Delete(int id);

    }
}