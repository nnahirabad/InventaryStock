using InventarioComercio.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Infrastructure
{
    public class InventarioComercioDbContext : DbContext
    {

        public InventarioComercioDbContext(DbContextOptions<InventarioComercioDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // CATEGORIA
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("Categorias");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // PRODUCTO
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Productos");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.Descripcion)
                      .HasMaxLength(500);

                entity.Property(p => p.Codigo)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(p => p.StockActual)
                      .IsRequired();

                entity.Property(p => p.StockMinimo)
                      .IsRequired();

                entity.HasOne(p => p.Categoria)
                      .WithMany(c => c.Productos)
                      .HasForeignKey(p => p.CategoriaId)
                      .OnDelete(DeleteBehavior.Restrict); // Cambialo a Cascade si querés que se borren productos al borrar categoría
            });

            // MOVIMIENTO STOCK
            modelBuilder.Entity<MovimientoStock>(entity =>
            {
                entity.ToTable("MovimientosStock");

                entity.HasKey(m => m.Id);

                entity.Property(m => m.Cantidad)
                      .IsRequired();

                entity.Property(m => m.Fecha)
                      .IsRequired();

                entity.Property(m => m.Observacion)
                      .HasMaxLength(300);

                entity.Property(m => m.Tipo)
                      .IsRequired();

                entity.HasOne(m => m.Producto)
                      .WithMany(p => p.MovimientosStock)
                      .HasForeignKey(m => m.ProductoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // USUARIO
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.PasswordHash)
                      .IsRequired();

                entity.Property(u => u.Rol)
                      .IsRequired()
                      .HasMaxLength(20);
            });
        }


        public DbSet<Producto>? Productos { get; set; }
        public DbSet<Usuario>? Usuarios { get; set;  }

        public DbSet<Categoria> Categorias { get; set;  }

        public DbSet<MovimientoStock> MovimientoStocks { get; set;  }
    }
}
