using Microsoft.AspNetCore.Mvc;
using MovieManagement.Repositories;

namespace MovieManagement.Controllers;

public class MovieController: Controller
{
    private readonly IMovieRepository _movieRepository;

    public MovieController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

        
}