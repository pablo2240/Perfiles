using PerfilSena.WEB.Models;

namespace PerfilSena.WEB.Services
{
    public interface IPabloReyesService
    {
        Task<List<Pabloreyes>> ObtenerTodosAsync();
        Task<Pabloreyes?> ObtenerPorIdAsync(int id);
        Task<Pabloreyes?> CrearAsync(Pabloreyes pabloreyes, byte[]? imagenBytes, string? imagenNombre);
        Task<bool> EliminarAsync(int id);
    }
}