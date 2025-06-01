using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.Entities;
using MovieManagement.Services;

namespace MovieManagement.Controllers;

public class UserController: Controller
{
    private readonly IUserServices _userServices;
    private readonly IMovieService _movieService;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(IUserServices userService, UserManager<ApplicationUser> userManager, IMovieService movieService)
    {
        _userServices = userService;
        _userManager = userManager;
        _movieService = movieService;
    }
    
    [HttpGet("watchlist")] 
    [Authorize]
    public async Task<IActionResult> Watchlist()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var movieCards = await _userServices.GetUserMovieCardsAsync(userId);
        return View(movieCards);  // Pass to Razor View
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddMovieToWatchlist(int movieId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        // You would probably fetch Movie by id here or send a movieId DTO
        var movie =  _movieService.GetMovieById(movieId);

        if (movie == null)
            return NotFound();

        await _userServices.AddMovieAsync(userId, movie);
        return RedirectToAction("Watchlist");  // Redirect back to the watchlist view
    }

    [HttpPost]
    public async Task<IActionResult> RemoveMovieFromWatchlist(int movieId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _userServices.RemoveMovieAsync(userId, movieId);
        return RedirectToAction("Watchlist");  // Redirect back to the watchlist view
    }
}