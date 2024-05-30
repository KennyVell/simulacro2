using Microsoft.EntityFrameworkCore;
using System.Net;
using simulacro2.Data;
using simulacro2.Models;

namespace simulacro2.Services.Citas
{
    public class CitasRepository : ICitasRepository
    {
        public readonly ClinicaContext _context;
        public CitasRepository(ClinicaContext context)
        {
            _context = context;
        }

        public async Task<(Cita cita, string mensaje, HttpStatusCode statusCode)> Add(Cita cita)
        {
            try
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return (cita, "Cita agregada correctamente", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return (null, $"Error al agregar la cita: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public async Task<(IEnumerable<Cita> citas, string mensaje, HttpStatusCode statusCode)> GetAll()
        {
            try
            {
                var citas = await _context.Citas.ToListAsync();
                if (citas.Any())
                    return (citas, "Citas obtenidas correctamente", HttpStatusCode.OK);
                else
                    return (null, "No se encontraron citas", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return (null, $"Error al obtener las citas: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public async Task<(Cita cita, string mensaje, HttpStatusCode statusCode)> GetById(int id)
        {
            try
            {
                var cita = await _context.Citas.FindAsync(id);
                if (cita != null)
                    return (cita, "Cita obtenida correctamente", HttpStatusCode.OK);
                else
                    return (null, $"No se encontró ninguna cita con el ID {id}", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return (null, $"Error al obtener la cita: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public async Task<(Cita cita, string mensaje, HttpStatusCode statusCode)> Update(Cita cita)
        {
            try
            {
                _context.Entry(cita).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return (cita, "Cita actualizada correctamente", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return (null, $"Error al actualizar la cita: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

    }
}