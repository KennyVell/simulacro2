

namespace simulacro2.DTOs.Pacientes
{
    public class PacienteDTO
    {
        public int Id { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Estado { get; set; }
    }
}