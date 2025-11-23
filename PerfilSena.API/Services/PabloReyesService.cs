using Microsoft.EntityFrameworkCore;
using PerfilSena.API.Data;
using PerfilSena.API.Models;

namespace PerfilSena.API.Services
{
    public class PabloReyesService : IPabloReyesService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<PabloReyesService> _logger;

        public PabloReyesService(AppDbContext context, IWebHostEnvironment env, ILogger<PabloReyesService> logger)
        {
            _context = context;
            _env = env;
            _logger = logger;
        }

        public async Task<List<PabloReyes>> ObtenerTodosAsync()
        {
            return await _context.PabloReyes
                .OrderByDescending(p => p.FechaCreacion)
                .ToListAsync();
        }

        public async Task<PabloReyes?> ObtenerPorIdAsync(int id)
        {
            return await _context.PabloReyes
                .Include(p => p.ComentariosEnviados)
                .Include(p => p.ComentariosRecibidos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PabloReyes> CrearAsync(PabloReyes pabloReyes, IFormFile? imagen)
        {
            if (imagen != null && imagen.Length > 0)
            {
                pabloReyes.Imagen = await GuardarImagenAsync(imagen);
            }

            _context.PabloReyes.Add(pabloReyes);
            await _context.SaveChangesAsync();

            _logger.LogInformation("PabloReyes creado: {Id} - {Nombre}", pabloReyes.Id, pabloReyes.Nombre);
            return pabloReyes;
        }

        public async Task<bool> ActualizarAsync(PabloReyes pabloReyes, IFormFile? imagen)
        {
            var existente = await _context.PabloReyes.FindAsync(pabloReyes.Id);
            if (existente == null) return false;

            existente.Nombre = pabloReyes.Nombre;
            existente.Telefono = pabloReyes.Telefono;
            existente.Direccion = pabloReyes.Direccion;

            if (imagen != null && imagen.Length > 0)
            {
                if (!string.IsNullOrEmpty(existente.Imagen))
                {
                    EliminarImagenFisica(existente.Imagen);
                }
                existente.Imagen = await GuardarImagenAsync(imagen);
            }

            _context.Entry(existente).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var pabloReyes = await _context.PabloReyes.FindAsync(id);
            if (pabloReyes == null) return false;

            if (!string.IsNullOrEmpty(pabloReyes.Imagen))
            {
                EliminarImagenFisica(pabloReyes.Imagen);
            }

            _context.PabloReyes.Remove(pabloReyes);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> GuardarImagenAsync(IFormFile imagen)
        {
            var extension = Path.GetExtension(imagen.FileName);
            var fileName = $"pabloreyes_{Guid.NewGuid()}{extension}";
            var carpetaImg = Path.Combine(_env.WebRootPath, "img");

            if (!Directory.Exists(carpetaImg))
            {
                Directory.CreateDirectory(carpetaImg);
            }

            var filePath = Path.Combine(carpetaImg, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imagen.CopyToAsync(stream);
            }

            return $"img/{fileName}";
        }

        private void EliminarImagenFisica(string rutaImagen)
        {
            try
            {
                var imagePath = Path.Combine(_env.WebRootPath, rutaImagen);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar imagen: {RutaImagen}", rutaImagen);
            }
        }
    }
}