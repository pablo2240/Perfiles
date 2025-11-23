using System.Net.Http.Json;
using PerfilSena.WEB.Models;

namespace PerfilSena.WEB.Services
{
    public class PabloReyesService : IPabloReyesService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "api/pabloreyes";  // URL relativa

        public PabloReyesService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Pabloreyes>> ObtenerTodosAsync()
        {
            try
            {
                var url = BaseUrl;
                Console.WriteLine($"📡 GET {_http.BaseAddress}{url}");
                var result = await _http.GetFromJsonAsync<List<Pabloreyes>>(url);
                Console.WriteLine($"✅ Perfiles obtenidos: {result?.Count ?? 0}");
                return result ?? new List<Pabloreyes>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener perfiles: {ex.Message}");
                return new List<Pabloreyes>();
            }
        }

        public async Task<Pabloreyes?> ObtenerPorIdAsync(int id)
        {
            try
            {
                return await _http.GetFromJsonAsync<Pabloreyes>($"{BaseUrl}/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener perfil {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<Pabloreyes?> CrearAsync(Pabloreyes pabloreyes, byte[]? imagenBytes, string? imagenNombre)
        {
            try
            {
                using var content = new MultipartFormDataContent();
                content.Add(new StringContent(pabloreyes.Nombre), "nombre");
                content.Add(new StringContent(pabloreyes.Telefono), "telefono");
                content.Add(new StringContent(pabloreyes.Direccion), "direccion");

                if (imagenBytes != null && imagenNombre != null)
                {
                    content.Add(new ByteArrayContent(imagenBytes), "imagen", imagenNombre);
                }

                Console.WriteLine($"📤 POST {_http.BaseAddress}{BaseUrl}");
                var response = await _http.PostAsync(BaseUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ Perfil creado exitosamente");
                    return await response.Content.ReadFromJsonAsync<Pabloreyes>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ Error del servidor: {response.StatusCode} - {error}");
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al crear perfil: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"{BaseUrl}/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al eliminar perfil: {ex.Message}");
                return false;
            }
        }
    }
}