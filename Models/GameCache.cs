using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoX.Models;

[Table("VIDEOJUEGOS")]
public class GameCache
{
    public int Id { get; set; }
    public int IgdbId { get; set; }
    public string? Nombre { get; set; }
    public DateTime? FechaDeLanzamiento { get; set; }
    public string? Descripcion { get; set; }
    public string? Portada { get; set; }
    public int? EmpresaId { get; set; }

    public DateTime GuardadoEn { get; set; }

    [ForeignKey("EmpresaId")]
    public Empresa? Empresa { get; set; }
}