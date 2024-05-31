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
        
        public async Task<(Especialidad especialidad, string mensaje, HttpStatusCode statusCode)> Add(EspecialidadDTO especialidad)
        {
            try
            {
                if (especialidad.Nombre == null || especialidad.Descripcion == null)
                {
                    return (null, "Todos los campos son obligatorios.", HttpStatusCode.BadRequest);
                }

                var nuevaEspecialidad = new Especialidad
                {
                    Nombre = especialidad.Nombre,
                    Descripcion = especialidad.Descripcion,
                    Estado = "activo",
                };

                await _context.Especialidades.AddAsync(nuevaEspecialidad);
                await _context.SaveChangesAsync();
                return (nuevaEspecialidad, "Especialidad creada correctamente", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return (null, $"Error al crear la especialidad: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public async Task<(IEnumerable<Especialidad> especialidades, string mensaje, HttpStatusCode statusCode)> GetAll()
        {
            try
            {
                var especialidades = await _context.Especialidades.ToListAsync();
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
                var especialidad = await _context.Especialidades.FirstOrDefaultAsync(e => e.Id == id);
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

        public async Task<(Especialidad especialidad, string mensaje, HttpStatusCode statusCode)> Update(int id, EspecialidadDTO especialidadDTO)
        {
            try
            {
                var especialidad = await _context.Especialidades.FindAsync(id);
                if (especialidad == null)
                {
                    return (null, "Especialidad no encontrada", HttpStatusCode.NotFound);
                }

                // Actualiza los campos necesarios solo si no son nulos
                if (!string.IsNullOrEmpty(especialidadDTO.Nombre))
                {
                    especialidad.Nombre = especialidadDTO.Nombre;
                }
                if (!string.IsNullOrEmpty(especialidadDTO.Descripcion))
                {
                    especialidad.Descripcion = especialidadDTO.Descripcion;
                }

                _context.Entry(especialidad).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return (especialidad, "Especialidad actualizada correctamente", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return (null, $"Error al actualizar la especialidad, los datos especificados son incorrectos: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public async Task Delete(int id)
        {
            var especialidad = await _context.Especialidades.FindAsync(id);            
            especialidad.Estado = "inactivo";
            _context.Entry(especialidad).State = EntityState.Modified;
            await _context.SaveChangesAsync(); 
        }
        
        public async Task<(IEnumerable<Especialidad> especialidades, string mensaje, HttpStatusCode statusCode)> GetAllDeleted()
        {
            try
            {
                var especialidades = await _context.Especialidades.IgnoreQueryFilters().Where(e => e.Estado.ToLower() == "inactivo").ToListAsync();
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

        public async Task<(Especialidad especialidad, string mensaje, HttpStatusCode statusCode)> Restore(int id)
        {
            try
            {
                var especialidad = await _context.Especialidades.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Id == id);

                if (especialidad == null)
                {
                    return (null, "La especialidad no existe", HttpStatusCode.NotFound);
                }

                // Realizar la lógica para restaurar la especialidad, por ejemplo, cambiar el estado a "activo"
                especialidad.Estado = "activo";
                _context.Especialidades.Update(especialidad);
                await _context.SaveChangesAsync();

                return (especialidad, "Especialidad restaurada correctamente", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return (null, $"Error al restaurar la especialidad: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

    }
}