using Microsoft.EntityFrameworkCore;
using MovieManagement.Entities;

namespace MovieManagement.Repositories;

public class MovieRepository: IMovieRepository
{
    private readonly MovieManagementDbContext _context;

    public MovieRepository(MovieManagementDbContext context)
    {
        _context = context;
    }
    
    public List<Movie> GetAllMovies()
    {
        return _context.Movies.ToList();
    }

    public Movie GetMovieById(int id)
    {
        var movie = _context.Movies.FirstOrDefault(m => m.MovieId == id);
        if (movie == null) throw new Exception("Movie not found");
        return movie;
    }

    public Movie GetMovieByTitle(string title)
    {
        var movie = _context.Movies.FirstOrDefault(m => m.Title == title);
        if (movie == null) throw new Exception("Movie not found");
        return movie;    
    }

    public List<Movie> GetAllMoviesByDirector(int id)
    {
        return _context.Movies.Include(m=>m.Director).Where(m=>m.Director.DirectorId==id).ToList(); 
    }
}