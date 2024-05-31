

namespace simulacro2.DTOs.Citas
{
    public class CitaDTO
    {
        public DateTime? Fecha { get; set; }
        public string? EstadoCita { get; set; }
        public int? MedicoId { get; set; }
        public int? PacienteId { get; set; }
    }
}