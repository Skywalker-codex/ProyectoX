using Microsoft.EntityFrameworkCore;
using ProyectoX.Models;

namespace ProyectoX.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Usuario> Usuarios {get;set;}
    public DbSet<GameCache> Videojuegos {get;set;}
    public DbSet<Empresa> Empresas {get;set;}
    public DbSet<Log> Registros {get;set;}
}