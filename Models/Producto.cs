using System.ComponentModel.DataAnnotations;

namespace MiProyecto
{
  public class Producto
  {
    [Key]
    public long ID_Producto { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public long ID_Marca { get; set; }
    public string Modelo { get; set; } = string.Empty;
    public int Costo_Unitario { get; set; }
    public Marca Marca { get; set; } = new Marca();
  }
}