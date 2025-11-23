using Microsoft.EntityFrameworkCore;
using PerfilSena.API.Data;
using PerfilSena.API.Models;

namespace PerfilSena.API.Services
{
    public class ComentarioService : IComentarioService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ComentarioService> _logger;

        public ComentarioService(AppDbContext context, ILogger<ComentarioService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Comentario>> ObtenerConversacionAsync(int pabloReyesId1, int pabloReyesId2)
        {
            try
            {
                var comentarios = await _context.Comentarios
                    .Include(c => c.PabloReyesEmisor)
                    .Include(c => c.PabloReyesReceptor)
                    .Where(c =>
                        (c.PabloReyesEmisorId == pabloReyesId1 && c.PabloReyesReceptorId == pabloReyesId2) ||
                        (c.PabloReyesEmisorId == pabloReyesId2 && c.PabloReyesReceptorId == pabloReyesId1))
                    .OrderBy(c => c.Fecha)
                    .ToListAsync();

                _logger.LogInformation("📊 Conversación: {Count} mensajes entre {Id1} y {Id2}",
                    comentarios.Count, pabloReyesId1, pabloReyesId2);

                return comentarios;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener conversación");
                throw;
            }
        }

        public async Task<Comentario> CrearAsync(Comentario comentario)
        {
            try
            {
                comentario.Fecha = DateTime.UtcNow;

                _logger.LogInformation("💾 Guardando comentario en BD");
                _context.Comentarios.Add(comentario);
                await _context.SaveChangesAsync();

                // Cargar las relaciones
                await _context.Entry(comentario)
                    .Reference(c => c.PabloReyesEmisor)
                    .LoadAsync();
                await _context.Entry(comentario)
                    .Reference(c => c.PabloReyesReceptor)
                    .LoadAsync();

                _logger.LogInformation("✅ Comentario guardado: ID={Id}, De={Emisor}, Para={Receptor}",
                    comentario.Id, comentario.PabloReyesEmisorId, comentario.PabloReyesReceptorId);

                return comentario;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear comentario");
                throw;
            }
        }

        public async Task<List<Comentario>> ObtenerPorPabloReyesAsync(int pabloReyesId)
        {
            return await _context.Comentarios
                .Include(c => c.PabloReyesEmisor)
                .Include(c => c.PabloReyesReceptor)
                .Where(c => c.PabloReyesEmisorId == pabloReyesId || c.PabloReyesReceptorId == pabloReyesId)
                .OrderByDescending(c => c.Fecha)
                .ToListAsync();
        }
    }
}