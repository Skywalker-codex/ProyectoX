using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoX.Models;
using IGDB;
using IGDB.Models;

namespace ProyectoX.Controllers;

public class HomeController : Controller
{
    private readonly IGDBClient _igdb;

    public HomeController(IGDBClient igdb)
    {
        _igdb = igdb;
    }

    [HttpGet]
    public async Task<IActionResult>  Index(string? name, int limit)
    {
        if(limit > 50)
        {
            limit = 50;
        }
        else if (limit == 0 || limit < 0)
        {
            limit = 10;
        }

        name="Zelda";

        var games = await _igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields name, rating_count, total_rating; where name ~ *\"" + name + "\"* & rating_count > 100; limit " + limit + ";");

        return View(games);
    }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
