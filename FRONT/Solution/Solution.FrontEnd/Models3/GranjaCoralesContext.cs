using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Solution.FrontEnd.Models
{
    public partial class GranjaCoralesContext : DbContext
    {
        public GranjaCoralesContext()
        {
        }

        public GranjaCoralesContext(DbContextOptions<GranjaCoralesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articulo> Articulos { get; set; } = null!;
        public virtual DbSet<ArticulosSolicitud> ArticulosSolicituds { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<Solicitude> Solicitudes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=GranjaCorales;Trusted_Connection=True; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.HasKey(e => e.IdArticulo);

                entity.Property(e => e.IdArticulo).HasColumnName("id_articulo");

                entity.Property(e => e.Color)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("color");

                entity.Property(e => e.Dieta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dieta");

                entity.Property(e => e.Dificultad)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dificultad");

                entity.Property(e => e.Familia)
                    .HasMaxLength(100)
                    .HasColumnName("familia")
                    .IsFixedLength();

                entity.Property(e => e.FecModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_modificacion");

                entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("imagePath");

                entity.Property(e => e.ModificadoPor)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("modificado_por");

                entity.Property(e => e.NombreCientifico)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre_cientifico");

                entity.Property(e => e.NombreComun)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre_comun");

                entity.Property(e => e.Origen)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("origen");

                entity.Property(e => e.TamanoMax)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tamano_max");

                entity.Property(e => e.TamanoMinPecera)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tamano_min_pecera");

                entity.Property(e => e.Temperamento)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("temperamento");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(100)
                    .HasColumnName("tipo")
                    .IsFixedLength();

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK_Articulos_Categorias");
            });

            modelBuilder.Entity<ArticulosSolicitud>(entity =>
            {
                entity.HasKey(e => e.IdArticuloSolicitud);

                entity.ToTable("ArticulosSolicitud");

                entity.Property(e => e.IdArticuloSolicitud).HasColumnName("id_articulo_solicitud");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdArticulo).HasColumnName("id_articulo");

                entity.Property(e => e.IdSolicitud).HasColumnName("id_solicitud");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.ArticulosSolicituds)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticulosSolicitud_Articulos");

                entity.HasOne(d => d.IdSolicitudNavigation)
                    .WithMany(p => p.ArticulosSolicituds)
                    .HasForeignKey(d => d.IdSolicitud)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticulosSolicitud_Solicitudes");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.Property(e => e.IdCategoria)
                    .ValueGeneratedNever()
                    .HasColumnName("id_categoria");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<Solicitude>(entity =>
            {
                entity.HasKey(e => e.IdSolicitud);

                entity.Property(e => e.IdSolicitud).HasColumnName("id_solicitud");

                entity.Property(e => e.EstadoAprobacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado_aprobacion")
                    .HasDefaultValueSql("('Enviada')");

                entity.Property(e => e.EstadoSolicitud)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado_solicitud");

                entity.Property(e => e.FecCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fec_creacion");

                entity.Property(e => e.IdUsuario)
                    .HasMaxLength(450)
                    .HasColumnName("id_usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
