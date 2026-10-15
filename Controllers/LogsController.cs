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
    public async Task<IActionResult> Index(int? id, int pagina = 1)
    {
        if(id==null)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == User.FindFirstValue(ClaimTypes.Email));
            id = usuario.Id;
        }

        var offset = (pagina - 1) * TamanoPagina;

        var logs = await _context.Registros
        .Where(log => log.UserId == id)
        .OrderByDescending(log => log.LogDate)
        /*.Skip(offset)
        .Take(TamanoPagina)*/
        .ToListAsync();

        if(logs.Count == 0)
        {
            return View("~/Views/User/Logs.cshtml", new List<LogConJuegoViewModel>());
        }

        var gameIds = logs.Select(log => log.GameId)
        .Distinct()
        .ToList();

        var idsText = string.Join(",", gameIds);
        var games = await _igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games, 
            query: $"fields id, name, rating, cover.url; where id = ({idsText});");

        var logsConJuego = logs.Select(log => new LogConJuegoViewModel
        {
            Log = log,
            Juego = games.FirstOrDefault(j => j.Id == log.GameId)
        }).ToList();

        ViewBag.PaginaActual = pagina;

        

        return View("~/Views/User/Logs.cshtml", logsConJuego);
    }

    public async Task<IActionResult> Edit(int id, string gameState, string review, int rating)
    {
        var logToEdit = _context.Registros.FirstOrDefault(l => l.Id == id);

        if (logToEdit == null)
        {
            return NotFound();
        }
        logToEdit.GameState = gameState;
        logToEdit.Review = review;
        logToEdit.Rating = rating;

        await _context.SaveChangesAsync();

        return RedirectToAction("Index", new { id = logToEdit.UserId });
    }




}


