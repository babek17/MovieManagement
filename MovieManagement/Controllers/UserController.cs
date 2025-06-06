using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieManagement.Entities;
using MovieManagement.Models;
using MovieManagement.Services;

namespace MovieManagement.Controllers;

public class UserController: Controller
{
    private readonly IUserServices _userServices;
    private readonly IMovieService _movieService;
    private readonly IDirectorService _directorService;
    public UserController(IUserServices userService, IDirectorService directorService, IMovieService movieService)
    {
        _userServices = userService;
        _movieService = movieService;
        _directorService = directorService;
    }
    
    [HttpGet] 
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

    [HttpGet]
    public IActionResult AddNewMovie()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddNewMovie(MovieDetails model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var movieId=_movieService.AddMovieAsync(model,model.Id);
        return RedirectToAction("MovieDetails","Movie",new { id = movieId });
    }
    
    [HttpGet]
    public IActionResult RemoveMovie(int movieId)
    {
        var movie = _movieService.GetMovieById(movieId);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ConfirmRemoveMovie(int movieId)
    {
        _movieService.RemoveMovie(movieId);
        TempData["SuccessMessage"] = "Movie deleted successfully.";
        return RedirectToAction("Index", "Home");
    }
}