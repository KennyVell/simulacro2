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
        public async Task<(Paciente paciente, string mensaje, HttpStatusCode statusCode)> Add(PacienteDTO paciente)
        {
            try
            {
                if (paciente.Nombres == null || paciente.Apellidos == null || paciente.Correo == null || paciente.Telefono == null 
                || paciente.FechaNacimiento == null || paciente.Direccion == null)
                {
                    return (null, "Todos los campos son obligatorios.", HttpStatusCode.BadRequest);
                }

                var nuevoPaciente = new Paciente
                {
                    Nombres = paciente.Nombres,
                    Apellidos = paciente.Apellidos,
                    FechaNacimiento = paciente.FechaNacimiento.Value,
                    Correo = paciente.Correo,
                    Telefono = paciente.Telefono,
                    Direccion = paciente.Direccion,
                    Estado = "activo",
                };

                await _context.Pacientes.AddAsync(nuevoPaciente);
                await _context.SaveChangesAsync();
                return (nuevoPaciente, "Paciente creado correctamente", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return (null, $"Error al crear la paciente: {ex.Message}", HttpStatusCode.BadRequest);
            }
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
                var pacientes = await _context.Pacientes.Where(p => p.Estado.ToLower() == "activo").ToListAsync();
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
                var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == id);
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
            try
            {
                var pacientes = await _context.Pacientes.Where(p => p.Estado.ToLower() == "inactivo").ToListAsync();
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

        public async Task<(Paciente paciente, string mensaje, HttpStatusCode statusCode)> Update(int id, PacienteDTO PacienteDTO)
        {
            try
            {
                var Paciente = await _context.Pacientes.FindAsync(id);
                if (Paciente == null)
                {
                    return (null, "Paciente no encontrado", HttpStatusCode.NotFound);
                }

                // Actualiza los campos necesarios solo si no son nulos
                
                if (!string.IsNullOrEmpty(PacienteDTO.Nombres))
                {
                    Paciente.Nombres = PacienteDTO.Nombres;
                }
                if (!string.IsNullOrEmpty(PacienteDTO.Apellidos))
                {
                    Paciente.Apellidos = PacienteDTO.Apellidos;
                }
                if (!string.IsNullOrEmpty(PacienteDTO.Telefono))
                {
                    Paciente.Telefono = PacienteDTO.Telefono;
                }
                if (!string.IsNullOrEmpty(PacienteDTO.Correo))
                {
                    Paciente.Correo = PacienteDTO.Correo;
                }
                if (!string.IsNullOrEmpty(PacienteDTO.Direccion))
                {
                    Paciente.Direccion = PacienteDTO.Direccion;
                }
                if (PacienteDTO.FechaNacimiento != null)
                {
                    Paciente.FechaNacimiento = PacienteDTO.FechaNacimiento.Value;
                }
                

                _context.Entry(Paciente).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return (Paciente, "Paciente actualizado correctamente", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return (null, $"Error al actualizar el Paciente, los datos especificados son incorrectos: {ex.Message}", HttpStatusCode.BadRequest);
            }
        }

        public async Task<(Paciente paciente, string mensaje, HttpStatusCode statusCode)> Restore(int id)
        {
            try
            {
                var paciente = await _context.Pacientes.FindAsync(id);

                if (paciente == null)
                {
                    return (null, "El Paciente no existe", HttpStatusCode.NotFound);
                }

                // Realizar la lógica para restaurar el Paciente, por ejemplo, cambiar el estado a "activo"
                paciente.Estado = "activo";
                _context.Pacientes.Update(paciente);
                await _context.SaveChangesAsync();

                return (paciente, "Paciente restaurado correctamente", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return (null, $"Error al restaurar el Paciente: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
    }
}