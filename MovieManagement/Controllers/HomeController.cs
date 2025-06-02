using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.Entities;
using MovieManagement.Models;
using MovieManagement.Repositories;
using MovieManagement.Services;

namespace MovieManagement.Controllers;

public class HomeController: Controller
{
    private readonly IMovieService _movieService;
    private readonly IUserServices _userServices;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(IMovieService movieService, IUserServices userServices, UserManager<ApplicationUser> userManager)
    {
        _movieService = movieService;
        _userServices = userServices;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(string sortBy = "Title", int page = 1, string genre = "All Genres")
    {
        string? userId = _userManager.GetUserId(User);
        HashSet<int> userWatchlistMovieIds = new();

        if (userId != null)
        {
            var userWatchlist = await _userServices.GetUserMovieCardsAsync(userId);
            userWatchlistMovieIds = userWatchlist.Select(m => m.Id).ToHashSet();
        }

        var movieQuery = new MovieQuery
        {
            SortBy = sortBy,
            Page = page,
            Genre = genre,
        };

        var result = _movieService.GetFilteredMoviesAsync(movieQuery);
        var movieCards = _movieService.BuildMovieCards(result.Movies, userWatchlistMovieIds);

        ViewBag.CurrentPage = result.CurrentPage;
        ViewBag.TotalPages = result.TotalPages;
        ViewBag.SelectedGenre = genre;
        ViewBag.SortBy = sortBy;
        return View(movieCards);
    }
    
    
}