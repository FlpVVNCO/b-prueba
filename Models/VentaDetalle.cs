using System.ComponentModel.DataAnnotations;

namespace MiProyecto
{
  public class VentaDetalle
  {
    [Key]
    public long ID_VentaDetalle { get; set; }
    public long ID_Venta { get; set; }
    public int Precio_Unitario { get; set; }
    public int Cantidad { get; set; }
    public int TotalLinea { get; set; }
    public long ID_Producto { get; set; }
    public Venta Venta { get; set; } = new Venta();
    public Producto Producto { get; set; } = new Producto();
  }
}