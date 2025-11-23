using Microsoft.EntityFrameworkCore;
using PerfilSena.API.Models;

namespace PerfilSena.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PabloReyes> PabloReyes { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relación de Comentario 
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.PabloReyesEmisor)
                .WithMany(p => p.ComentariosEnviados)
                .HasForeignKey(c => c.PabloReyesEmisorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar relación de Comentario 
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.PabloReyesReceptor)
                .WithMany(p => p.ComentariosRecibidos)
                .HasForeignKey(c => c.PabloReyesReceptorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índices para mejorar rendimiento
            modelBuilder.Entity<Comentario>()
                .HasIndex(c => new { c.PabloReyesEmisorId, c.PabloReyesReceptorId });

            modelBuilder.Entity<PabloReyes>()
                .HasIndex(p => p.Nombre);
        }
    }
}