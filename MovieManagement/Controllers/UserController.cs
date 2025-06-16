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
    public IActionResult AddNewMovie(int? directorId)
    {
        var model = new MovieDetails();
        if (directorId.HasValue)
        {
            model.DirectorId = directorId.Value;
            var director = _directorService.GetAllDirectors()
                .FirstOrDefault(d => d.DirectorId == directorId.Value);
            if (director != null)
                ViewBag.SelectedDirector = $"{director.Name}, {director.Age}";
        }
        ViewBag.FormTitle = "Add New Movie";
        ViewBag.Action = "AddNewMovie";
        ViewBag.Method = "POST";
        return View("_MovieForm", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddNewMovie(MovieDetails model, int directorId)
    {
        if (model.DirectorId <= 0)
        {
            ModelState.AddModelError("DirectorId", "Please select a director.");
            return View("_MovieForm", model);
        }

        var rootPath = Directory.GetCurrentDirectory();
        await _movieService.AddMovieAsync(model, rootPath, directorId);

        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    public IActionResult EditMovie(int movieId)
    {
        var model = _movieService.GetMovieById(movieId);
        if (model == null) return NotFound();

        var movieDetails = new MovieDetails()
        {
            Id  = model.MovieId,
            Title  = model.Title,
            Rating = model.Rating,
            TrailerUrl = model.TrailerUrl,
            DirectorId = model.DirectorId,
            ImageUrl = model.ImageUrl,
            Description = model.ShortDescription,
            ReleaseYear = model.ReleaseYear,
            Genre = model.Genre,
            RunningTime = model.RunningTime
        };
        movieDetails.DirectorId = model.DirectorId;
        movieDetails.CurrentImageUrl = model.ImageUrl;
        ViewBag.FormTitle = "Edit Movie";
        ViewBag.Action = "EditMovie";
        ViewBag.Method = "POST";
        return View("_MovieForm", movieDetails);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditMovie(MovieDetails model)
    {
        if (ModelState.IsValid)
        {
            return View("_MovieForm", model);
        }
        
        _movieService.EditMovieAsync(model);

        return RedirectToAction("Index", "Home");
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
    
    [HttpGet]
    public IActionResult AddDirector()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddDirector(DirectorToAdd model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var rootPath = Directory.GetCurrentDirectory();
        int directorId=  _directorService.AddDirectorAsync(model, rootPath);
        return RedirectToAction("AddNewMovie",new {directorId});
    }
    
    [HttpGet]
    public IActionResult RemoveDirector(int directorId)
    {
        var director = _directorService.GetDirectorById(directorId);
        if (director == null)
        {
            return NotFound();
        }

        return View(director);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ConfirmRemoveDirector(int directorId)
    {
        _directorService.RemoveDirector(directorId);
        TempData["SuccessMessage"] = "Director deleted successfully.";
        return RedirectToAction("Index", "Home");
    }
    
}