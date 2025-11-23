using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PerfilSena.WEB;
using PerfilSena.WEB.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// IMPORTANTE: Usar el puerto correcto del backend (5134)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5134/")  // Cambiar a HTTP y puerto 5134
});

builder.Services.AddScoped<IPabloReyesService, PabloReyesService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();

// Log de configuración
Console.WriteLine($"🌐 Frontend configurado para conectar a: http://localhost:5134/");

await builder.Build().RunAsync();