using Microsoft.AspNetCore.Mvc;
using PerfilSena.API.Models;
using PerfilSena.API.Services;

namespace PerfilSena.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PabloReyesController : ControllerBase
    {
        private readonly IPabloReyesService _service;
        private readonly ILogger<PabloReyesController> _logger;

        public PabloReyesController(IPabloReyesService service, ILogger<PabloReyesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/pabloreyes
        [HttpGet]
        public async Task<ActionResult<List<PabloReyes>>> GetAll()
        {
            try
            {
                var perfiles = await _service.ObtenerTodosAsync();
                return Ok(perfiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener perfiles");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/pabloreyes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PabloReyes>> GetById(int id)
        {
            try
            {
                var pabloReyes = await _service.ObtenerPorIdAsync(id);
                if (pabloReyes == null)
                    return NotFound($"PabloReyes con ID {id} no encontrado");

                return Ok(pabloReyes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener perfil {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/pabloreyes
        [HttpPost]
        public async Task<ActionResult<PabloReyes>> Create([FromForm] string nombre,
                                                             [FromForm] string telefono,
                                                             [FromForm] string direccion,
                                                             IFormFile? imagen)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                    return BadRequest("El nombre es requerido");

                var pabloReyes = new PabloReyes
                {
                    Nombre = nombre.Trim(),
                    Telefono = telefono?.Trim() ?? string.Empty,
                    Direccion = direccion?.Trim() ?? string.Empty
                };

                var created = await _service.CrearAsync(pabloReyes, imagen);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear perfil");
                return StatusCode(500, "Error al crear el perfil");
            }
        }

        // PUT: api/pabloreyes/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id,
                                               [FromForm] string nombre,
                                               [FromForm] string telefono,
                                               [FromForm] string direccion,
                                               IFormFile? imagen)
        {
            try
            {
                var pabloReyes = new PabloReyes
                {
                    Id = id,
                    Nombre = nombre.Trim(),
                    Telefono = telefono?.Trim() ?? string.Empty,
                    Direccion = direccion?.Trim() ?? string.Empty
                };

                var result = await _service.ActualizarAsync(pabloReyes, imagen);
                if (!result)
                    return NotFound($"PabloReyes con ID {id} no encontrado");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar perfil {Id}", id);
                return StatusCode(500, "Error al actualizar el perfil");
            }
        }

        // DELETE: api/pabloreyes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.EliminarAsync(id);
                if (!result)
                    return NotFound($"PabloReyes con ID {id} no encontrado");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar perfil {Id}", id);
                return StatusCode(500, "Error al eliminar el perfil");
            }
        }
    }
}