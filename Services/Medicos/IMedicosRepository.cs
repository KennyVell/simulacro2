using System.Net;
using simulacro2.DTOs.Medicos;
using simulacro2.Models;

namespace simulacro2.Services.Medicos
{
    public interface IMedicosRepository
    {
        Task<(Medico medico, string mensaje, HttpStatusCode statusCode)> Add(MedicoDTO medicoDTO);
        Task<(IEnumerable<Medico> medicos, string mensaje, HttpStatusCode statusCode)> GetAll();
        Task<(IEnumerable<Medico> medicos, string mensaje, HttpStatusCode statusCode)> GetAllDeleted();
        Task<(Medico medico, string mensaje, HttpStatusCode statusCode)> GetById(int id);
        Task<(Medico medico, string mensaje, HttpStatusCode statusCode)> Update(int id, MedicoDTO medicoDTO);
        Task Delete(int id);
        Task<(Medico medico, string mensaje, HttpStatusCode statusCode)> Restore(int id);
    }
}