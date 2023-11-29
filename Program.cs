using System;
using System.Linq;
using MiProyecto;
using Microsoft.EntityFrameworkCore;

class Program
{
  static void Main()
  {
    using (var context = new MyDbContext()) // Reemplaza "TuDbContext" con el nombre de tu DbContext
    {



      context.ChangeTracker.Clear();

      var datos = context.VentaDetalle
          .Where(vd => vd.Venta.Fecha >= DateTime.Now.AddDays(-30))
          .Include(vd => vd.Venta.Local)
          .Include(vd => vd.Producto)
          .ThenInclude(p => p.Marca)
          .ToList();


      // Pregunta 1: El total de ventas de los últimos 30 días (monto total y cantidad total de ventas).
      var totalMontoVentas = datos.Sum(s => s.TotalLinea);
      var cantidadTotalVentas = datos.Count;
      Console.WriteLine($"Pregunta 1: Total de monto de ventas en los últimos 30 días: {totalMontoVentas}");
      Console.WriteLine($"           Cantidad total de ventas en los últimos 30 días: {cantidadTotalVentas}");

      // Pregunta 2: El día y hora en que se realizó la venta con el monto más alto (y cuál es aquel monto).
      var ventaMasAlta = datos.OrderByDescending(s => s.TotalLinea).FirstOrDefault();
      var fechaVentaMasAlta = ventaMasAlta?.Venta.Fecha ?? DateTime.MinValue;
      var montoVentaMasAlta = ventaMasAlta?.TotalLinea ?? 0;
      Console.WriteLine($"Pregunta 2: La venta más alta ocurrió el {fechaVentaMasAlta} con un monto de {montoVentaMasAlta}");

      // Pregunta 3: Indicar cuál es el producto con mayor monto total de ventas.
      var productoMasVentas = datos.GroupBy(vd => vd.ID_Producto)
          .Select(group => new
          {
            ID_Producto = group.Key,
            Nombre_Producto = group.FirstOrDefault()?.Producto.Nombre,
            TotalVentas = group.Sum(vd => vd.TotalLinea),
            Detalles = group.ToList()
          })
          .OrderByDescending(x => x.TotalVentas)
          .FirstOrDefault();
      Console.WriteLine($"Pregunta 3: El producto con mayor monto total de ventas (ID_Producto): {productoMasVentas?.ID_Producto}");
      Console.WriteLine($"           Nombre del producto: {productoMasVentas?.Nombre_Producto}");
      Console.WriteLine($"           Monto de ventas: {productoMasVentas?.TotalVentas}");

      // Pregunta 4: Indicar el local con mayor monto de ventas.
      var localConMayorVentas = datos.GroupBy(vd => vd.Venta.ID_Local)
          .Select(group => new
          {
            ID_Local = group.Key,
            Nombre_Local = group.FirstOrDefault()?.Venta.Local.Nombre,
            TotalVentasLocal = group.Sum(vd => vd.TotalLinea),
            Detalles = group.ToList()
          })
          .OrderByDescending(x => x.TotalVentasLocal)
          .FirstOrDefault();
      Console.WriteLine($"Pregunta 4: El local con mayor monto de ventas (ID_Local): {localConMayorVentas?.ID_Local}");
      Console.WriteLine($"           Nombre del local: {localConMayorVentas?.Nombre_Local}");
      Console.WriteLine($"           Monto de ventas: {localConMayorVentas?.TotalVentasLocal}");

      // Pregunta 5: ¿Cuál es la marca con mayor margen de ganancias?
      var marcaConMayorMargen = datos.GroupBy(vd => vd.Producto.ID_Marca)
          .Select(group => new
          {
            ID_Marca = group.Key,
            MargenGanancias = group.Sum(vd => (vd.Cantidad * vd.Precio_Unitario) - (vd.Cantidad * vd.Producto.Costo_Unitario)),
            Nombre_Marca = group.First().Producto.Marca.Nombre  // Acceder al nombre de la marca
          })
          .OrderByDescending(x => x.MargenGanancias)
          .FirstOrDefault();

      Console.WriteLine($"Pregunta 5: La marca con mayor margen de ganancias (ID_Marca): {marcaConMayorMargen?.ID_Marca}");
      Console.WriteLine($"           Nombre de la marca: {marcaConMayorMargen?.Nombre_Marca}");
      Console.WriteLine($"           Margen de ganancias: {marcaConMayorMargen?.MargenGanancias}");

      // Pregunta 6: ¿Cómo obtendrías cuál es el producto que más se vende en cada local?
      var productosMasVendidosPorLocal = datos
          .GroupBy(vd => new { vd.Venta.ID_Local, vd.ID_Producto, vd.Producto.Nombre })
          .Select(group => new
          {
            ID_Local = group.Key.ID_Local,
            ID_Producto = group.Key.ID_Producto,
            Nombre_Producto = group.Key.Nombre,
            TotalVentas = group.Sum(vd => vd.Cantidad),
            Detalles = group.ToList()
          })
          .OrderBy(x => x.ID_Local)
          .GroupBy(x => x.ID_Local)
          .Select(group => new
          {
            ID_Local = group.Key,
            Empates = group.Where(x => x.TotalVentas == group.Max(y => y.TotalVentas))
          })
          .ToList();

      Console.WriteLine("Pregunta 6: Productos más vendidos por local:");
      foreach (var productoPorLocal in productosMasVendidosPorLocal)
      {
        Console.WriteLine($"En el local {productoPorLocal.ID_Local}, los productos más vendidos son:");

        foreach (var empate in productoPorLocal.Empates)
        {
          Console.WriteLine($"  ID_Producto: {empate.ID_Producto}, Nombre del producto: {empate.Nombre_Producto}, TotalVentas: {empate.TotalVentas}");
        }
      }

      Console.WriteLine("Terminó...");
    }

  }
}