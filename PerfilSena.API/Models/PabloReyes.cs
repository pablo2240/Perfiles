using System.ComponentModel.DataAnnotations;

namespace PerfilSena.API.Models
{
    public class PabloReyes
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Telefono { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Direccion { get; set; } = string.Empty;

        public string? Imagen { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public ICollection<Comentario> ComentariosEnviados { get; set; } = new List<Comentario>();
        public ICollection<Comentario> ComentariosRecibidos { get; set; } = new List<Comentario>();
    }
}