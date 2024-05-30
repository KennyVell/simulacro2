using Microsoft.EntityFrameworkCore;
using simulacro2.Models;

namespace simulacro2.Data
{
    public class ClinicaContext : DbContext
    {
        public ClinicaContext(DbContextOptions<ClinicaContext> options) : base(options){}

        public DbSet<Cita> Citas { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }
    }
}