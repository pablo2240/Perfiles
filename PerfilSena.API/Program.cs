using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PerfilSena.API.Data;
using PerfilSena.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPabloReyesService, PabloReyesService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();

// CORS - Permitir todo en desarrollo
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ORDEN CORRECTO
app.UseCors("AllowAll");

// Servir archivos estáticos desde wwwroot
app.UseStaticFiles();

// TAMBIÉN servir archivos desde wwwroot/img específicamente
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.WebRootPath, "img")),
    RequestPath = "/img"
});

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("🚀 Backend iniciado");
logger.LogInformation("📁 Archivos estáticos en: {Path}", builder.Environment.WebRootPath);
logger.LogInformation("🖼️ Imágenes en: {Path}", Path.Combine(builder.Environment.WebRootPath, "img"));

app.Run();