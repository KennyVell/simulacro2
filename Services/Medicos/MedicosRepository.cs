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
        public async Task<(Medico medico, string mensaje, HttpStatusCode statusCode)> Add(MedicoDTO medico)
        {
            try
            {
                if (medico.NombreCompleto == null || !medico.EspecialidadId.HasValue || medico.Correo == null || medico.Telefono == null)
                {
                    return (null, "Todos los campos son obligatorios.", HttpStatusCode.BadRequest);
                }

                var nuevoMedico = new Medico
                {
                    NombreCompleto = medico.NombreCompleto,
                    Correo = medico.Correo,
                    Telefono = medico.Telefono,
                    Estado = "activo",
                    EspecialidadId = medico.EspecialidadId.Value,
                };

                await _context.Medicos.AddAsync(nuevoMedico);
                await _context.SaveChangesAsync();
                return (nuevoMedico, "Medico creado correctamente", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return (null, $"Error al crear la medico: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public void Delete(int id)
        {
            var medico = _context.Medicos.Find(id);            
            medico.Estado = "inactivo";
            _context.Entry(medico).State = EntityState.Modified;
            _context.SaveChanges();
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

        public async Task<(IEnumerable<Medico> medicos, string mensaje, HttpStatusCode statusCode)> GetAllDeleted()
        {
            try
            {
                var medicos = await _context.Medicos.Include(m => m.Especialidad).Include(m => m.Citas)
                .Where(m => m.Estado.ToLower() == "inactivo").ToListAsync();
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

        public async Task<(Medico medico, string mensaje, HttpStatusCode statusCode)> Update(int id, MedicoDTO medicoDTO)
        {
            try
            {
                var medico = await _context.Medicos.FindAsync(id);
                if (medico == null)
                {
                    return (null, "Medico no encontrado", HttpStatusCode.NotFound);
                }

                // Actualiza los campos necesarios solo si no son nulos
                if (!string.IsNullOrEmpty(medicoDTO.NombreCompleto))
                {
                    medico.NombreCompleto = medicoDTO.NombreCompleto;
                }
                if (!string.IsNullOrEmpty(medicoDTO.Telefono))
                {
                    medico.Telefono = medicoDTO.Telefono;
                }
                if (!string.IsNullOrEmpty(medicoDTO.Correo))
                {
                    medico.Correo = medicoDTO.Correo;
                }
                if (medicoDTO.EspecialidadId.HasValue && medicoDTO.EspecialidadId != 0) 
                {
                    medico.EspecialidadId = medicoDTO.EspecialidadId.Value;
                }
                

                _context.Entry(medico).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return (medico, "Medico actualizado correctamente", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return (null, $"Error al actualizar el medico, los datos especificados son incorrectos: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public async Task<(Medico medico, string mensaje, HttpStatusCode statusCode)> Restore(int id)
        {
            try
            {
                var medico = await _context.Medicos.FindAsync(id);

                if (medico == null)
                {
                    return (null, "El Medico no existe", HttpStatusCode.NotFound);
                }

                // Realizar la lógica para restaurar el Medico, por ejemplo, cambiar el estado a "activo"
                medico.Estado = "activo";
                _context.Medicos.Update(medico);
                await _context.SaveChangesAsync();

                return (medico, "Medico restaurado correctamente", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return (null, $"Error al restaurar el Medico: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
    }
}