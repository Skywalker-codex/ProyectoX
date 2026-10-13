using IGDB;
using IGDB.Models;
using Microsoft.AspNetCore.Mvc;
using ProyectoX.Data;
using ProyectoX.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ProyectoX.Controllers;

public class GamesController : Controller
{
    private readonly IGDBClient _igdb;
    private const int TamanoPagina = 10;
    private readonly AppDbContext _context;

    public GamesController(IGDBClient igdb, AppDbContext context)
    {
        _igdb = igdb;
        _context = context;
    }

    public async Task<IActionResult> Index(int pagina = 1)
    {
        var offset = (pagina - 1) * TamanoPagina;
        var popIds = await _igdb.QueryAsync<PopularityPrimitive>(IGDBClient.Endpoints.PopularityPrimitives, 
            query: "fields game_id, value; where popularity_type=1; sort value desc; limit " + (TamanoPagina + 1) + "; offset " +  offset + ";");

        var ids = popIds
        .Take(TamanoPagina)
        .Where(p => p.GameId.HasValue)
        .Select(p => p.GameId!.Value)
        .ToList();

        var idsString = string.Join(",", ids);

        var games = await _igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games, 
            query: $"fields id, name, rating, cover.url; where id = ({idsString});");

        var juegosOrdenados = ids
            .Select(id => games.FirstOrDefault(g => g.Id == id))
            .Where(g => g != null)
            .ToList();

        ViewBag.Pagina = pagina;
        ViewBag.HayPaginaAnterior = pagina > 1;
        ViewBag.HayPaginaSiguiente = popIds.Count() > TamanoPagina;

        return View(juegosOrdenados);
    }

    public async Task<IActionResult>Details(int id)
    {
        var game = await _igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games,
        query: $"fields id, name, summary, cover.url, first_release_date, genres.name, platforms.name, total_rating; where id = {id};");

        var detailedGame = game.FirstOrDefault(g => g.Id == id);

        if(detailedGame == null)
        {
            return NotFound();
        }
        return View(detailedGame);
    }


    [HttpPost]/*Aqui se creará el Log*/
    [Authorize]
    public async Task<IActionResult> LogGame(int gameId, string gameState, string review, int rating)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);


        var newLog = new Log
        {
            GameState = gameState,
            Review = review,
            Rating = rating,
            UserId = usuario.Id,
            GameId = gameId,
            LogDate = DateTime.Now
        };

        _context.Registros.Add(newLog);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

}