using System.Net.Http.Json;
using PerfilSena.WEB.Models;

namespace PerfilSena.WEB.Services
{
    public class ComentarioService : IComentarioService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/comentario";  // URL relativa

        public ComentarioService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Comentario>> ObtenerConversacionAsync(int pabloReyesId1, int pabloReyesId2)
        {
            try
            {
                Console.WriteLine($"📡 GET conversación: {pabloReyesId1} <-> {pabloReyesId2}");
                return await _http.GetFromJsonAsync<List<Comentario>>($"{BaseUrl}/conversacion/{pabloReyesId1}/{pabloReyesId2}")
                       ?? new List<Comentario>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener conversación: {ex.Message}");
                return new List<Comentario>();
            }
        }

        public async Task<Comentario?> EnviarComentarioAsync(Comentario comentario)
        {
            try
            {
                Console.WriteLine($"📤 POST comentario de {comentario.PabloReyesEmisorId} a {comentario.PabloReyesReceptorId}");
                var response = await _http.PostAsJsonAsync(BaseUrl, comentario);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ Comentario enviado");
                    return await response.Content.ReadFromJsonAsync<Comentario>();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al enviar comentario: {ex.Message}");
                return null;
            }
        }
    }
}