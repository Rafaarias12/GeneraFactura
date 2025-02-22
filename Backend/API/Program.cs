using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Model.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var file = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Api.xml");
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();

builder.Services.AddScoped<IFacturaService, FacturaService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mi API",
        Version = "v1",
        Description = "API para gestionar Facturas",
        Contact = new OpenApiContact
        {
            Name = "Rafael Arias",
            Email = "rafa4562009@hotmail.com",
        }
    });

    var filePath = Path.Combine(AppContext.BaseDirectory, "Api.xml");
    if (System.IO.File.Exists(filePath))
    {
        c.IncludeXmlComments(filePath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
    c.DocumentTitle = "Documentación de mi API";
    //c.RoutePrefix = string.Empty; // Para que Swagger se abra en la raíz
});

app.UseHttpsRedirection();

app.Urls.Add($"http://*:{port}");

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.Run();
