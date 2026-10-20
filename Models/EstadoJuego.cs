using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoX.Models
{
    [Table("estados_juego")]
    public class EstadoJuego
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
    }
}