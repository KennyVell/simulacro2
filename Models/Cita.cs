using System.Text.Json.Serialization;

namespace simulacro2.Models
{
    public class Cita
    {
        public int Id { get; set; }
        public required int MedicoId { get; set; }
        public required int PacienteId { get; set; }
        public required DateTime Fecha { get; set; }
        public required string? Estado { get; set; }
        public Medico? Medico { get; set; }
        public Paciente? Paciente { get; set; }
        [JsonIgnore]
        public List<Tratamiento>? Tratamientos { get; set; }
    }
}