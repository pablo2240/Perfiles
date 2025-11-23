using Microsoft.AspNetCore.Mvc;
using PerfilSena.API.Models;
using PerfilSena.API.Services;

namespace PerfilSena.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioService _service;
        private readonly ILogger<ComentarioController> _logger;

        public ComentarioController(IComentarioService service, ILogger<ComentarioController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("conversacion/{pabloReyesId1}/{pabloReyesId2}")]
        public async Task<ActionResult<List<Comentario>>> GetConversacion(int pabloReyesId1, int pabloReyesId2)
        {
            try
            {
                _logger.LogInformation("📩 Obteniendo conversación entre {Id1} y {Id2}", pabloReyesId1, pabloReyesId2);
                var comentarios = await _service.ObtenerConversacionAsync(pabloReyesId1, pabloReyesId2);
                _logger.LogInformation("✅ {Count} comentarios encontrados", comentarios.Count);
                return Ok(comentarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error al obtener conversación");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("pabloreyes/{pabloReyesId}")]
        public async Task<ActionResult<List<Comentario>>> GetByPabloReyes(int pabloReyesId)
        {
            try
            {
                var comentarios = await _service.ObtenerPorPabloReyesAsync(pabloReyesId);
                return Ok(comentarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener comentarios");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Comentario>> Create([FromBody] ComentarioDto dto)
        {
            try
            {
                _logger.LogInformation("📨 Recibiendo comentario de {EmisorId} a {ReceptorId}", dto.PabloReyesEmisorId, dto.PabloReyesReceptorId);

                if (string.IsNullOrWhiteSpace(dto.Contenido))
                {
                    _logger.LogWarning("⚠️ Contenido vacío");
                    return BadRequest(new { error = "El contenido del comentario es requerido" });
                }

                if (dto.PabloReyesEmisorId == dto.PabloReyesReceptorId)
                {
                    _logger.LogWarning("⚠️ Intento de enviarse mensaje a sí mismo");
                    return BadRequest(new { error = "No puedes enviarte comentarios a ti mismo" });
                }

                var comentario = new Comentario
                {
                    Contenido = dto.Contenido,
                    PabloReyesEmisorId = dto.PabloReyesEmisorId,
                    PabloReyesReceptorId = dto.PabloReyesReceptorId
                };

                var created = await _service.CrearAsync(comentario);
                _logger.LogInformation("✅ Comentario creado con ID: {Id}", created.Id);

                return CreatedAtAction(nameof(GetByPabloReyes), new { pabloReyesId = created.PabloReyesEmisorId }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error al crear comentario");
                return StatusCode(500, new { error = ex.Message, details = ex.InnerException?.Message });
            }
        }
    }

    // DTO para recibir comentarios
    public class ComentarioDto
    {
        public string Contenido { get; set; } = string.Empty;
        public int PabloReyesEmisorId { get; set; }
        public int PabloReyesReceptorId { get; set; }
    }
}