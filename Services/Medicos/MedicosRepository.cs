using Microsoft.EntityFrameworkCore;
using System.Net;
using simulacro2.Data;
using simulacro2.Models;
using simulacro2.DTOs.Medicos;

namespace simulacro2.Services.Medicos
{
    public class MedicosRepository : IMedicosRepository
    {
        private readonly ClinicaContext _context;
        public MedicosRepository(ClinicaContext context)
        {
            _context = context;
        }
        public Task<(Medico medico, string mensaje, HttpStatusCode statusCode)> Add(MedicoDTO medicoDTO)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<(IEnumerable<Medico> medicos, string mensaje, HttpStatusCode statusCode)> GetAll()
        {
            try
            {
                var medicos = await _context.Medicos.Include(m => m.Especialidad).Include(m => m.Citas)
                .Where(m => m.Estado.ToLower() == "activo").ToListAsync();
                if (medicos.Any())
                    return (medicos, "Medicos obtenidos correctamente", HttpStatusCode.OK);
                else
                    return (null, "No se encontraron medicos", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return (null, $"Error al obtener los medicos: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public async Task<(Medico medico, string mensaje, HttpStatusCode statusCode)> GetById(int id)
        {
            try
            {
                var medico = await _context.Medicos.Include(m => m.Especialidad).Include(m => m.Citas).FirstOrDefaultAsync(c => c.Id == id);
                if (medico != null)
                    return (medico, "Medico obtenido correctamente", HttpStatusCode.OK);
                else
                    return (null, $"No se encontró ningun medico con el ID {id}", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return (null, $"Error al obtener el medico: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public Task<(IEnumerable<Medico> medicos, string mensaje, HttpStatusCode statusCode)> GetDelete()
        {
            throw new NotImplementedException();
        }

        public Task<(Medico medico, string mensaje, HttpStatusCode statusCode)> Update(MedicoDTO medicoDTO)
        {
            throw new NotImplementedException();
        }
    }
}