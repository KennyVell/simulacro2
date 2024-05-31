using Microsoft.EntityFrameworkCore;
using System.Net;
using simulacro2.Data;
using simulacro2.Models;
using simulacro2.DTOs.Pacientes;

namespace simulacro2.Services.Pacientes
{
    public class PacientesRepository : IPacientesRepository
    {
        private readonly ClinicaContext _context;
        public PacientesRepository(ClinicaContext context)
        {
            _context = context;
        }        
        public Task<(Paciente paciente, string mensaje, HttpStatusCode statusCode)> Add(PacienteCreateDTO pacienteDTO)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var paciente = _context.Pacientes.Find(id);            
            paciente.Estado = "inactivo";
            _context.Entry(paciente).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<(IEnumerable<Paciente> pacientes, string mensaje, HttpStatusCode statusCode)> GetAll()
        {
            try
            {
                var pacientes = await _context.Pacientes.Include(p => p.Citas).Where(p => p.Estado.ToLower() == "activo").ToListAsync();
                if (pacientes.Any())
                    return (pacientes, "Pacientes obtenidos correctamente", HttpStatusCode.OK);
                else
                    return (null, "No se encontraron pacientes", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return (null, $"Error al obtener los pacientes: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public async Task<(Paciente paciente, string mensaje, HttpStatusCode statusCode)> GetById(int id)
        {
            try
            {
                var paciente = await _context.Pacientes.Include(p => p.Citas).FirstOrDefaultAsync(p => p.Id == id);
                if (paciente != null)
                    return (paciente, "Paciente obtenido correctamente", HttpStatusCode.OK);
                else
                    return (null, $"No se encontró ningun paciente con el ID {id}", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return (null, $"Error al obtener el paciente: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public async Task<(IEnumerable<Paciente> pacientes, string mensaje, HttpStatusCode statusCode)> GetAllDeleted()
        {
            throw new NotImplementedException();
        }

        public async Task<(Paciente paciente, string mensaje, HttpStatusCode statusCode)> Update(int id, PacienteDTO pacienteDTO)
        {
            throw new NotImplementedException();
        }
    }
}