

namespace simulacro2.DTOs.Medicos
{
    public class MedicoDTO
    {
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
        public int EspecialidadId { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Estado { get; set; }
    }
}