using System.Text.Json.Serialization;

namespace simulacro2.Models
{
    public class Especialidad
    {
        public int Id { get; set; }
        public required string? Nombre { get; set; }
        public required string? Descripcion { get; set; }
        public required string? Estado { get; set; }
        [JsonIgnore]
        public ICollection<Medico>? Medicos { get; set; }
    }
}