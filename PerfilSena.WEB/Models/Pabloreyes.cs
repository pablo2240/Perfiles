namespace PerfilSena.WEB.Models
{
    public class Pabloreyes
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string? Imagen { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
