

namespace simulacro2.DTOs.Citas
{
    public class CitaCreateDTO
    {
        public DateTime? Fecha { get; set; }
        public int? MedicoId { get; set; }
        public int? PacienteId { get; set; }
    }
}