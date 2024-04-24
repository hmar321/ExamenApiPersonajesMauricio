using ExamenApiPersonajesMauricio.Data;
using ExamenApiPersonajesMauricio.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Personajes Examen Mauricio",
        Description = "Primer examen de azure",
    });
});
builder.Services.AddTransient<RepositoryPersonajes>();
string connectionString = builder.Configuration.GetConnectionString("SqlServerAzure");
builder.Services.AddDbContext<PersonajesContext>(options => options.UseSqlServer(connectionString));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Api Azure Crud Personajes v1");
    options.RoutePrefix = "";
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
