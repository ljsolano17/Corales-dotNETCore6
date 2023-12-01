using Microsoft.EntityFrameworkCore;
using Solution.DO.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.DAL.EF
{
    public partial class SolutionDbContext : DbContext
    {
        
        public SolutionDbContext(DbContextOptions<SolutionDbContext> options) : 
            base(options)
        {
        }
       
        public virtual DbSet<Articulos> Articulos { get; set; }

        public virtual DbSet<ArticulosSolicitud> ArticulosSolicitud { get; set; }

        public virtual DbSet<Categorias> Categorias { get; set; }

        public virtual DbSet<Solicitudes> Solicitudes { get; set; }
       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
      => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=GranjaCorales;Trusted_Connection=True; TrustServerCertificate=True;");
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulos>(entity =>
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
                    .IsFixedLength()
                    .HasColumnName("familia");
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
                    .IsFixedLength()
                    .HasColumnName("tipo");

                entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK_Articulos_Categorias");
            });

            modelBuilder.Entity<ArticulosSolicitud>(entity =>
            {
                entity.HasKey(e => e.IdArticuloSolicitud);

                entity.ToTable("ArticulosSolicitud");

                entity.Property(e => e.IdArticuloSolicitud).HasColumnName("id_articulo_solicitud");
                entity.Property(e => e.Cantidad)
                    .HasDefaultValueSql("((1))")
                    .HasColumnName("cantidad");
                entity.Property(e => e.IdArticulo).HasColumnName("id_articulo");
                entity.Property(e => e.IdSolicitud).HasColumnName("id_solicitud");

                entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.ArticulosSolicitud)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticulosSolicitud_Articulos");

                entity.HasOne(d => d.IdSolicitudNavigation).WithMany(p => p.ArticulosSolicitud)
                    .HasForeignKey(d => d.IdSolicitud)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticulosSolicitud_Solicitudes");
            });

            modelBuilder.Entity<Categorias>(entity =>
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

            modelBuilder.Entity<Solicitudes>(entity =>
            {
                entity.HasKey(e => e.IdSolicitud);

                entity.Property(e => e.IdSolicitud).HasColumnName("id_solicitud");
                entity.Property(e => e.EstadoAprobacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Enviada')")
                    .HasColumnName("estado_aprobacion");
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
