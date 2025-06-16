using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.Entities;
using MovieManagement.Models;
using MovieManagement.Repositories;
using MovieManagement.Services;

namespace MovieManagement.Controllers;

public class DirectorController: Controller
{
    private readonly IDirectorService _directorService;
    private readonly IMovieService _movieService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserServices _userServices;
    public DirectorController(IDirectorService directorService, IMovieService movieService, UserManager<ApplicationUser> userManager, IUserServices userServices)
    {
        _directorService = directorService;
        _movieService = movieService;
        _userManager = userManager;
        _userServices = userServices;
    }

    [HttpGet]
    public IActionResult DirectorNotFound()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> DirectorDetails(int id)
    {
        var director = _directorService.GetDirectorById(id);
        if (director == null)
        {
            return RedirectToAction("DirectorNotFound");
        }
        string? userId = _userManager.GetUserId(User);
        HashSet<int> userWatchlistMovieIds = new();

        if (userId != null)
        {
            var userWatchlist = await _userServices.GetUserMovieCardsAsync(userId);
            userWatchlistMovieIds = userWatchlist.Select(m => m.Id).ToHashSet();
        }

        var movies = _movieService.GetAllMovies().Where(m => m.DirectorId == id);
        var movieCards = _movieService.BuildMovieCards(movies, userWatchlistMovieIds);
        
        var directorDetails = new DirectorDetails()
        {
            Director = director,
            MovieCards = movieCards
        };
        return View(directorDetails);
    }
}