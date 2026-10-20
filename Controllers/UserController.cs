using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using ProyectoX.Data;
using ProyectoX.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace ProyectoX.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Profile(int? id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        var user = User.FindFirstValue(ClaimTypes.Name);
        if (user == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        var usuario = new Usuario();
        if(id == null)
        {
            usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }
        else
        {
            usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        
        var userModel = new ProfileViewModel
        {
            Nombre = usuario?.nombre,
            //Email = usuario?.Email,
            CreatedAt = usuario?.CreatedAt ?? DateTime.MinValue
        };

        var logStates = await _context.EstadosJuego.ToListAsync();

        ViewBag.states = logStates;
        ViewBag.userId = usuario?.Id;
        return View(userModel);
    }

    public async Task<IActionResult> Index(string? nameSearch)
    {
        var users = new List<Usuario>();

        if(nameSearch != null)
        {
            
        }
        else
        {
            users = _context.Usuarios.ToList();
        }
        return View(users);
    }
    
}