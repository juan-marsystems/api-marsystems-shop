using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.models;

public partial class MarketSystemsContext : DbContext
{
    public MarketSystemsContext()
    {
    }

    public MarketSystemsContext(DbContextOptions<MarketSystemsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Detalle> Detalles { get; set; }

    public virtual DbSet<Ordene> Ordenes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=db_MarketSystems;User Id=postgres;Password=admin;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.IdArt).HasName("articulos_pkey");

            entity.ToTable("articulos");

            entity.Property(e => e.IdArt).HasColumnName("id_art");
            entity.Property(e => e.DescriptionArt)
                .HasMaxLength(300)
                .HasColumnName("description_art");
            entity.Property(e => e.ImgArt)
                .HasMaxLength(100)
                .HasColumnName("img_art");
            entity.Property(e => e.NameArt)
                .HasMaxLength(100)
                .HasColumnName("name_art");
            entity.Property(e => e.PriceArt).HasColumnName("price_art");
            entity.Property(e => e.QuantityArt).HasColumnName("quantity_art");
        });

        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.IdCart).HasName("carritos_pkey");

            entity.ToTable("carritos");

            entity.Property(e => e.IdCart).HasColumnName("id_cart");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("carritos_id_user_fkey");
        });

        modelBuilder.Entity<Detalle>(entity =>
        {
            entity.HasKey(e => e.IdDetail).HasName("detalle_pkey");

            entity.ToTable("detalle");

            entity.Property(e => e.IdDetail).HasColumnName("id_detail");
            entity.Property(e => e.IdArt).HasColumnName("id_art");
            entity.Property(e => e.IdCart).HasColumnName("id_cart");
            entity.Property(e => e.QuantityDetail).HasColumnName("quantity_detail");

            entity.HasOne(d => d.IdArtNavigation).WithMany(p => p.Detalles)
                .HasForeignKey(d => d.IdArt)
                .HasConstraintName("detalle_id_art_fkey");

            entity.HasOne(d => d.IdCartNavigation).WithMany(p => p.Detalles)
                .HasForeignKey(d => d.IdCart)
                .HasConstraintName("detalle_id_cart_fkey");
        });

        modelBuilder.Entity<Ordene>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("ordenes_pkey");

            entity.ToTable("ordenes");

            entity.Property(e => e.IdOrder).HasColumnName("id_order");
            entity.Property(e => e.DateOrder)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_order");
            entity.Property(e => e.IdCart).HasColumnName("id_cart");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalOrder).HasColumnName("total_order");

            entity.HasOne(d => d.IdCartNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdCart)
                .HasConstraintName("ordenes_id_cart_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("ordenes_id_user_fkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.AgeUser).HasColumnName("age_user");
            entity.Property(e => e.EmailUser)
                .HasMaxLength(100)
                .HasColumnName("email_user");
            entity.Property(e => e.NameUser)
                .HasMaxLength(100)
                .HasColumnName("name_user");
            entity.Property(e => e.PassUser)
                .HasMaxLength(100)
                .HasColumnName("pass_user");
            entity.Property(e => e.SurnameUser)
                .HasMaxLength(100)
                .HasColumnName("surname_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
