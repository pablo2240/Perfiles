using PerfilSena.API.Models;

namespace PerfilSena.API.Services
{
    public interface IComentarioService
    {
        Task<List<Comentario>> ObtenerConversacionAsync(int pabloReyesId1, int pabloReyesId2);
        Task<Comentario> CrearAsync(Comentario comentario);
        Task<List<Comentario>> ObtenerPorPabloReyesAsync(int pabloReyesId);
    }
}