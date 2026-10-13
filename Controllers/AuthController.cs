using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using ProyectoX.Data;
using ProyectoX.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security;

namespace ProyectoX.Controllers;

public class AuthController : Controller
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ModelState.AddModelError("", "Todos los campos son obligatorios.");
            return View();
        }

        var user = _context.Usuarios.FirstOrDefault(u => u.Email == email);
        if(user == null)
        {
            ModelState.AddModelError("", "El email es incorrecto");
            return View();
        }

        var passwordHasher = new PasswordHasher<Usuario>();
        var hasherResult = passwordHasher.VerifyHashedPassword(user, user.Password, password);
        if(hasherResult != PasswordVerificationResult.Success)
        {
            ModelState.AddModelError("", "La contraseña es incorrecta");
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.nombre),
            new Claim(ClaimTypes.Email, user.Email)
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity)
        );

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string nombre, string email, string password)
    {
        Console.WriteLine($"--> LLEGÓ AL REGISTRO: {nombre} - {email}");
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ModelState.AddModelError("", "El correo y la contraseña son obligatorios.");
            return View();
        }

        var passwordHasher = new PasswordHasher<Usuario>();

        var nuevoUsuario = new Usuario
        {
            nombre = nombre,
            Email = email,
            Password = password, //Encriptar contraseña
            CreatedAt = DateTime.Now,
            FailedLoginAttempts = 0
        };
        nuevoUsuario.Password = passwordHasher.HashPassword(nuevoUsuario, password);
        _context.Usuarios.Add(nuevoUsuario);
        await _context.SaveChangesAsync();
        return RedirectToAction("Login");
    }


    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}