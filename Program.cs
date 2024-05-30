using Microsoft.EntityFrameworkCore;
using simulacro2.Data;
using simulacro2.Services.Citas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexion a la base de datos
builder.Services.AddDbContext<ClinicaContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MyConnection"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql"))
);

// Servicio de los controladores
builder.Services.AddControllers();

builder.Services.AddScoped<ICitasRepository, CitasRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Mapeo de los controladores
app.MapControllers();

app.Run();
