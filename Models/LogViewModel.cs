namespace ProyectoX.Models;

public class LogConJuegoViewModel
{
    public Log Log { get; set; } = null!;
    public IGDB.Models.Game? Juego { get; set; }
}