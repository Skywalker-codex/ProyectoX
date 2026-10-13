using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using ProyectoX.Data;
using ProyectoX.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security;
using IGDB;
using IGDB.Models;

namespace ProyectoX.Controllers;

public class IgdbController : Controller
{
    private readonly IGDBClient _igdb;

    public IgdbController(IGDBClient igdb)
    {
        _igdb = igdb;
    }

    [HttpGet]
    public async Task<IActionResult>  List(string? name, int limit)
    {
        if(limit > 50)
        {
            limit = 50;
        }
        else if (limit == 0 || limit < 0)
        {
            limit = 10;
        }

        var games = await _igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields name; limit " + limit + ";");

        return View(games);
    }
}