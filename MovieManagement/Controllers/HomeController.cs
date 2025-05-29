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

    public IActionResult Index(string sortBy= "Title", int page=1, string genre = "All Genres", string query="")
    {

        var movies = _movieService.GetAllMovies();
        if (!string.IsNullOrWhiteSpace(query))
            movies = movies.Where(m => m.Title.ToLower().Contains(query.ToLower()));

        if (genre != "All Genres")
        {
            movies =  movies.Where(m=>m.Genre==genre);
        }
        movies =  _movieService.Sort(movies, sortBy);
        
        int pageSize = 12;
        int totalMovies = movies.Count();
        int totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

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
        
        ViewBag.SelectedGenre = genre;
        ViewBag.SortBy = sortBy;
        ViewBag.Query = query;
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        
        return View(movieCard);
    }
    
    
}