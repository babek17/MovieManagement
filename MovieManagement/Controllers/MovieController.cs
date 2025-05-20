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
    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public IActionResult MovieDetails(int movieId)
    {
        var movie = _movieService.GetMovieById(movieId);
        var movieDetails = new MovieDetails
        {
            Id = movie.MovieId, 
            Title = movie.Title,
            Comments = movie.Comments,
            Genre = movie.Genre,
            ImageUrl = movie.ImageUrl,
            TrailerUrl = movie.TrailerUrl,
            ReleaseYear = movie.ReleaseYear,
            RunningTime = movie.RunningTime,
            DirectorName = movie.Director.Name,
            Description = movie.ShortDescription
        };
        return View(movieDetails);
    }
    
    [HttpGet("search/movies")]
    public async Task<IActionResult> SearchMovies([FromQuery] string query)
    {
        ViewData["query"] = query;
        var results = await _movieService.SearchAsync(query);
        var movieCards = results.Select(m => new MovieCard
        {
            Id = m.MovieId,
            Title = m.Title,
            Year = m.ReleaseYear,
            Genre = m.Genre,
            ImageUrl = m.ImageUrl
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
    
}