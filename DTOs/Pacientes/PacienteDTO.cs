

namespace simulacro2.DTOs.Pacientes
{
    public class PacienteDTO
    {
        public required string? Nombres { get; set; }
        public required string? Apellidos { get; set; }
        public required DateTime FechaNacimiento { get; set; }
        public required string? Correo { get; set; }
        public required string? Telefono { get; set; }
        public required string? Direccion { get; set; }
        public required string? Estado { get; set; }
    }
}