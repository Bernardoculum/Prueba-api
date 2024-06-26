// Importando las libresias 
using Microsoft.EntityFrameworkCore;

// Aqui tenemos el dbAPI context
using Probando_api.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBAPIContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Conexion")));


// Convertimos el json, para ignorar esa referencia ciclica
builder.Services.AddControllers().AddJsonOptions(opt =>
{

    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // probando
    app.UseSwaggerUI();
    // probando en cotrolladores
}

app.UseAuthorization();

app.MapControllers();

app.Run();
