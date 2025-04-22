using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Pruebas.Cliente.Models;

public partial class SigaFfmContext : DbContext
{
    public SigaFfmContext()
    {
    }

    public SigaFfmContext(DbContextOptions<SigaFfmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Arma> Armas { get; set; }

    public virtual DbSet<Categoría> Categorías { get; set; }

    public virtual DbSet<Grado> Grados { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    public virtual DbSet<TblProducto> TblProductos { get; set; }

    public virtual DbSet<TblUsuario> TblUsuarios { get; set; }

    public virtual DbSet<Tercero> Terceros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    // => optionsBuilder.UseSqlServer("workstation id=SIGA_FFM.mssql.somee.com;packet size=4096;user id=aperezNWOGmail_SQLLogin_1;pwd=4tvg3todsw;data source=SIGA_FFM.mssql.somee.com;persist security info=False;initial catalog=SIGA_FFM;TrustServerCertificate=True");
    => optionsBuilder.UseSqlServer("workstation id=SIGA_FFMM.mssql.somee.com;packet size=4096;user id=aperezNWOOutlook_SQLLogin_1;pwd=vtl7mbj91h;data source=SIGA_FFMM.mssql.somee.com;persist security info=False;initial catalog=SIGA_FFMM;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Arma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Arma__3213E83F95FFEE03");

            entity.ToTable("Arma");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Arma1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Arma");
        });

        modelBuilder.Entity<Categoría>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categorí__3213E83FB6C5C312");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
        });

        modelBuilder.Entity<Grado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grados__3213E83FAE85C4B2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Grado1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Grado");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personal__3214EC07BBB5CE9E");

            entity.ToTable("Personal");

            entity.HasIndex(e => e.Cedula, "UQ__Personal__B4ADFE384BC901CA").IsUnique();

            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Creado).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FNacimiento).HasColumnName("F_Nacimiento");
            entity.Property(e => e.Foto).HasMaxLength(2500);
            entity.Property(e => e.Huella1).HasMaxLength(2500);
            entity.Property(e => e.Huella2).HasMaxLength(2500);
            entity.Property(e => e.IdArma).HasColumnName("Id_Arma");
            entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");
            entity.Property(e => e.IdCuerpo).HasColumnName("Id_Cuerpo");
            entity.Property(e => e.IdGenero)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("Id_Genero");
            entity.Property(e => e.IdGrado).HasColumnName("Id_Grado");
            entity.Property(e => e.IdRh)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Id_RH");
            entity.Property(e => e.PApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("P_Apellido");
            entity.Property(e => e.PNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("P_Nombre");
            entity.Property(e => e.Registro)
                .HasMaxLength(27)
                .IsUnicode(false)
                .HasComputedColumnSql("(concat(CONVERT([varchar](4),datepart(year,[Creado])),right('00'+CONVERT([varchar](2),datepart(month,[Creado])),(2)),right('00'+CONVERT([varchar](2),datepart(day,[Creado])),(2)),'-0000-0000-',right('000000'+CONVERT([varchar],[Id]),(6)),right('00'+CONVERT([varchar],datepart(day,[Creado])),(2))))", false);
            entity.Property(e => e.SApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("S_Apellido");
            entity.Property(e => e.SNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("S_Nombre");
        });

        modelBuilder.Entity<TblProducto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.ToTable("tbl_Producto");

            entity.Property(e => e.IdProducto).HasColumnName("id_Producto");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Detalle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("detalle");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precio");
        });

        modelBuilder.Entity<TblUsuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("tbl_usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .HasColumnName("apellido_materno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .HasColumnName("apellido_paterno");
            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .HasColumnName("cargo");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Identificacion).HasColumnName("identificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Tercero>(entity =>
        {
            entity.HasKey(e => e.IdPersona);

            entity.ToTable("Tercero");

            entity.HasIndex(e => e.Cedula, "UQ_Cedula").IsUnique();

            entity.Property(e => e.IdPersona).HasColumnName("Id_Persona");
            entity.Property(e => e.Cedula)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Creado).HasDefaultValueSql("(CONVERT([date],getdate()))");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FNacimiento).HasColumnName("F_Nacimiento");
            entity.Property(e => e.FPublica).HasColumnName("F_Publica");
            entity.Property(e => e.Huella).IsUnicode(false);
            entity.Property(e => e.IdArma).HasColumnName("Id_Arma");
            entity.Property(e => e.IdBatallon).HasColumnName("Id_Batallon");
            entity.Property(e => e.IdBrigada).HasColumnName("Id_Brigada");
            entity.Property(e => e.IdCargo).HasColumnName("Id_Cargo");
            entity.Property(e => e.IdCargoA).HasColumnName("Id_CargoA");
            entity.Property(e => e.IdCasa).HasColumnName("Id_Casa");
            entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");
            entity.Property(e => e.IdComandante).HasColumnName("Id_Comandante");
            entity.Property(e => e.IdComando).HasColumnName("Id_Comando");
            entity.Property(e => e.IdCompañia).HasColumnName("Id_Compañia");
            entity.Property(e => e.IdCuerpo).HasColumnName("Id_Cuerpo");
            entity.Property(e => e.IdDivision).HasColumnName("Id_Division");
            entity.Property(e => e.IdFuerza).HasColumnName("Id_Fuerza");
            entity.Property(e => e.IdGenero).HasColumnName("Id_Genero");
            entity.Property(e => e.IdGrado).HasColumnName("Id_Grado");
            entity.Property(e => e.IdJefatura).HasColumnName("Id_Jefatura");
            entity.Property(e => e.IdPais).HasColumnName("Id_Pais");
            entity.Property(e => e.IdPeloton).HasColumnName("Id_Peloton");
            entity.Property(e => e.IdRh).HasColumnName("Id_RH");
            entity.Property(e => e.IdSeccion).HasColumnName("Id_Seccion");
            entity.Property(e => e.IdSuperior).HasColumnName("Id_Superior");
            entity.Property(e => e.Observaciones).IsUnicode(false);
            entity.Property(e => e.PApellido)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("P_Apellido");
            entity.Property(e => e.PNombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("P_Nombre");
            entity.Property(e => e.Piso)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Placa)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Registro)
                .HasMaxLength(27)
                .IsUnicode(false)
                .HasComputedColumnSql("(concat(CONVERT([varchar](4),datepart(year,[Creado])),right('00'+CONVERT([varchar](2),datepart(month,[Creado])),(2)),right('00'+CONVERT([varchar](2),datepart(day,[Creado])),(2)),'-0000-0000-',right('000000'+CONVERT([varchar],[Id_Persona]),(6)),right('00'+CONVERT([varchar],datepart(day,[Creado])),(2))))", false);
            entity.Property(e => e.SApellido)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("S_Apellido");
            entity.Property(e => e.SNombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("S_Nombre");
            entity.Property(e => e.Sector)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Titular)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Torre)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Zona)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
