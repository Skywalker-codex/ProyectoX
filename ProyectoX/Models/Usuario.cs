using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoX.Models;

[Table("usuarios")]
public class Usuario
{
    public int Id { get; set;}
    public string? nombre {get;set;}
    public string? Email{get;set;}
    public string? Password {get;set;}
    public int FailedLoginAttempts {get;set;}
    public DateTime? LockoutEnd {get;set;}
    public DateTime CreatedAt {get;set;}
}