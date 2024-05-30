using System.Net;
using simulacro2.DTOs.Pacientes;
using simulacro2.Models;

namespace simulacro2.Services.Pacientes
{
    public interface IPacientesRepository
    {
        Task<(Paciente paciente, string mensaje, HttpStatusCode statusCode)> Add(PacienteDTO pacienteDTO);
        Task<(IEnumerable<Paciente> pacientes, string mensaje, HttpStatusCode statusCode)> GetAll();
        Task<(IEnumerable<Paciente> pacientes, string mensaje, HttpStatusCode statusCode)> GetDelete();
        Task<(Paciente paciente, string mensaje, HttpStatusCode statusCode)> GetById(int id);
        Task<(Paciente paciente, string mensaje, HttpStatusCode statusCode)> Update(PacienteDTO pacienteDTO);
        void Delete(int id);      
    }
}