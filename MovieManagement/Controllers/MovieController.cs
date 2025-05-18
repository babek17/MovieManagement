using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.Entities;
using MovieManagement.Models;
using MovieManagement.Repositories;

namespace MovieManagement.Controllers;

public class MovieController: Controller
{
    private readonly IMovieRepository _movieRepository;
    // private readonly UserManager<ApplicationUser> _userManager;

    public MovieController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public IActionResult MovieDetails(int movieId)
    {
        // var user = await _userManager.GetUserAsync(User);
        var movie = _movieRepository.GetMovieById(movieId);
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
}