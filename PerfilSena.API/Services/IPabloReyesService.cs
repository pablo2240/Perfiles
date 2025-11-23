using PerfilSena.API.Models;

namespace PerfilSena.API.Services
{
    public interface IPabloReyesService
    {
        Task<List<PabloReyes>> ObtenerTodosAsync();
        Task<PabloReyes?> ObtenerPorIdAsync(int id);
        Task<PabloReyes> CrearAsync(PabloReyes pabloReyes, IFormFile? imagen);
        Task<bool> ActualizarAsync(PabloReyes pabloReyes, IFormFile? imagen);
        Task<bool> EliminarAsync(int id);
    }
}