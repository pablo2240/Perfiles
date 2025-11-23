namespace PerfilSena.WEB.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Contenido { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int PabloReyesEmisorId { get; set; }
        public int PabloReyesReceptorId { get; set; }

        // Propiedades adicionales para la UI
        public string NombreEmisor { get; set; } = string.Empty;
        public string ImagenEmisor { get; set; } = string.Empty;
    }
}
