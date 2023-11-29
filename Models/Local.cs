using System.ComponentModel.DataAnnotations;

namespace MiProyecto
{
  public class Local
  {
    [Key]
    public long ID_Local { get; set; }
    public string Nombre { get; set; } = string.Empty; // Inicializar propiedades no nulas
    public string Direccion { get; set; } = string.Empty; // Inicializar propiedades no nulas
  }
}