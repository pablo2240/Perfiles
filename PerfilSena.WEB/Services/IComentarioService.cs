using PerfilSena.WEB.Models;

namespace PerfilSena.WEB.Services
{
    public interface IComentarioService
    {
        Task<List<Comentario>> ObtenerConversacionAsync(int pabloReyesId1, int pabloReyesId2);
        Task<Comentario?> EnviarComentarioAsync(Comentario comentario);
    }
}