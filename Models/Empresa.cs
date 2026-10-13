using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoX.Models;

[Table("EMPRESAS")]
public class Empresa
{
    public int Id { get; set;}
    public string? name {get;set;}
}