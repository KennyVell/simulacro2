

namespace simulacro2.DTOs.Medicos
{
    public class MedicoDTO
    {
        public required string? NombreCompleto { get; set; }
        public required int EspecialidadId { get; set; }
        public required string? Correo { get; set; }
        public required string? Telefono { get; set; }
        public required string? Estado { get; set; }
    }
}