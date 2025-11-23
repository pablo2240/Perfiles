using System.ComponentModel.DataAnnotations;

namespace PerfilSena.API.Models
{
    public class Comentario
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Contenido { get; set; } = string.Empty;

        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        public int PabloReyesEmisorId { get; set; }
        public PabloReyes PabloReyesEmisor { get; set; } = null!;

        public int PabloReyesReceptorId { get; set; }
        public PabloReyes PabloReyesReceptor { get; set; } = null!;
    }
}