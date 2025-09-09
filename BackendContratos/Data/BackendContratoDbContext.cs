using BackendContratos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // <-- para PasswordHasher

namespace BackendContratos.Data
{
    public class BackendContratoDbContext : DbContext
    {
        public BackendContratoDbContext(DbContextOptions<BackendContratoDbContext> options)
              : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Liquidacion> Liquidaciones { get; set; }
        public DbSet<Poliza> Polizas { get; set; }
        public DbSet<Alerta> Alertas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuración decimal
            modelBuilder.Entity<Servicio>()
                .Property(s => s.Precio)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Liquidacion>()
                .Property(l => l.Total)
                .HasPrecision(18, 2);
            // Relaciones
            modelBuilder.Entity<Liquidacion>()
                .HasOne(l => l.Contrato)
                .WithMany(c => c.Liquidaciones)
                .HasForeignKey(l => l.ContratoId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Liquidacion>()
                .HasOne(l => l.Servicio)
                .WithMany(s => s.Liquidaciones)
                .HasForeignKey(l => l.ServicioId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Liquidacion>()
                .HasOne(l => l.Usuario)
                .WithMany(u => u.Liquidaciones)
                .HasForeignKey(l => l.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
    }