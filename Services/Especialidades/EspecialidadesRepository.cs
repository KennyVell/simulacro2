using Microsoft.EntityFrameworkCore;
using System.Net;
using simulacro2.Data;
using simulacro2.Models;
using simulacro2.DTOs.Especialidades;

namespace simulacro2.Services.Especialidades
{
    public class EspecialidadesRepository : IEspecialidadesRepository
    {
        public readonly ClinicaContext _context;
        public EspecialidadesRepository(ClinicaContext context)
        {
            _context = context;
        }
        
        public Task<(Especialidad especialidad, string mensaje, HttpStatusCode statusCode)> Add(EspecialidadDTO especialidadDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<(IEnumerable<Especialidad> especialidades, string mensaje, HttpStatusCode statusCode)> GetAll()
        {
            try
            {
                var especialidades = await _context.Especialidades.Include(e => e.Medicos).Where(e => e.Estado.ToLower() == "activo").ToListAsync();
                if (especialidades.Any())
                    return (especialidades, "especialidades obtenidas correctamente", HttpStatusCode.OK);
                else
                    return (null, "No se encontraron especialidades", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return (null, $"Error al obtener las especialidades: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public async Task<(Especialidad especialidad, string mensaje, HttpStatusCode statusCode)> GetById(int id)
        {
            try
            {
                var especialidad = await _context.Especialidades.Include(e => e.Medicos).FirstOrDefaultAsync(e => e.Id == id);
                if (especialidad != null)
                    return (especialidad, "Especialidad obtenida correctamente", HttpStatusCode.OK);
                else
                    return (null, $"No se encontró ninguna especialidad con el ID {id}", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return (null, $"Error al obtener la especialidad: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public Task<(Especialidad especialidad, string mensaje, HttpStatusCode statusCode)> Update(EspecialidadDTO especialidad)
        {
            throw new NotImplementedException();
        }
    }
}