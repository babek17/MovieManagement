using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.Entities;
using MovieManagement.Models;
using MovieManagement.Repositories;
using MovieManagement.Services;

namespace MovieManagement.Controllers;

public class MovieController: Controller
{
    private readonly IMovieService _movieService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserServices _userServices;
    public MovieController(IMovieService movieService, UserManager<ApplicationUser> userManager, IUserServices userServices)
    {
        _movieService = movieService;
        _userManager = userManager;
        _userServices = userServices;
    }

    public async Task<IActionResult> MovieDetails(int id)
    {
        var movie = _movieService.GetMovieById(id);
        var comments = await _movieService.GetNestedCommentsAsync(id);
        int? userRating = null;
        HashSet<int> userWatchlistMovieIds = new();
        if (User.Identity.IsAuthenticated)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userWatchlist =  _userServices.GetUserMovieCardsAsync(userId).Result;
            userWatchlistMovieIds = userWatchlist.Select(m => m.Id).ToHashSet();
            userRating = _userServices.GetUserRating(userId, id);
            ViewBag.UserRating = userRating;
        }
        var movieDetails = new MovieDetails
        {
            Id = movie.MovieId, 
            Title = movie.Title,
            Comments = comments,
            Genre = movie.Genre,
            ImageUrl = movie.ImageUrl,
            TrailerUrl = movie.TrailerUrl,
            ReleaseYear = movie.ReleaseYear,
            RunningTime = movie.RunningTime,
            DirectorName = movie.Director.Name,
            DirectorId = movie.DirectorId,
            Description = movie.ShortDescription,
            Rating = movie.Rating,
            UserRating = userRating,
            IsInWatchlist = userWatchlistMovieIds.Contains(movie.MovieId)
        };
        return View(movieDetails);
    }
    
    [HttpGet("search/movies")]
    public async Task<IActionResult> SearchMovies([FromQuery] string query)
    {
        ViewData["query"] = query;
        var results = await _movieService.SearchAsync(query);
        
        string? userId = _userManager.GetUserId(User);
        HashSet<int> userWatchlistMovieIds = new();
        
        if (userId != null)
        {
            var userWatchlist = await _userServices.GetUserMovieCardsAsync(userId);
            userWatchlistMovieIds = userWatchlist.Select(m => m.Id).ToHashSet();
        }
        
        var movieCards = results.Select(m => new MovieCard
        {
            Id = m.MovieId,
            Title = m.Title,
            Year = m.ReleaseYear,
            Genre = m.Genre,
            ImageUrl = m.ImageUrl,
            IsInWatchlist = userWatchlistMovieIds.Contains(m.MovieId)
        }).ToList();

        if (!movieCards.Any())
        {
            ViewData["Message"] = $"No movies matched your search for '{query}'.";
            return View("SearchMovies", new List<MovieCard>());
        }

        return View(movieCards);
    }

    public IActionResult Sort(string sortBy)
    {
        return RedirectToAction("Index", "Home", new { sortBy = sortBy });
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult RateMovie(int movieId, int score)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            throw new Exception("UserId is null!");
        _movieService.RateMovie(movieId, userId, score);
        TempData["Message"] = "Your rating was submitted!";
        return RedirectToAction("MovieDetails", new { id = movieId });
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddComment(int movieId, string comment, string? parentCommentId)
    {
        if (string.IsNullOrWhiteSpace(comment))
        {
            TempData["Error"] = "Comment cannot be empty.";
            return RedirectToAction("MovieDetails", new { id = movieId });
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = User.Identity.Name;

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName))
            throw new Exception("User information is incomplete!");

        await _movieService.AddCommentAsync(userId, userName,movieId, comment, parentCommentId);

        return RedirectToAction("MovieDetails", new { id = movieId });
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> RemoveComment(int movieId)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            throw new Exception("UserId is null!");
        await _movieService.DeleteCommentAsync(userId, movieId);
        return RedirectToAction("MovieDetails", new { id = movieId });
    }
    
}