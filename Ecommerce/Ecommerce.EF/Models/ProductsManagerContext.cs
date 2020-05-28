using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ecommerce.Domain.Models
{
    public partial class ProductsManagerContext : DbContext
    {
        public ProductsManagerContext()
        {
        }

        public ProductsManagerContext(DbContextOptions<ProductsManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articulo> Articulo { get; set; }
        public virtual DbSet<ArticuloTipo> ArticuloTipo { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Lote> Lote { get; set; }
        public virtual DbSet<Notificaciones> Notificaciones { get; set; }
        public virtual DbSet<Solicitud> Solicitud { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;database=ProductsManager;user=usrpm;password=usrpm");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.Marca).IsUnicode(false);

                entity.Property(e => e.NumeroSerie).IsUnicode(false);

                entity.HasOne(d => d.IdLoteNavigation)
                    .WithMany(p => p.Articulo)
                    .HasForeignKey(d => d.IdLote)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Articulo_Lote");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Articulo)
                    .HasForeignKey(d => d.IdTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Articulo_ArticuloTipo");

                entity.HasOne(d => d.UsuarioAdjudicadoNavigation)
                    .WithMany(p => p.Articulo)
                    .HasForeignKey(d => d.UsuarioAdjudicado)
                    .HasConstraintName("FK_Articulo_Usuario");
            });

            modelBuilder.Entity<ArticuloTipo>(entity =>
            {
                entity.Property(e => e.Codigo).IsUnicode(false);

                entity.Property(e => e.Descripcion).IsUnicode(false);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo).IsUnicode(false);

                entity.Property(e => e.Descripcion).IsUnicode(false);
            });

            modelBuilder.Entity<Lote>(entity =>
            {
                entity.Property(e => e.Actualizacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.NombreImagen).IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Lote)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lote__IdEstado__32E0915F");
            });

            modelBuilder.Entity<Notificaciones>(entity =>
            {
                entity.Property(e => e.Leido).HasDefaultValueSql("((0))");

                entity.Property(e => e.Stamp).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.Notificaciones)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notificaciones_Articulo");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Notificaciones)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notificaciones_Usuario");
            });

            modelBuilder.Entity<Solicitud>(entity =>
            {
                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.Solicitud)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Solicitud_Articulo");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Solicitud)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Solicitud_Usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Apellido).IsUnicode(false);

                entity.Property(e => e.Clave).IsUnicode(false);

                entity.Property(e => e.Creacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Mail).IsUnicode(false);

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.UltimoIngreso).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Usuario1).IsUnicode(false);
            });
        }
    }
}
