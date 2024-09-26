using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinalAPI2024_HTTPS.Models
{
    public partial class AplicacionRecorridos2024Context : DbContext
    {
      

        public AplicacionRecorridos2024Context(DbContextOptions<AplicacionRecorridos2024Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Busetum> Buseta { get; set; }
        public virtual DbSet<Comentario> Comentarios { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Recorrido> Recorridos { get; set; }
        public virtual DbSet<Recorridodetalle> Recorridodetalles { get; set; }
        public virtual DbSet<Reserva> Reservas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Busetum>(entity =>
            {
                entity.HasKey(e => e.IdBus)
                    .HasName("PRIMARY");

                entity.ToTable("buseta");

                entity.Property(e => e.IdBus).HasColumnName("id_bus");

                entity.Property(e => e.Anio).HasColumnName("anio");

                entity.Property(e => e.Capacidad).HasColumnName("capacidad");

                entity.Property(e => e.IdUsu).HasColumnName("id_usu");

                entity.Property(e => e.Imagen)
                    .HasColumnType("blob")
                    .HasColumnName("imagen");

                entity.Property(e => e.Modelo)
                    .HasMaxLength(40)
                    .HasColumnName("modelo");

                entity.Property(e => e.Placa)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("placa");
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.IdCom)
                    .HasName("PRIMARY");

                entity.ToTable("comentario");

                entity.Property(e => e.IdCom).HasColumnName("id_com");

                entity.Property(e => e.Comentario1)
                    .HasMaxLength(500)
                    .HasColumnName("comentario");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdUsu).HasColumnName("id_usu");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.IdLog)
                    .HasName("PRIMARY");

                entity.ToTable("login");

                entity.Property(e => e.IdLog)
                    .ValueGeneratedNever()
                    .HasColumnName("id_log");

                entity.Property(e => e.CorreoLog)
                    .HasMaxLength(100)
                    .HasColumnName("correo_log");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.PasswdLog)
                    .HasMaxLength(192)
                    .HasColumnName("passwd_log");

                entity.Property(e => e.Rol)
                    .HasMaxLength(20)
                    .HasColumnName("rol");
            });

            modelBuilder.Entity<Recorrido>(entity =>
            {
                entity.HasKey(e => e.IdRec)
                    .HasName("PRIMARY");

                entity.ToTable("recorrido");

                entity.Property(e => e.IdRec).HasColumnName("id_rec");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaRec)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("fecha_rec");

                entity.Property(e => e.HoraRec)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("hora_rec");

                entity.Property(e => e.IdBus).HasColumnName("id_bus");

                entity.Property(e => e.IdUsu).HasColumnName("id_usu");

                entity.Property(e => e.NombreRec)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("nombre_rec");

                entity.Property(e => e.NumRec).HasColumnName("num_rec");

                entity.Property(e => e.PrecioRec)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("precio_rec");
            });

            modelBuilder.Entity<Recorridodetalle>(entity =>
            {
                entity.HasKey(e => e.IdRecDetalle)
                    .HasName("PRIMARY");

                entity.ToTable("recorridodetalle");

                entity.HasIndex(e => e.IdRec, "id_rec");

                entity.Property(e => e.IdRecDetalle).HasColumnName("id_rec_detalle");

                entity.Property(e => e.IdRec).HasColumnName("id_rec");

                entity.Property(e => e.Latitud)
                    .HasPrecision(18, 16)
                    .HasColumnName("latitud");

                entity.Property(e => e.Longitud)
                    .HasPrecision(18, 16)
                    .HasColumnName("longitud");

                entity.HasOne(d => d.IdRecNavigation)
                    .WithMany(p => p.Recorridodetalles)
                    .HasForeignKey(d => d.IdRec)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("recorridodetalle_ibfk_1");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.IdRes)
                    .HasName("PRIMARY");

                entity.ToTable("reserva");

                entity.Property(e => e.IdRes).HasColumnName("id_res");

                entity.Property(e => e.FechaRes)
                    .HasMaxLength(40)
                    .HasColumnName("fecha_res");

                entity.Property(e => e.HoraRec)
                    .HasMaxLength(40)
                    .HasColumnName("hora_rec");

                entity.Property(e => e.IdConductor).HasColumnName("id_conductor");

                entity.Property(e => e.IdRec).HasColumnName("id_rec");

                entity.Property(e => e.IdUsu).HasColumnName("id_usu");

                entity.Property(e => e.NombreEst)
                    .HasMaxLength(40)
                    .HasColumnName("nombre_est");

                entity.Property(e => e.NombreRec)
                    .HasMaxLength(40)
                    .HasColumnName("nombre_rec");

                entity.Property(e => e.TelefonoEst)
                    .HasMaxLength(10)
                    .HasColumnName("telefono_est");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PRIMARY");

                entity.ToTable("usuario");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("apellido");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("nombre");

                entity.Property(e => e.Passwd)
                    .IsRequired()
                    .HasMaxLength(192)
                    .HasColumnName("passwd");

                entity.Property(e => e.ResetPasswdTkn).HasColumnName("resetPasswdTkn");

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("rol");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("telefono");

                entity.Property(e => e.Token).HasColumnName("token");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
