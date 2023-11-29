using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiProyecto
{
  public class Venta
  {
    [Key]
    public long ID_Venta { get; set; }
    public int Total { get; set; }
    public DateTime Fecha { get; set; }

    public long ID_Local { get; set; }

    public Local Local { get; set; } = new Local(); // Propiedad de navegación
    public List<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
  }
}
