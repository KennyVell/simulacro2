using Microsoft.EntityFrameworkCore;
using simulacro2.Data;
using simulacro2.Services.Citas;
using simulacro2.Services.Especialidades;
using simulacro2.Services.Medicos;
using simulacro2.Services.Pacientes;
using simulacro2.Services.Tratamientos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexión a la base de datos con resiliencia a errores transitorios
builder.Services.AddDbContext<ClinicaContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MyConnection"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql"),
    mySqlOptions => mySqlOptions.EnableRetryOnFailure())
);

// Servicio de los controladores
builder.Services.AddControllers();

builder.Services.AddScoped<ICitasRepository, CitasRepository>();
builder.Services.AddScoped<IEspecialidadesRepository, EspecialidadesRepository>();
builder.Services.AddScoped<IMedicosRepository, MedicosRepository>();
builder.Services.AddScoped<IPacientesRepository, PacientesRepository>();
builder.Services.AddScoped<ITratamientosRepository, TratamientosRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Mapeo de los controladores
app.UseRouting();
app.MapControllers();

app.Run();
