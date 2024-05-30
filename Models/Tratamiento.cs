

namespace simulacro2.Models
{
    public class Tratamiento
    {
        public int Id { get; set; }
        public required int CitaId { get; set; }
        public required string? Descripcion { get; set; }
        public required string? Estado { get; set; }
        public Cita? Cita { get; set; }
    }
}