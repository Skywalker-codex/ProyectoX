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

public class FollowsController : Controller
{
     private readonly AppDbContext _context;

    public FollowsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Follow(int userId)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        var currentUserIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(currentUserIdValue, out var currentUserId))
        {
            return Unauthorized();
        }

        //Comprobar si ya lo sigue
        bool alreadyFollowed = await _context.Follows.AnyAsync(f => f.FollowerId == currentUserId && f.FollowedId == userId);

        if(alreadyFollowed) //Si ya lo sigue y ha pulsado el botón, borrar el follow de la base de datos.
        {
            var follow = await _context.Follows.FirstOrDefaultAsync(f => f.FollowerId == currentUserId && f.FollowedId == userId);

            _context.Follows.Remove(follow);
            _context.SaveChanges();

            return Ok(new {following = false});
        }

        var newFollow = new Follow
        {
            FollowerId = currentUserId,
            FollowedId = userId,
            CreatedAt = DateTime.Now
        };

        _context.Follows.Add(newFollow);
        await _context.SaveChangesAsync();

        return Ok(new {following = true});
    }

    [HttpGet]
    public async Task<IActionResult> FollowersIndex(int userId)
    {
        var followers = await _context.Follows.Where(f => f.FollowedId == userId).Select(f => f.Follower).ToListAsync();

            return View("~/Views/User/Followers.cshtml", followers);

    }

    [HttpGet]
    public async Task<IActionResult> FollowedIndex(int userId)
    {
        var followeds = await _context.Follows.Where(f => f.FollowerId == userId).Select(f => f.Followed).ToListAsync();

        return View("~/Views/User/Followeds.cshtml", followeds);
    }
}