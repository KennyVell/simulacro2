using Microsoft.EntityFrameworkCore;
using System.Net;
using simulacro2.Data;
using simulacro2.Models;
using simulacro2.DTOs.Tratamientos;
using simulacro2.DTOs.Tratamientos;

namespace simulacro2.Services.Tratamientos
{
    public class TratamientosRepository : ITratamientosRepository
    {
        private readonly ClinicaContext _context;
        public TratamientosRepository(ClinicaContext context)
        {
            _context = context;
        }

        public Task<(Tratamiento tratamiento, string mensaje, HttpStatusCode statusCode)> Add(TratamientoDTO tratamientoDTO)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<(IEnumerable<Tratamiento> tratamientos, string mensaje, HttpStatusCode statusCode)> GetAll()
        {
            try
            {
                var tratamientos = await _context.Tratamientos.Include(t => t.Cita.Medico.Especialidad)
                .Include(t => t.Cita.Paciente).Where(t => t.Estado.ToLower() == "activo").ToListAsync();
                if (tratamientos.Any())
                    return (tratamientos, "Tratamientos obtenidos correctamente", HttpStatusCode.OK);
                else
                    return (null, "No se encontraron tratamientos", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return (null, $"Error al obtener los tratamientos: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public async Task<(Tratamiento tratamiento, string mensaje, HttpStatusCode statusCode)> GetById(int id)
        {
            try
            {
                var tratamiento = await _context.Tratamientos.Include(t => t.Cita).Include(t => t.Cita.Medico).Include(t => t.Cita.Medico.Especialidad).Include(t => t.Cita.Paciente).FirstOrDefaultAsync(t => t.Id == id);
                if (tratamiento != null)
                    return (tratamiento, "Tratamiento obtenido correctamente", HttpStatusCode.OK);
                else
                    return (null, $"No se encontró ningun tratamiento con el ID {id}", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return (null, $"Error al obtener el tratamiento: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public Task<(IEnumerable<Tratamiento> tratamientos, string mensaje, HttpStatusCode statusCode)> GetDelete()
        {
            throw new NotImplementedException();
        }

        public Task<(Tratamiento tratamiento, string mensaje, HttpStatusCode statusCode)> Update(TratamientoDTO tratamientoDTO)
        {
            throw new NotImplementedException();
        }
    }
}