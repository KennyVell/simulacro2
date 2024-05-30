using Microsoft.EntityFrameworkCore;
using System.Net;
using simulacro2.Data;
using simulacro2.Models;
using simulacro2.DTOs.Citas;

namespace simulacro2.Services.Citas
{
    public class CitasRepository : ICitasRepository
    {
        public readonly ClinicaContext _context;
        public CitasRepository(ClinicaContext context)
        {
            _context = context;
        }

        public async Task<(Cita cita, string mensaje, HttpStatusCode statusCode)> Add(CitaDTO citaDTO)
        {
            try
            {
                var cita = new Cita {
                    Fecha = citaDTO.Fecha,
                    Estado = citaDTO.Estado,
                    MedicoId = citaDTO.MedicoId,
                    PacienteId = citaDTO.PacienteId
                };
                
                await _context.Citas.AddAsync(cita);
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
                var citas = await _context.Citas.Include(c => c.Paciente).Include(c => c.Medico.Especialidad)
                .Where(c => c.Estado.ToLower() != "cancelada").ToListAsync();
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
                var cita = await _context.Citas.Include(c => c.Medico).Include(c => c.Paciente).Include(c => c.Medico.Especialidad).FirstOrDefaultAsync(c => c.Id == id);
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

        public async Task<(Cita cita, string mensaje, HttpStatusCode statusCode)> Update(CitaDTO citaDTO)
        {
            try
            {
                var cita = new Cita {
                    Fecha = citaDTO.Fecha,
                    Estado = citaDTO.Estado,
                    MedicoId = citaDTO.MedicoId,
                    PacienteId = citaDTO.PacienteId
                };

                _context.Entry(cita).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return (cita, "Cita actualizada correctamente", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return (null, $"Error al actualizar la cita: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public void Delete(int id)
        {
            var cita = _context.Citas.Find(id);            
            cita.Estado = "cancelada";
            _context.Entry(cita).State = EntityState.Modified;
            _context.SaveChanges();                
        }
                
        public async Task<(IEnumerable<Cita> citas, string mensaje, HttpStatusCode statusCode)> GetCanceladas()
        {
            try
            {
                var citas = await _context.Citas.Include(c => c.Paciente).Include(c => c.Medico.Especialidad)
                .Where(c => c.Estado.ToLower() == "cancelada").ToListAsync();
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

    }
}