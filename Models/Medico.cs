using System.Text.Json.Serialization;

namespace simulacro2.Models
{
    public class Medico
    {
        public int Id { get; set; }
        public required string? NombreCompleto { get; set; }
        public required int EspecialidadId { get; set; }
        public required string? Correo { get; set; }
        public required string? Telefono { get; set; }
        public required string? Estado { get; set; }
        public Especialidad? Especialidad { get; set; }
        [JsonIgnore]
        public List<Cita>? Citas { get; set; }
    }
}