using System;
using System.Collections.Generic;
using MandiraApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MandiraApi.Data;

public partial class MandiraDbContext : DbContext
{
    public MandiraDbContext(DbContextOptions<MandiraDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<DetalleOrden> DetalleOrdens { get; set; }

    public virtual DbSet<ImagenesProducto> ImagenesProductos { get; set; }

    public virtual DbSet<Ordene> Ordenes { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VistaProductosActivo> VistaProductosActivos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.CarritoId).HasName("PRIMARY");

            entity.ToTable("carrito");

            entity.HasIndex(e => e.ProductoId, "productoID");

            entity.HasIndex(e => e.UsuarioId, "usuarioID");

            entity.Property(e => e.CarritoId).HasColumnName("carritoID");
            entity.Property(e => e.AgregadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("agregado_en");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.ProductoId).HasColumnName("productoID");
            entity.Property(e => e.UsuarioId).HasColumnName("usuarioID");

            entity.HasOne(d => d.Producto).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("carrito_ibfk_2");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("carrito_ibfk_1");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PRIMARY");

            entity.ToTable("categorias");

            entity.HasIndex(e => e.Nombre, "idx_categoria_nombre");

            entity.Property(e => e.CategoriaId).HasColumnName("categoriaID");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<DetalleOrden>(entity =>
        {
            entity.HasKey(e => e.DetalleOrdenId).HasName("PRIMARY");

            entity.ToTable("detalle_orden");

            entity.HasIndex(e => e.OrdenId, "ordenID");

            entity.HasIndex(e => e.ProductoId, "productoID");

            entity.Property(e => e.DetalleOrdenId).HasColumnName("detalleOrdenID");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.OrdenId).HasColumnName("ordenID");
            entity.Property(e => e.PrecioUnitario)
                .HasPrecision(10, 2)
                .HasColumnName("precio_unitario");
            entity.Property(e => e.ProductoId).HasColumnName("productoID");

            entity.HasOne(d => d.Orden).WithMany(p => p.DetalleOrdens)
                .HasForeignKey(d => d.OrdenId)
                .HasConstraintName("detalle_orden_ibfk_1");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetalleOrdens)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("detalle_orden_ibfk_2");
        });

        modelBuilder.Entity<ImagenesProducto>(entity =>
        {
            entity.HasKey(e => e.ImagenProductoId).HasName("PRIMARY");

            entity.ToTable("imagenes_producto");

            entity.HasIndex(e => e.ProductoId, "productoID");

            entity.Property(e => e.ImagenProductoId).HasColumnName("imagenProductoID");
            entity.Property(e => e.Imagen).HasColumnName("imagen");
            entity.Property(e => e.ProductoId).HasColumnName("productoID");
            entity.Property(e => e.Url)
                .HasColumnType("text")
                .HasColumnName("url");

            entity.HasOne(d => d.Producto).WithMany(p => p.ImagenesProductos)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("imagenes_producto_ibfk_1");
        });

        modelBuilder.Entity<Ordene>(entity =>
        {
            entity.HasKey(e => e.OrdenId).HasName("PRIMARY");

            entity.ToTable("ordenes");

            entity.HasIndex(e => e.Estado, "idx_orden_estado");

            entity.HasIndex(e => e.UsuarioId, "usuarioID");

            entity.Property(e => e.OrdenId).HasColumnName("ordenID");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'pendiente'")
                .HasColumnType("enum('pendiente','pagado','cancelado','enviado')")
                .HasColumnName("estado");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");
            entity.Property(e => e.UsuarioId).HasColumnName("usuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("ordenes_ibfk_1");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PRIMARY");

            entity.ToTable("productos");

            entity.HasIndex(e => e.CategoriaId, "categoriaID");

            entity.HasIndex(e => e.Nombre, "idx_producto_nombre");

            entity.Property(e => e.ProductoId).HasColumnName("productoID");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.CategoriaId).HasColumnName("categoriaID");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.ImagenUrl)
                .HasColumnType("text")
                .HasColumnName("imagen_url");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.Stock)
                .HasDefaultValueSql("'0'")
                .HasColumnName("stock");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("productos_ibfk_1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("usuarioID");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .HasColumnName("contraseña");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.Direccion)
                .HasColumnType("text")
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol)
                .HasDefaultValueSql("'cliente'")
                .HasColumnType("enum('cliente','admin')")
                .HasColumnName("rol");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<VistaProductosActivo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vista_productos_activos");

            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .HasColumnName("categoria");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.ImagenUrl)
                .HasColumnType("text")
                .HasColumnName("imagen_url");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.ProductoId).HasColumnName("productoID");
            entity.Property(e => e.Stock)
                .HasDefaultValueSql("'0'")
                .HasColumnName("stock");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
