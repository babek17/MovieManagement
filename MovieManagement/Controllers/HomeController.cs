using Microsoft.AspNetCore.Mvc;
using MovieManagement.Models;
using MovieManagement.Repositories;

namespace MovieManagement.Controllers;

public class HomeController: Controller
{
    private readonly IMovieRepository _movieRepository;

    public HomeController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public IActionResult Index(int page=1)
    {
        int pageSize = 12;
        int totalMovies = _movieRepository.GetAllMovies().Count();
        int totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;

        var movieCard = _movieRepository.GetAllMovies().Skip((page-1)*pageSize).Take(pageSize).
            Select(mc => new MovieCard()
        {
            Title = mc.Title,
            Rating = mc.Rating,
            Year = mc.ReleaseYear,
            Genre = mc.Genre,
            ImageUrl = mc.ImageUrl
        });
        
        return View(movieCard);
    }
    
    
}