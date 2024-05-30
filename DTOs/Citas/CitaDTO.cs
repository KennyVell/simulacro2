

namespace simulacro2.DTOs.Citas
{
    public class CitaDTO
    {
        public required DateTime Fecha { get; set; }
        public required string? Estado { get; set; }
        public required int MedicoId { get; set; }
        public required int PacienteId { get; set; }
    }
}