using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Videojuegos.Infrastructura.Entities.Videojuegos;

public partial class VideojuegosDbContext : DbContext
{
    public VideojuegosDbContext()
    {
    }

    public VideojuegosDbContext(DbContextOptions<VideojuegosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calificacione> Calificaciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuariosToken> UsuariosTokens { get; set; }

    public virtual DbSet<Videojuego> Videojuegos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calificacione>(entity =>
        {
            entity.HasKey(e => e.CalificacionId).HasName("PK__Califica__4CF54ABE32E0466D");

            entity.Property(e => e.CalificacionId)
                .ValueGeneratedNever()
                .HasColumnName("CalificacionID");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Puntuacion).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.VideojuegoId).HasColumnName("VideojuegoID");

            entity.HasOne(d => d.Videojuego).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.VideojuegoId)
                .HasConstraintName("FK__Calificac__Video__29572725");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07E4CA7E9D");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Username).HasMaxLength(256);
        });

        modelBuilder.Entity<UsuariosToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07599C0F26");

            entity.ToTable("UsuariosToken");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Username).HasMaxLength(256);
        });

        modelBuilder.Entity<Videojuego>(entity =>
        {
            entity.HasKey(e => e.VideojuegoId).HasName("PK__Videojue__D6B5FD497603088E");

            entity.Property(e => e.VideojuegoId).HasColumnName("VideojuegoID");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PuntajePromedio)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(4, 2)");
        });
    }

}
