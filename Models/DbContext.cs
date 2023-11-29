// using Microsoft.EntityFrameworkCore;
// using MiProyecto;



// public class MyDbContext : DbContext
// {
//   public DbSet<Marca> Marca { get; set; }
//   public DbSet<Producto> Producto { get; set; }
//   public DbSet<Local> Local { get; set; }
//   public DbSet<Venta> Venta { get; set; }
//   public DbSet<VentaDetalle> VentaDetalle { get; set; }



//   // Otras propiedades para tus tablas...


//   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

//   {
//     string serverName = "lab-defontana-202310.caporvnn6sbh.us-east-1.rds.amazonaws.com";
//     string dbName = "Prueba";
//     string username = "ReadOnly";
//     string password = "d*3PSf2MmRX9vJtA5sgwSphCVQ26*T53uU";

//     string connectionString = $"Server={serverName};Database={dbName};User ID={username};Password={password};TrustServerCertificate=True;";
//     // Aquí debes configurar tu cadena de conexión a la base de datos

//     try
//     {
//       optionsBuilder.UseSqlServer(connectionString);
//       using (var context = new MyDbContext())
//       {
//         // context.Database.OpenConnection();
//         Console.WriteLine("Conexión exitosa");
//       }
//     }
//     catch (Exception ex)
//     {
//       Console.WriteLine($"Error al intentar conectar a la base de datos: {ex.Message}");
//     }
//   }
// }

using Microsoft.EntityFrameworkCore;
using MiProyecto;
using System;

public class MyDbContext : DbContext
{
  public DbSet<Marca> Marca { get; set; }
  public DbSet<Producto> Producto { get; set; }
  public DbSet<Local> Local { get; set; }
  public DbSet<Venta> Venta { get; set; }
  public DbSet<VentaDetalle> VentaDetalle { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    string serverName = "lab-defontana-202310.caporvnn6sbh.us-east-1.rds.amazonaws.com";
    string dbName = "Prueba";
    string username = "ReadOnly";
    string password = "d*3PSf2MmRX9vJtA5sgwSphCVQ26*T53uU";

    string connectionString = $"Server={serverName};Database={dbName};User ID={username};Password={password};TrustServerCertificate=True;";

    try
    {
      optionsBuilder.UseSqlServer(connectionString);
      Console.WriteLine("Conexión exitosa");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error al intentar conectar a la base de datos: {ex.Message}");
    }
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {

    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<VentaDetalle>()
        .HasOne(vd => vd.Producto)
        .WithMany()
        .HasForeignKey(vd => vd.ID_Producto);

    modelBuilder.Entity<VentaDetalle>()
        .HasOne(vd => vd.Venta)
        .WithMany(v => v.VentaDetalles)
        .HasForeignKey(vd => vd.ID_Venta);

    modelBuilder.Entity<Venta>()
        .HasOne(v => v.Local)
        .WithMany()
        .HasForeignKey(v => v.ID_Local);

    modelBuilder.Entity<Producto>()
        .HasOne(p => p.Marca)
        .WithMany()
        .HasForeignKey(p => p.ID_Marca);

    modelBuilder.Entity<Marca>()
           .HasMany(m => m.Producto)
           .WithOne(p => p.Marca)
           .HasForeignKey(p => p.ID_Marca);

    modelBuilder.Entity<Venta>()
      .HasOne(v => v.Local)
      .WithMany()
      .HasForeignKey(v => v.ID_Local);
  }

}
