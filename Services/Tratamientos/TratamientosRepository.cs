using Microsoft.EntityFrameworkCore;
using System.Net;
using simulacro2.Data;
using simulacro2.Models;
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

        public async Task<(Tratamiento tratamiento, string mensaje, HttpStatusCode statusCode)> Add(TratamientoDTO tratamiento)
        {
            try
            {
                if (tratamiento.Descripcion == null || !tratamiento.CitaId.HasValue)
                {
                    return (null, "Todos los campos son obligatorios.", HttpStatusCode.BadRequest);
                }

                if (tratamiento.CitaId.Value == 0) 
                {
                    return (null, "Los campos ingresados son invalidos", HttpStatusCode.BadRequest);
                }

                var nuevoTratamiento = new Tratamiento
                {
                    Descripcion = tratamiento.Descripcion,                    
                    CitaId = tratamiento.CitaId.Value,
                    Estado = "activo",
                };

                await _context.Tratamientos.AddAsync(nuevoTratamiento);
                await _context.SaveChangesAsync();
                return (nuevoTratamiento, "Tratamiento creado correctamente", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return (null, $"Error al crear la tratamiento: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public void Delete(int id)
        {
            var tratamiento = _context.Tratamientos.Find(id);            
            tratamiento.Estado = "inactivo";
            _context.Entry(tratamiento).State = EntityState.Modified;
            _context.SaveChanges();
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

        public async Task<(IEnumerable<Tratamiento> tratamientos, string mensaje, HttpStatusCode statusCode)> GetAllDeleted()
        {
            try
            {
                var tratamientos = await _context.Tratamientos.Include(t => t.Cita.Medico.Especialidad)
                .Include(t => t.Cita.Paciente).Where(t => t.Estado.ToLower() == "inactivo").ToListAsync();
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

        public async Task<(Tratamiento tratamiento, string mensaje, HttpStatusCode statusCode)> Update(int id, TratamientoDTO tratamientoDTO)
        {
            try
            {
                var tratamiento = await _context.Tratamientos.FindAsync(id);
                if (tratamiento == null)
                {
                    return (null, "Tratamiento no encontrado", HttpStatusCode.NotFound);
                }

                // Actualiza los campos necesarios solo si no son nulos
                if (!string.IsNullOrEmpty(tratamientoDTO.Descripcion))
                {
                    tratamiento.Descripcion = tratamientoDTO.Descripcion;
                }
                if (tratamientoDTO.CitaId.HasValue && tratamientoDTO.CitaId != 0) 
                {
                    tratamiento.CitaId = tratamientoDTO.CitaId.Value;
                }
                

                _context.Entry(tratamiento).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return (tratamiento, "Tratamiento actualizado correctamente", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return (null, $"Error al actualizar el tratamiento, los datos especificados son incorrectos: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public async Task<(Tratamiento tratamiento, string mensaje, HttpStatusCode statusCode)> Restore(int id)
        {
            try
            {
                var tratamiento = await _context.Tratamientos.FindAsync(id);

                if (tratamiento == null)
                {
                    return (null, "El Tratamiento no existe", HttpStatusCode.NotFound);
                }

                // Realizar la lógica para restaurar el Tratamiento, por ejemplo, cambiar el estado a "activo"
                tratamiento.Estado = "activo";
                _context.Tratamientos.Update(tratamiento);
                await _context.SaveChangesAsync();

                return (tratamiento, "Tratamiento restaurado correctamente", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return (null, $"Error al restaurar el Tratamiento: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
    }
}