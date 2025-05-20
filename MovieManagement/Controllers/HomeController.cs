using Microsoft.AspNetCore.Mvc;
using MovieManagement.Models;
using MovieManagement.Repositories;
using MovieManagement.Services;

namespace MovieManagement.Controllers;

public class HomeController: Controller
{
    private readonly IMovieService _movieService;

    public HomeController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public IActionResult Index(string sortBy= "Title", int page=1)
    {
        int pageSize = 12;
        int totalMovies = _movieService.GetAllMovies().Count();
        int totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

        var movies =  _movieService.Sort(sortBy);

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;

        var movieCard = movies.Skip((page-1)*pageSize).Take(pageSize).
            Select(mc => new MovieCard()
        {
            Id = mc.MovieId,
            Title = mc.Title,
            Year = mc.ReleaseYear,
            Genre = mc.Genre,
            ImageUrl = mc.ImageUrl
        });
        return View(movieCard);
    }
    
    
}