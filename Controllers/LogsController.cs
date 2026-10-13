using IGDB;
using IGDB.Models;
using Microsoft.AspNetCore.Mvc;
using ProyectoX.Data;
using ProyectoX.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ProyectoX.Controllers;

public class LogsController : Controller
{
    private readonly AppDbContext _context;
    private readonly IGDBClient _igdb;
    private const int TamanoPagina = 10;

    public LogsController(AppDbContext context, IGDBClient igdb)
    {
        _context = context;
        _igdb = igdb;
    }

    [Authorize]
    public async Task<IActionResult> Index(int pagina = 1)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == User.FindFirstValue(ClaimTypes.Email));

        var offset = (pagina - 1) * TamanoPagina;

        var logs = await _context.Registros
        .Where(log => log.UserId == usuario.Id)
        .OrderByDescending(log => log.LogDate)
        /*.Skip(offset)
        .Take(TamanoPagina)*/
        .ToListAsync();

        return View("~/Views/User/Logs.cshtml", logs);
    }




}


