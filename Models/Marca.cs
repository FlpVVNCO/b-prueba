using System.ComponentModel.DataAnnotations;

namespace MiProyecto
{
  public class Marca
  {
    [Key]
    public long? ID_Marca { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public List<Producto> Producto { get; set; } = new List<Producto>(); // Inicializar propiedades no nulas
  }
}