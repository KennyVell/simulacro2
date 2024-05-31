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

        public async Task<(Cita cita, string mensaje, HttpStatusCode statusCode)> Add(CitaCreateDTO cita)
        {
            try
            {
                if (cita.Fecha == null || !cita.MedicoId.HasValue || !cita.PacienteId.HasValue)
                {
                    return (null, "Todos los campos son obligatorios.", HttpStatusCode.BadRequest);
                }

                if (cita.MedicoId.Value == 0 || cita.PacienteId.Value == 0) 
                {
                    return (null, "Los campos ingresados son invalidos", HttpStatusCode.BadRequest);
                }

                var nuevaCita = new Cita
                {
                    Fecha = cita.Fecha.Value,
                    Estado = "activo",
                    EstadoCita = "programada",
                    MedicoId = cita.MedicoId.Value,
                    PacienteId = cita.PacienteId.Value
                };

                await _context.Citas.AddAsync(nuevaCita);
                await _context.SaveChangesAsync();
                return (nuevaCita, "Cita creada correctamente", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return (null, $"Error al crear la cita: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public async Task<(IEnumerable<Cita> citas, string mensaje, HttpStatusCode statusCode)> GetAll()
        {
            try
            {
                var citas = await _context.Citas.Include(c => c.Paciente).Include(c => c.Medico.Especialidad)
                .Where(c => c.Estado.ToLower() == "activo").ToListAsync();
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

        public async Task<(Cita cita, string mensaje, HttpStatusCode statusCode)> Update(int id, CitaDTO citaDTO)
        {
            try
            {
                var cita = await _context.Citas.FindAsync(id);
                if (cita == null)
                {
                    return (null, "Cita no encontrada", HttpStatusCode.NotFound);
                }

                // Actualiza los campos necesarios solo si no son nulos
                if (citaDTO.Fecha != null)
                {
                    cita.Fecha = citaDTO.Fecha.Value;
                }
                if (!string.IsNullOrEmpty(citaDTO.EstadoCita))
                {
                    cita.EstadoCita = citaDTO.EstadoCita;
                }
                if (citaDTO.MedicoId.HasValue && citaDTO.MedicoId != 0) 
                {
                    cita.MedicoId = citaDTO.MedicoId.Value;
                }
                if (citaDTO.PacienteId.HasValue && citaDTO.MedicoId != 0)
                {
                    cita.PacienteId = citaDTO.PacienteId.Value;
                }

                _context.Entry(cita).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return (cita, "Cita actualizada correctamente", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return (null, $"Error al actualizar la cita, los datos especificados son incorrectos: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public void Delete(int id)
        {
            var cita = _context.Citas.Find(id);
            cita.Estado = "inactivo";
            _context.Entry(cita).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<(IEnumerable<Cita> citas, string mensaje, HttpStatusCode statusCode)> GetAllDeleted()
        {
            try
            {
                var citas = await _context.Citas.Include(c => c.Paciente).Include(c => c.Medico.Especialidad)
                .Where(c => c.Estado.ToLower() == "inactivo").ToListAsync();
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

        public async Task<(Cita cita, string mensaje, HttpStatusCode statusCode)> Restore(int id)
        {
            try
            {
                var cita = await _context.Citas.FindAsync(id);

                if (cita == null)
                {
                    return (null, "La cita no existe", HttpStatusCode.NotFound);
                }

                // Realizar la lógica para restaurar la cita, por ejemplo, cambiar el estado a "activo"
                cita.Estado = "activo";
                _context.Citas.Update(cita);
                await _context.SaveChangesAsync();

                return (cita, "Cita restaurada correctamente", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return (null, $"Error al restaurar la cita: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
    }
}